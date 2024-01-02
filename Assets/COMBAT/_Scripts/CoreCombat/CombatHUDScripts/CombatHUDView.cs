using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Common;
using System;
using DG.Tweening;


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

        #region Declarations

        [Header("Skill NAME ")]
        [SerializeField] TextMeshProUGUI skillNameTxt; 
        [Header("Scriptable objects")]
        public StatIconSO statIconSO;
        //public CharStateModelSO charStateIconSO;
      // public AllCharStateSO allStatesSO; 
        public TransitionSO transitionSO;

        [Header("Buff Display")]
        [SerializeField] int maxExpectedLines = 10;
        [SerializeField] float buffTransX = 250f;
        [SerializeField] float buffTransY = 250f;
        [SerializeField] Transform buffView;
        [SerializeField] Transform deBuffView;
        [SerializeField] float buffListHt;
        [SerializeField] List<string> buffList = new List<string>();
        [SerializeField] List<string> deBuffList = new List<string>();

        [Header("Action Buttons")]
        public Button attribPanelToggleBtn;

        [Header("Side Panels")]
        public GameObject AttributePanel;
        public GameObject CombatLogPanel;


        [Header("Top Panels")]
        public GameObject PortraitPanelAlly;
        public GameObject PortraitPanelEnemy;
        public GameObject roundDisplayGO; 

        [Header("Animation Panels")]
        public GameObject animPanel;


        [Header("MISC Refernces")]
        public GameObject charOnTurn;

        [Header("Round Controller")]
        RoundController roundController;
        TopPortraitsController topPortraitsController;

        [Header("Constants")]
        float animTime = 0.2f;


        public List<GameObject> statBars = new List<GameObject>();  // get Health,stamina, fortitude, hunger thrist bars in order
        public List<StatDisplayData> statDisplay = new List<StatDisplayData>();
        public GameObject CharStatesPanel;
        public List<GameObject> allcharStatesDisplay = new List<GameObject>();
        public Image charImg;
        public TextMeshProUGUI charName; 

        public GameObject StatesPanel;
        public StatPanelToggleState portraitToggleState;
        #endregion

        void Start()
        {
            roundController = GetComponent<RoundController>();
            if(gameObject.GetComponent<TopPortraitsController>() == null)
                topPortraitsController = gameObject.AddComponent<TopPortraitsController>();
            
           // UnityEditor.EditorUtility.SetDirty(statIconSO);
            portraitToggleState = StatPanelToggleState.None;
            //attribPanelToggleBtn.onClick.AddListener(OnAttributesPanelTogglePressed);
            //AttributePanelToggle();
            // CombatEventService.Instance.OnSOTactics += SetDefaultTurnOrder;
            //CombatEventService.Instance.OnSOR += SetCharOnTopPanel;

            // CombatEventService.Instance.OnCharClicked += SetBuffDebuffList;


            //CombatEventService.Instance.OnSOR += SetDefaultTurnOrder;

            //CombatEventService.Instance.OnSOT += CleanOnTurnGO; 

            CombatEventService.Instance.OnSOTactics += () => PlayTransitAnim("TACTICS"); 
            CombatEventService.Instance.OnSOC += () => PlayTransitAnim("COMBAT");
            CombatEventService.Instance.OnEOC += OnCombatEnd; 
            // CombatEventService.Instance.OnSOR += () => transitionSO.PlayAnims("ROUND " + CombatService.Instance.currentRound, animPanel);
            CombatEventService.Instance.OnSOR1 += RoundDisplay; 
           // CombatEventService.Instance.OnCombatLoot += CombatResultDisplay;

            //CombatEventService.Instance.OnCharOnTurnSet
            //                                              += SetCharAttributesDisplay;

            //CombatEventService.Instance.OnCharClicked += SetCharAttributesDisplay;

            // to be kept 
            CombatEventService.Instance.OnCharOnTurnSet += SetSelectCharPortrait;
            CombatEventService.Instance.OnCharClicked += SetSelectCharPortrait;
            SkillService.Instance.OnSkillUsed += DsplySkillName; 
            // CombatEventService.Instance.OnCharOnTurnSet += SetCombatStatesDisplay;
            // CombatEventService.Instance.OnCharClicked += SetCombatStatesDisplay;
            //// CharStatesPanelIconsClear();
            // CharStatesService.Instance.OnCharStateStart += UpdateCharStateChg;
            // CharStatesService.Instance.OnCharStateEnd += UpdateCharStateChg;

        }
        void DsplySkillName(SkillEventData skillEventData)
        {
            skillNameTxt.text = skillEventData.skillName.ToString().CreateSpace();   
        }
        void PlayTransitAnim(string  str)
        {
            transitionSO.PlayAnims(str, animPanel);
        }

        void OnCombatEnd()
        {
            Sequence seq = DOTween.Sequence();

            
            seq
                .AppendCallback(() => PlayTransitAnim("COMBAT ENDS"))
                .AppendInterval(1.5f)
                .AppendCallback(()=>combatEndView.InitCombatEndView())
                ;

            seq.Play(); 
        }
        void RoundDisplay(int roundNo)
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_normal)
            {
                transitionSO.PlayAnims("ROUND " + roundNo, animPanel);
                roundDisplayGO.GetComponent<TextMeshProUGUI>().text = roundNo.ToString();
            }
        }
 


        #region CharTOPPANEL
        //public void ShuffleCharOnTopPanel()
        //{  
        //    gameObject.GetComponent<TopPortraitsController>().ShufflePortraits();
        //}
        public void CleanPanelGO(List<GameObject> toBeCleanList)
        {
            for (int i = 0; i < toBeCleanList.Count; i++)
            {
               toBeCleanList[i].SetActive(false);
            }
        }

        //public void CleanOnTurnGO()
        //{
        //    int allyCount = roundController.allyTurnOrder.Count;
        //    int enemyCount = roundController.enemyTurnOrder.Count;

        //    for (int i = 0; i < allyCount+enemyCount; i++)
        //    {
        //       if(i < allyCount)
        //        {
        //            if (i >= CombatService.Instance.currentTurn)
        //            {
        //                allyInCombat[i].transform.GetChild(0).gameObject.SetActive(true);
        //            }
        //            else
        //            {
        //                allyInCombat[i].transform.GetChild(0).gameObject.SetActive(false);
        //            }

        //        } else if (i < allyCount + enemyCount)
        //        {
        //            if (i >= CombatService.Instance.currentTurn)
        //            {
        //                enemyInCombat[i-allyCount].transform.GetChild(0).gameObject.SetActive(true);
        //            }
        //            else
        //            {
        //                enemyInCombat[i-allyCount].transform.GetChild(0).gameObject.SetActive(false);
        //            }
        //        }
        //    }
      //  }

        #endregion

        #region CharPORTRAIT
        public void SetSelectCharPortrait(CharController charController)
        {
         
             //  Transform charOnTurnBtn = charOnTurn.transform; 
            CharMode charMode = charController.charModel.charMode;

            RectTransform portraitRectAlly = PortraitPanelAlly.GetComponent<RectTransform>();
            RectTransform portRectEnemy = PortraitPanelEnemy.GetComponent<RectTransform>();
           // Transform fortTrans = PortraitPanelAlly.transform.GetChild(3);
            Transform currPanel; 
            charOnTurn.SetActive(true);
            if (charMode == CharMode.Ally)
            {
                currPanel = PortraitPanelAlly.transform;            
                portRectEnemy.DOScaleY(0.0f, 0.4f);
                portraitRectAlly.DOScaleY(1.0f, 0.4f);

            }
            else
            {
                currPanel = portRectEnemy.transform;
                portRectEnemy.DOScaleY(1.0f, 0.4f);
                portraitRectAlly.DOScaleY(0.0f, 0.4f);
            }

            Image charImg = currPanel.transform.GetChild(0).GetComponent<Image>();
            charImg.sprite = charController.charModel.charSprite;
            // name 
            currPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = charController.charModel.charName.ToString();

            // bars
            Transform portraitBars = currPanel.transform.GetChild(2);
            Image HPBarImg = portraitBars.GetChild(0).GetChild(0).GetComponent<Image>();
            Image StaminaBarImg = portraitBars.GetChild(1).GetChild(0).GetComponent<Image>();

            StatData HPData = charController.GetStat(StatName.health);
            StatData StaminaData = charController.GetStat(StatName.stamina);

            // Change fill
            float HPbarValue = HPData.currValue / HPData.maxLimit;
            float staminaBarVal = StaminaData.currValue / StaminaData.maxLimit;
           // Debug.Log("HP Values " + HPbarValue + "Stamina" + staminaBarVal); 
            HPBarImg.fillAmount = HPbarValue;
            StaminaBarImg.fillAmount = staminaBarVal;

            if (charMode == CharMode.Ally)
            {
                // fortitude
                //StatData fortData = charController.GetStat(StatName.fortitude);
                //Transform fortCircleTrans = PortraitPanelAlly.transform.GetChild(3);

                //// Fortitude Update 
                //if (fortData.currValue >= 0)
                //{
                //    fortCircleTrans.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = fortData.currValue / fortData.maxLimit;
                //}
                //else if (fortData.currValue <= 0)
                //{
                //    fortCircleTrans.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = Mathf.Abs(fortData.currValue / fortData.maxLimit);
                //}
                //fortCircleTrans.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = fortData.currValue.ToString();
                //float fortOrg = charController.GetAttrib(AttribName.fortOrg).currValue; 

                //fortCircleTrans.DORotate(new Vector3(0, 0, -fortOrg * 6), 0.5f);
            }       
        }
        public void UpdateTurnBtn(CharController charController)
        {   
           charOnTurn.GetComponent<CharOnTurnBtn>().Check4DiffChar(charController);
        }
        #endregion

        #region CharAttributes

        //public void OnAttributesPanelTogglePressed()
        //{         
        //    //if (portraitToggleState == StatPanelToggleState.OnlyAttributes)
        //    //    portraitToggleState = StatPanelToggleState.None;
        //    //else
        //    //    portraitToggleState++;            
        //    //AttributePanelToggle(); 
        //}

        //public void AttributePanelToggle()
        //{
        //    RectTransform statPanelRect = AttributePanel.GetComponent<RectTransform>();
        //    RectTransform combatPanelRect = CombatLogPanel.GetComponent<RectTransform>();
           
        //    switch (portraitToggleState)
        //    {
        //        case StatPanelToggleState.None:
        //            statPanelRect.DOScaleX(0, animTime);
        //            combatPanelRect.DOScaleY(0, animTime);

        //            break;
        //        case StatPanelToggleState.OnlyLog:
        //            combatPanelRect.DOScaleY(1, animTime);
        //            break;
        //        case StatPanelToggleState.LogPlusAttributes:
        //            statPanelRect.DOScaleX(1, animTime);

        //            break;
        //        case StatPanelToggleState.OnlyAttributes:
        //            combatPanelRect.DOScaleY(0, animTime);
        //            break;
        //        default:
        //            statPanelRect.DOScaleX(0, animTime);
        //            combatPanelRect.DOScaleY(0, animTime);
        //            break;
        //    }


        //}

        //public void SetCharAttributesDisplay(CharController charController)
        //{

        //    //for (int i = 1; i <= 16; i++)    // 6-21  // 0 in list "none"      // ICON STATS            
        //    //{

        //    //    int index = i;
        //    //    SpriteData spriteData = null;

        //    //    int j = statIconSO.allSpriteData.FindIndex(x => x.statName == (AttribName)index);
        //    //    if (j != -1)
        //    //        spriteData = statIconSO.allSpriteData[j];
        //    //    else
        //    //    {
        //    //        Debug.LogError(" Attrib data missoing" + (AttribName)index);
        //    //    }


        //    //    // img from SO 
        //    //    AttribData attribData = charController.GetAttrib((AttribName)index);  // current stats from ctrller               

        //    //    StatDisplayData statDisplayData = statDisplay.Find(x => x.statName == (AttribName)index); // reference list
        //    //    statDisplayData.statDisplayGO.GetComponentInChildren<Image>().sprite = spriteData.statSprite;

        //    //    PopUpAndHL popUpAndHL = statDisplayData.statDisplayGO.GetComponent<PopUpAndHL>();
        //    //    popUpAndHL.spriteNormal = spriteData.statSprite;
        //    //    popUpAndHL.spriteLit = spriteData.statSpriteLit;
        //    //    popUpAndHL.statName = (AttribName)index;


        //    //    popUpAndHL.desc = attribData.desc;
        //    //    string statStr;
        //    //    if (((AttribName)index).IsAttribDamage())
        //    //    {
        //    //        float dmgMin = charController.GetAttrib(AttribName.dmgMin).currValue;
        //    //        float dmgMax = charController.GetAttrib(AttribName.dmgMax).currValue;

        //    //        statStr = dmgMin + "-" + dmgMax;
        //    //    }
        //    //    else if (((AttribName)index).IsAttribArmor())
        //    //    {
        //    //        float armorMin = charController.GetAttrib(AttribName.armorMin).currValue;
        //    //        float armorMax = charController.GetAttrib(AttribName.armorMax).currValue;

        //    //        statStr = armorMin + "-" + armorMax;
        //    //    }
        //    //    else
        //    //    {
        //    //        float val = attribData.currValue;
        //    //        statStr = val.ToString();
        //    //    }
        //    //    statDisplayData.statDisplayGO.GetComponentInChildren<TextMeshProUGUI>().text = statStr;
        //    //    statDisplayData.statDisplayGO.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;


        //    //}
        //}

        //public void SetBuffDebuffList(CharController charController)
        //{
        //    buffList.Clear(); deBuffList.Clear();
        //    if (statDisplay.Count == 0) return; 
            
        //        buffView = statDisplay.Find(x => x.isBuff == true).statDisplayGO.transform.GetChild(0);

        //        deBuffView = statDisplay.Find(x => x.isDebuff == true).statDisplayGO.transform.GetChild(0);
            
        
        //    buffListHt = buffView.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.height;
        //    Transform buffContent = buffView.GetChild(0).GetChild(0);
        //    buffContent.GetComponent<RectTransform>().sizeDelta
        //                                    = new Vector2(buffTransX, buffListHt);


        //    buffList = charController.gameObject.GetComponent<BuffController>().GetBuffList();
        //    deBuffList = charController.gameObject.GetComponent<BuffController>().GetDeBuffList();


        //    int lines = buffList.Count;
        //    Debug.Log("BUFF LIST LINES" + lines);
        //    if (lines > maxExpectedLines)
        //    {
        //        int incr = lines - maxExpectedLines;
        //        buffContent.GetComponent<RectTransform>().sizeDelta
        //            = new Vector2(buffTransX, buffListHt + incr * 40f);
        //    }
        //    else
        //    {
        //        buffContent.GetComponent<RectTransform>().sizeDelta
        //          = new Vector2(buffTransX, buffListHt);
        //    }


        //    for (int i = 0; i < lines; i++)
        //    {
        //        if (i < buffContent.childCount)
        //        {
        //            buffContent.GetChild(i).gameObject.SetActive(true);
        //            buffContent.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text
        //                                                                          = buffList[i];
        //        }
        //        else
        //        {
        //            Debug.Log("MORE LINES NEED TO BE ADDED ");
        //        }
        //    }
        //}

        //public void ToggleBuffDebuff(bool isBuff, bool isDebuff)
        //{
        //    SetBuffDebuffList(CombatService.Instance.currCharClicked);
        //    if (isBuff)
        //    {
        //        buffView.DOScaleX(1, animTime);
        //      //  deBuffView.DOScaleX(0, animTime);
        //    }
        //    else if(isDebuff) 
        //    {
        //        buffView.DOScaleX(0, animTime);
        //        //deBuffView.DOScaleX(1, animTime);
        //    }
        //    else if(!isBuff && !isDebuff)
        //    {
        //        buffView.DOScaleX(0, animTime);
        //        deBuffView.DOScaleX(0, animTime);

        //    }
        //}
        #endregion
    }

}