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
    public class CombatEndView : MonoBehaviour, INotify
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI headingTxt; 
        [SerializeField] Transform charPortContainer;
        [SerializeField] CombatEndContinueBtn combatEndContinueBtn;
        [SerializeField] ManualExpBtn manualExpBtn; 
        [SerializeField] List<CharController> allAllyInclDeadNFled = new List<CharController>();

        [Header(" Notify Box View TBR")]
        [SerializeField] NotifyBoxView notifyBoxView;


        [Header(" Global var")]
        public bool manualExpBtnPressed = false; 
        public bool manualExpRewarded = false;

        CombatEndCondition combatEndCondition;
        CombatResult combatResult;
        public CharController firstBloodChar;

        public NotifyName notifyName { get; set; }
        public bool isDontShowItAgainTicked { get ; set; }

        Transform endTrans; 
        public void InitCombatEndView()
        {
            this.combatResult= CombatEventService.Instance.currCombatResult;    
  
            allAllyInclDeadNFled = CharService.Instance
                                 .allCharsInPartyLocked.Where(t => t.charModel.orgCharMode == CharMode.Ally).ToList();
            combatEndContinueBtn.InitContinueBtn(this); 
            manualExpBtn.ManualExpBtnInit(this);
            FillCharPort();
            FillHeading();
            Transform lootTrans = transform.GetChild(0);
            endTrans = transform.GetChild(1); 
            endTrans.SetAsFirstSibling();
            lootTrans.SetAsLastSibling();
            lootTrans.gameObject.SetActive(true);
            endTrans.gameObject.SetActive(true);

        }
        public void SetCharAsFirstBlood(CharController charController)
        {
            firstBloodChar = charController;
        }
        public void OnManualExpAwarded()
        {
            manualExpBtn.StateNA(); 
            manualExpRewarded = true; 
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

        public void OnNotifyAnsPressed()
        {
            LootService.Instance.lootView.UnLoad();
            UnLoad();
        }

        public void OnContinueBtnClick()
        {
            if (LootService.Instance.isLootDsplyed)
            {
                LootNotifyBoxChk();
            }
            else
            {
                UnLoad();
            }
        }
        public void UnLoad()
        {
            LootService.Instance.lootView.UnLoad();
            endTrans.gameObject.SetActive(false);
        }
        public void LootNotifyBoxChk()
        {
            NotifyName notifyName = NotifyName.LootTaken;
            notifyBoxView.OnShowNotifyBox(this, notifyName);
        }
    }
}