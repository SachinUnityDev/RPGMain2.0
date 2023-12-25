using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 
namespace Combat
{
    public class PortView : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI nametxt;
        [SerializeField] ExpDetailedView expDetailedView; 

        [Header("Global var")]
        [SerializeField] CharModel charModel;
        [SerializeField] int sharedExp; 



        public void InitPortView(CharModel charModel, int sharedExp)
        {
            this.charModel = charModel;
            FillPort(sharedExp); 
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

        void FillSharedExp()
        {

        }
    }
}