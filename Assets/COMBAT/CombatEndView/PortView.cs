using Common;
using DG.Tweening;
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
        [SerializeField] Image charPortImg; 

        

        [Header(" Lvl up Bird")]
        [SerializeField] Image lvlUpBird;


        [Header(" Lvl Bar TBR")]
        [SerializeField] Image lvlbarImg;
        [SerializeField] OnLvlBarHover onLvlBarHover; 
        [SerializeField] LvlNExpSO lvlNExpSO;

        [Header(" Char lvl Text")]
        [SerializeField] TextMeshProUGUI lvlTxt; 


        CombatEndView combatEndView; 

        [SerializeField] AllCharSO allCharSO; 
        [Header("Global var")]
        [SerializeField] CharModel charModel;
        [SerializeField]int sharedExp;
        [SerializeField] int manualExp;
        [SerializeField] int firstBloodExp;
        [SerializeField] int killsNSavesExp; 
        public void InitPortView(CharModel charModel, int sharedExp, CombatEndView combatEndView)
        {
            this.charModel = charModel;
            this.combatEndView= combatEndView;
            onLvlBarHover.InitOnLvlBarHover(charModel);
            FillPort(sharedExp);
            bgPortImg.sprite = allCharSO.bgPortUnClicked;
            FillLvlExpBar(); 
            ResetLvlUpBirds();
            CalcFirstBloodExp();
            CalcKillsNSavesExp();            
        }
        void CalcKillsNSavesExp()
        {
            CombatModel combatModel = CombatEventService.Instance.combatModel;
            int savesExp = combatModel.GetSavesExp(charModel.charID);     
            int killsExp = combatModel.GetKillsExp(charModel.charID);
            killsNSavesExp = sharedExp*(savesExp + killsExp);
            
        }
        void CalcFirstBloodExp()
        {
            if(charModel.charID == combatEndView.firstBloodChar.charModel.charID)
            {
                firstBloodExp = 6 * charModel.charLvl;
            }
            else
            {
                firstBloodExp = 0;
            }
        }
        void ResetLvlUpBirds()
        {
            Sequence seq = DOTween.Sequence();
            seq                
                .AppendCallback(() => lvlUpBird.gameObject.SetActive(false))
                .Append(lvlUpBird.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f,-1000f), 0.4f, true))                
                ;
            seq.Play();
        }   
        void FillLvlExpBar()
        {           
            int deltaExp = lvlNExpSO.GetdeltaExpPts4Lvl(charModel.charLvl);
            int thresholdExp = lvlNExpSO.GetThresholdExpPts4Lvl(charModel.charLvl);
            lvlbarImg.DOFillAmount(((float)charModel.mainExp -thresholdExp) / deltaExp,0.1f);
            int currExp = charModel.mainExp + sharedExp + manualExp + firstBloodExp + killsNSavesExp; 
            if (!charModel.LvlUpCharChk(currExp))
            {
                charModel.LvlNExpUpdate(currExp);
                float val = (float)(currExp - thresholdExp) / deltaExp;
                Sequence seq = DOTween.Sequence();
                seq
                    .AppendInterval(1f)
                    .Append(lvlbarImg.DOFillAmount(val, 0.4f))
                    ;
                seq.Play();
            }
            else
            {
                 charModel.LvlNExpUpdate(currExp);
                 deltaExp = lvlNExpSO.GetdeltaExpPts4Lvl(charModel.charLvl);
                 thresholdExp = lvlNExpSO.GetThresholdExpPts4Lvl(charModel.charLvl);

                float val1 = (float)(currExp - thresholdExp) / deltaExp;
                // exp gain in combat
                Sequence seq = DOTween.Sequence();

                seq
                    .AppendInterval(1f)
                    .Append(lvlbarImg.DOFillAmount(1, 0.4f))
                    .Append(lvlbarImg.DOFillAmount(0, 0.1f))
                    .AppendInterval(0.4f)
                    .AppendCallback(() => lvlUpBird.gameObject.SetActive(true))
                    .Append(lvlUpBird.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, -98f), 0.4f, true))
                    .Append(lvlbarImg.DOFillAmount(val1, 0.4f))
                    ;
                seq.Play(); 
            }
            lvlTxt.text = charModel.charLvl.ToString();
            onLvlBarHover.InitOnLvlBarHover(charModel);
        }
        void FillPort(int sharedExp)
        {
            
            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            ClassType classType = charModel.classType; 
            nametxt.text = classType.ToString().CreateSpace();

            if (charModel.stateOfChar == StateOfChar.UnLocked)
            {                
                charPortImg.sprite = charSO.bpPortraitUnLocked;
                bgPortImg.sprite = charCompSO.frameAvail;
                lvlbarImg.sprite  = CharService.Instance.charComplimentarySO.lvlBarAvail;
                Sprite BGUnClicked = CharService.Instance.charComplimentarySO.BGAvailUnClicked;
                Sprite BGClicked = CharService.Instance.charComplimentarySO.BGAvailClicked;

                bgPortImg.gameObject.SetActive(true); 
                bgPortImg.sprite = BGUnClicked;

                this.sharedExp = sharedExp;
            }
            else // fled and dead
            {
                charPortImg.gameObject.SetActive(false);
                charPortImg.sprite = charSO.bpPortraitUnAvail;
                bgPortImg.sprite = charCompSO.frameUnavail;
                // SIDE BARS LVL
                lvlbarImg.sprite =  CharService.Instance.charComplimentarySO.lvlbarUnAvail;
                this.sharedExp = 0;                
            }
            expDetailedView.InitExp(charModel, sharedExp, firstBloodExp, killsNSavesExp);

            //transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
            //                                    = charModel.classType.ToString().CreateSpace();

        }

        public void AddManualExp()
        {
            manualExp = CombatService.Instance.GetManualExp();
            expDetailedView.AddManualExpDsply(manualExp);
            combatEndView.OnManualExpAwarded();
            FillLvlExpBar();
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
            //if (CanAwardManualExp())
            //{
                bgPortImg.sprite = allCharSO.bgPortUnClicked;
            //}
        }
    }
}