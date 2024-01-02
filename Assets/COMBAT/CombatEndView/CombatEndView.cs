using Combat;
using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


namespace Combat
{
    public class CombatEndView : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI headingTxt; 
        [SerializeField] Transform charPortContainer;
        [SerializeField] CombatEndContinueBtn combatEndContinueBtn;
        [SerializeField] ManualExpBtn manualExpBtn; 
        [SerializeField] List<CharController> allAllyInclDeadNFled = new List<CharController>();


        [Header(" Global var")]
        public bool manualExpBtnPressed = false; 
        public bool manualExpRewarded = false;

        CombatEndCondition combatEndCondition;
        CombatResult combatResult;

    
        public void ShowCombatPanel()
        {
           
        }
        public void InitCombatEndView()
        {
            this.combatResult= CombatEventService.Instance.currCombatResult;    
  
            allAllyInclDeadNFled = CharService.Instance
                                 .allCharsInPartyLocked.Where(t => t.charModel.orgCharMode == CharMode.Ally).ToList();
            combatEndContinueBtn.InitContinueBtn(this); 
            manualExpBtn.ManualExpBtnInit(this);
            FillCharPort();
            FillHeading();
            transform.GetChild(0).gameObject.SetActive(true);
        }

        public void OnManualExpAwarded()
        {
            manualExpBtn.StateNA(); 
            manualExpRewarded = true; 
        }
        public void CloseCombatView()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        void FillHeading()
        {
            switch (combatResult)
            {
                case CombatResult.None:
                    break;
                case CombatResult.Victory:
                    headingTxt.text = "VICTORY"; 
                    break;
                case CombatResult.Draw:
                    headingTxt.text = "DRAW";
                    break;
                case CombatResult.Defeat:
                    headingTxt.text = "DEFEAT";
                    break;
                default:
                    break;
            }
        }
        void FillCharPort()
        {
            int charSharedExp = CombatService.Instance.GetSharedExp();
            for (int i = 0; i < charPortContainer.childCount; i++)
            {
                if (i < allAllyInclDeadNFled.Count)
                {
                    charPortContainer.GetChild(i).gameObject.SetActive(true);
                    charPortContainer.GetChild(i).GetComponent<PortView>()
                                    .InitPortView(allAllyInclDeadNFled[i].charModel, charSharedExp, this); 
                }
                else
                {
                    charPortContainer.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
  
        
        public bool IsOnlyAbbas()
        {
           if(allAllyInclDeadNFled.Count == 1)
            {
                if (allAllyInclDeadNFled[0].charModel.charName == CharNames.Abbas) return true; 
            }
            return false; 
        }


    }
}