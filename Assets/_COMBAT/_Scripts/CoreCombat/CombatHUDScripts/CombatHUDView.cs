using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Common;
using System;
using DG.Tweening;
using Quest;
using UnityEngine.SceneManagement;

namespace Combat
{
    [System.Serializable]
    public class StatDisplayData
    {
        public AttribName statName;     
        public GameObject statDisplayGO;
        public bool isBuff = false;
        public bool isDebuff = false; 
    }

    public enum StatPanelToggleState
    {
        //Click 1 - Only Combat log
        //Click 2 - Combat log + Attributes
        //Click 3 - Only Attributes
        //Click 4 - Nothing
        None,
        OnlyLog,
        LogPlusAttributes,
        OnlyAttributes,
    }

    public class CombatHUDView : MonoBehaviour
    {

        [Header("In Use")]
        [SerializeField] CombatEndView combatEndView;
        [SerializeField] Transform combatOpts; 
        #region Declarations

        [Header("Skill NAME ")]
        [SerializeField] TextMeshProUGUI skillNameTxt; 
        [Header("Scriptable objects")]
        public StatIconSO statIconSO;
 
        public TransitionSO transitionSO;

        [Header("Combat BG Dsply")]
        [SerializeField] SpriteRenderer combatBG; 
        [Header("Animation Panels")]
        public GameObject animPanel;


        [Header("MISC Refernces")]
        public GameObject charOnTurn;

        public List<GameObject> statBars = new List<GameObject>();  // get Health,stamina, fortitude, hunger thrist bars in order
        public List<StatDisplayData> statDisplay = new List<StatDisplayData>();
        public GameObject CharStatesPanel;
        public List<GameObject> allcharStatesDisplay = new List<GameObject>();
        //public Image charImg;
        //public TextMeshProUGUI charName; 

       // public GameObject StatesPanel;
        public StatPanelToggleState portraitToggleState;
        #endregion

        void OnEnable()
        {
            GetRef(); 
            portraitToggleState = StatPanelToggleState.None;
            CombatEventService.Instance.OnSOTactics += PlaySOTacticsAnim;
            CombatEventService.Instance.OnSOC += PlayCombatStartAnim;
            CombatEventService.Instance.OnSOR1 += PlaySORAnim;

            CombatEventService.Instance.OnEOC += OnCombatEnd;
            SkillService.Instance.OnSkillUsed += DsplySkillName;
            CombatEventService.Instance.OnCharClicked += ToggleCombatOpts;
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
            SceneManager.activeSceneChanged += OnActiveSceneChg;

        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnSOTactics -= PlaySOTacticsAnim;
            CombatEventService.Instance.OnSOC -= PlayCombatStartAnim; 
            CombatEventService.Instance.OnSOR1 -= PlaySORAnim; 
            CombatEventService.Instance.OnEOC -= OnCombatEnd;
            SkillService.Instance.OnSkillUsed -= DsplySkillName;
            CombatEventService.Instance.OnCharClicked -= ToggleCombatOpts;
            SceneManager.activeSceneChanged -= OnActiveSceneChg;

        }

        void OnActiveSceneChg(Scene curr, Scene next)
        {
            if(next.name =="COMBAT")
            {
                GetRef();
          
            }
        }


        void GetRef()
        {
            Canvas canvas = FindObjectOfType<Canvas>();

            combatEndView = FindObjectOfType<CombatEndView>();
            combatOpts = FindObjectOfType<CombatOptsParent>(true).transform;
            
            combatBG= FindObjectOfType<CombatBgSprite>().transform.GetComponent<SpriteRenderer>();
            animPanel = FindObjectOfType<AnimPanelView>().gameObject;    
            charOnTurn = FindObjectOfType<CharOnTurnBtn>(true).gameObject;
            
            CharStatesView charStates = FindObjectOfType<CharStatesView>();
            CharStatesPanel = charStates.gameObject;
            charStates.CharStatesPanelIconsClear(); 

        }
        public void SetCombatBG(LandscapeNames landscape)
        {
            LandscapeSO landSO = LandscapeService.Instance.allLandSO.GetLandSO(landscape);
            if (CalendarService.Instance.calendarModel.currtimeState == TimeState.Day)
                combatBG.sprite = landSO.spriteBG_C_day;
            else if (CalendarService.Instance.calendarModel.currtimeState == TimeState.Night)
                combatBG.sprite = landSO.spriteBG_C_night;
        }
        void DsplySkillName(SkillEventData skillEventData)
        {
            skillNameTxt.text = skillEventData.skillName.ToString().CreateSpace();   
        }

        void PlaySOTacticsAnim()
        {
            PlayTransitAnim("TACTICS");
        }
        void PlayCombatStartAnim()
        {
            PlayTransitAnim("COMBAT");
        }
        void PlaySORAnim(int rd)
        {    
            PlayTransitAnim($"ROUND {rd}");
        }

        void PlayTransitAnim(string  str)
        {
            transitionSO.PlayAnims(str, animPanel);
        }

        void OnCombatEnd()
        {   
            Sequence endSeq = DOTween.Sequence();
            endSeq
                .AppendCallback(() => PlayTransitAnim("COMBAT ENDS"))
                .AppendInterval(2.5f)
                //.AppendCallback(()=>combatEndView.InitCombatEndView())
                ;

            endSeq.Play().OnComplete(() => ShowCombatEndView());  
        }

        void ShowCombatEndView()
        {
            DOTween.KillAll();
            combatEndView.InitCombatEndView(); 
        }
        public void CleanPanelGO(List<GameObject> toBeCleanList)
        {
            for (int i = 0; i < toBeCleanList.Count; i++)
            {
               toBeCleanList[i].SetActive(false);
            }
        }
        #region CharPORTRAIT
        public void SetSelectCharPortrait(CharController charController)
        {
         
           //  //  Transform charOnTurnBtn = charOnTurn.transform; 
           // CharMode charMode = charController.charModel.charMode;

           // RectTransform portraitRectAlly = PortraitPanelAlly.GetComponent<RectTransform>();
           // RectTransform portRectEnemy = PortraitPanelEnemy.GetComponent<RectTransform>();
           //// Transform fortTrans = PortraitPanelAlly.transform.GetChild(3);
           // Transform currPanel; 
           // charOnTurn.SetActive(true);
           // if (charMode == CharMode.Ally)
           // {
           //     currPanel = PortraitPanelAlly.transform;            
           //     portRectEnemy.DOScaleY(0.0f, 0.4f);
           //     portraitRectAlly.DOScaleY(1.0f, 0.4f);

           // }
           // else
           // {
           //     currPanel = portRectEnemy.transform;
           //     portRectEnemy.DOScaleY(1.0f, 0.4f);
           //     portraitRectAlly.DOScaleY(0.0f, 0.4f);
           // }

           // Image charImg = currPanel.transform.GetChild(0).GetComponent<Image>();
           // charImg.sprite = charController.charModel.charSprite;
           // // name 
           // currPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text
           //     = charController.charModel.charName.ToString();

           // // bars
           // Transform portraitBars = currPanel.transform.GetChild(2);
           // Image HPBarImg = portraitBars.GetChild(0).GetChild(0).GetComponent<Image>();
           // Image StaminaBarImg = portraitBars.GetChild(1).GetChild(0).GetComponent<Image>();

           // StatData HPData = charController.GetStat(StatName.health);
           // StatData StaminaData = charController.GetStat(StatName.stamina);

           // // Change fill
           // float HPbarValue = HPData.currValue / HPData.maxLimit;
           // float staminaBarVal = StaminaData.currValue / StaminaData.maxLimit;
           //// Debug.Log("HP Values " + HPbarValue + "Stamina" + staminaBarVal); 
           // HPBarImg.fillAmount = HPbarValue;
           // StaminaBarImg.fillAmount = staminaBarVal;

           // if (charMode == CharMode.Ally)
           // {
           //     // fortitude
           //     //StatData fortData = charController.GetStat(StatName.fortitude);
           //     //Transform fortCircleTrans = PortraitPanelAlly.transform.GetChild(3);

           //     //// Fortitude Update 
           //     //if (fortData.currValue >= 0)
           //     //{
           //     //    fortCircleTrans.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = fortData.currValue / fortData.maxLimit;
           //     //}
           //     //else if (fortData.currValue <= 0)
           //     //{
           //     //    fortCircleTrans.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = Mathf.Abs(fortData.currValue / fortData.maxLimit);
           //     //}
           //     //fortCircleTrans.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = fortData.currValue.ToString();
           //     //float fortOrg = charController.GetAttrib(AttribName.fortOrg).currValue; 

           //     //fortCircleTrans.DORotate(new Vector3(0, 0, -fortOrg * 6), 0.5f);
           // }       
        }
        public void UpdateTurnBtn(CharController charController)
        {   
           charOnTurn.GetComponent<CharOnTurnBtn>().Check4DiffChar(charController);
        }
        #endregion

#region COMBAT OPTIONS

        void ToggleCombatOpts(CharController c)
        {
            if(CombatService.Instance.combatState == CombatState.INTactics)
            {
                combatOpts.gameObject.SetActive(false);
            }
            else
            {
                combatOpts.gameObject.SetActive(true);
            }
        }

#endregion


    }

}
//void RoundDisplay(int roundNo)
//{
//    if (CombatService.Instance.combatState == CombatState.INCombat_normal)
//    {
//        transitionSO.PlayAnims("ROUND " + roundNo, animPanel);
//        roundDisplayGO.GetComponent<TextMeshProUGUI>().text = roundNo.ToString();
//    }
//}
