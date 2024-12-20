using Combat;
using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        public bool highMeritExpBtnPressed = false; 
        public bool highMeritExpRewarded = false;

        CombatEndCondition combatEndCondition;
        Result combatResult;
        public CharController firstBloodChar;

        public NotifyName notifyName { get; set; }
        public bool isDontShowItAgainTicked { get ; set; }

        [SerializeField] Transform endTrans;

        private void OnEnable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
            SceneManager.activeSceneChanged += OnActiveSceneChg;
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
        }

        void OnActiveSceneChg(Scene curr, Scene next)
        {
            if (next.name == "COMBAT")
            {
                charPortContainer = FindObjectOfType<CombatEndPort>(true).transform;                
            }
        }

        public void InitCombatEndView()
        {
            this.combatResult= CombatEventService.Instance.currCombatResult;    
  
            allAllyInclDeadNFled = CharService.Instance
                                 .allCharsInPartyLocked.Where(t => t.charModel.orgCharMode == CharMode.Ally).ToList();
            combatEndContinueBtn.InitContinueBtn(this); 
            manualExpBtn.ManualExpBtnInit(this);
            try
            {
                FillCharPort();
            }catch(System.Exception e)
            {
                Debug.Log("EXCEPTION!!1" +e.Message); 
                Debug.Log("EXCEPTION!!1" + e.StackTrace);
                Debug.Log("EXCEPTION!!1" + e.TargetSite);
            }
            
            FillHeading();
            
            if (combatResult == Result.Victory)
            {
                endTrans = transform.GetChild(1);
                Transform lootTrans = transform.GetChild(0); 
                endTrans.SetAsFirstSibling();
                lootTrans.SetAsLastSibling();
                lootTrans.gameObject.SetActive(true);
                endTrans.gameObject.SetActive(true);
            }
            else if(combatResult == Result.Draw || combatResult == Result.Defeat)
            {
                endTrans = transform.GetChild(0);
                endTrans.gameObject.SetActive(true);                
            }
            notifyBoxView.Init();
        }
        public void SetCharAsFirstBlood(CharController charController)
        {
            firstBloodChar = charController;
        }
        public void OnManualExpAwarded()
        {
            manualExpBtn.StateNA(); 
            highMeritExpRewarded = true; 
        }
      
        void FillHeading()
        {
            switch (combatResult)
            {
                case Result.None:
                    break;
                case Result.Victory:
                    headingTxt.text = "VICTORY"; 
                    break;
                case Result.Draw:
                    headingTxt.text = "DRAW";
                    break;
                case Result.Defeat:
                    headingTxt.text = "DEFEAT";
                    break;
                default:
                    break;
            }
        }
        void FillCharPort()
        {
            float charSharedExp = CombatService.Instance.GetSharedExp();
            for (int i = 0; i < charPortContainer.childCount; i++)
            {
                if (i < allAllyInclDeadNFled.Count)
                {
                    charPortContainer.GetChild(i).gameObject.SetActive(true);
                    charPortContainer.GetChild(i).GetComponent<PortView>()
                                    .InitPortView(allAllyInclDeadNFled[i].charModel, (int)charSharedExp, this); 
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

                CombatEventService.Instance.OnCombatEndClicked(); 
                UnLoad();
            }
        }
        public void UnLoad()
        {
            endTrans.gameObject.SetActive(false);
        }
        public void LootNotifyBoxChk()
        {
            NotifyName notifyName = NotifyName.LootTaken;
            notifyBoxView.OnShowNotifyBox(this, notifyName);
        }
    }
}