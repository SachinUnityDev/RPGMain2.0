using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Combat
{
    public class PortView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI nametxt;
        [SerializeField] ExpDetailedView expDetailedView;
        [SerializeField] Image bgPortImg;

        CombatEndView combatEndView; 

        [SerializeField] AllCharSO allCharSO; 
        [Header("Global var")]
        [SerializeField] CharModel charModel;
        [SerializeField]int sharedExp;
        [SerializeField] int manualExp; 
        public void InitPortView(CharModel charModel, int sharedExp, CombatEndView combatEndView)
        {
            this.charModel = charModel;
            this.combatEndView= combatEndView;  
            FillPort(sharedExp);
            bgPortImg.sprite = allCharSO.bgPortUnClicked;
        }
        void FillPort(int sharedExp)
        {
            
            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            string charNameStr = charModel.charNameStr;
            nametxt.text = charNameStr.CreateSpace();

            if (charModel.stateOfChar == StateOfChar.UnLocked)
            {                
                transform.GetChild(1).GetComponent<Image>().sprite = charSO.bpPortraitUnLocked;
                transform.GetChild(2).GetComponent<Image>().sprite = charCompSO.frameAvail;
                transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                                    = CharService.Instance.charComplimentarySO.lvlBarAvail;
                Sprite BGUnClicked = CharService.Instance.charComplimentarySO.BGAvailUnClicked;
                Sprite BGClicked = CharService.Instance.charComplimentarySO.BGAvailClicked;

                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Image>().sprite
                                                            = BGUnClicked;

                this.sharedExp = sharedExp;
            }
            else // fled and dead
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).GetComponent<Image>().sprite
                                                               = charSO.bpPortraitUnAvail;
                transform.GetChild(2).GetComponent<Image>().sprite
                                              = charCompSO.frameUnavail;
                // SIDE BARS LVL
                transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                            = CharService.Instance.charComplimentarySO.lvlbarUnAvail;
                this.sharedExp = 0;                
            }
            expDetailedView.InitExp(charModel, sharedExp);

            transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
                                                = charModel.classType.ToString().CreateSpace();

        }

        public void AddManualExp()
        {
            manualExp = CombatService.Instance.GetManualExp();
            expDetailedView.AddManualExpDsply(manualExp);
            combatEndView.OnManualExpAwarded();
        }

        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (CanAwardManualExp())
            {
                AddManualExp();
                combatEndView.manualExpRewarded = true;
            }
        }
        bool CanAwardManualExp()
        {
            if (charModel.charName == CharNames.Abbas) return false;            
            if (!combatEndView.manualExpBtnPressed) return false;
            if (combatEndView.manualExpRewarded) return false;
            return true; 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (CanAwardManualExp())
            {
                bgPortImg.sprite = allCharSO.bgPortClicked;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {   
            if (CanAwardManualExp())
            {
                bgPortImg.sprite = allCharSO.bgPortUnClicked;
            }
        }
    }
}