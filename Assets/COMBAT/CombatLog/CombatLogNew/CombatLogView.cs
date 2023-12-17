using Common;
using Spine.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Combat
{
    public class CombatLogView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] const float MIN_HT = 140f;
        [SerializeField] const float MAX_HT = 500f;


        [Header("Content and log Prefab TBR")]
        [SerializeField] GameObject containerCombatLog;

        [Header(" Combat Log Panel")]
        [SerializeField] GameObject logPanelPrefab;
        [Header("NTBR GO ")]
        [SerializeField] GameObject logPanelGO;
        public List<CombatLogData> combatLog = new List<CombatLogData>();

        [Header(" String Duplication Correction")]
        [SerializeField] string strPrev = "";

       
        void Start()
        {

            // TEMP TRAITS 
            //TempTraitService.Instance.OnTempTraitStart += PrintTempTraitStart;
            //TempTraitService.Instance.OnTempTraitEnd += PrintTempTraitEnd;



            //// PERMANENT TRAITS 
            //PermanentTraitsService.Instance.OnPermaTraitAdded += PrintPermaTraitAdded;
            // SKILL USED
       

            // COMBAT EVENT START EVENTS
            CombatEventService.Instance.OnSOC += StartOfCombat;  

            //CharService.Instance.OnCharDeath += DeathOfCharUpdate;
            CharStatesService.Instance.OnCharStateStart += CharStateStart;
            CharStatesService.Instance.OnCharStateEnd += CharStateEnd;
            CombatEventService.Instance.OnEOC += OnCombatEnd;
            CombatEventService.Instance.OnSOR1 += StartOfRound;
            CombatEventService.Instance.OnSOT += StartOfTurn;
            SkillService.Instance.OnSkillUsed += SkillUsed;
            CombatEventService.Instance.OnDodge += OnDodge;
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnSOC -= StartOfCombat;
            CombatEventService.Instance.OnEOC -= OnCombatEnd;
            //CharService.Instance.OnCharDeath -= DeathOfCharUpdate;
            CharStatesService.Instance.OnCharStateStart -= CharStateStart;
            CharStatesService.Instance.OnCharStateEnd -= CharStateEnd;

            CombatEventService.Instance.OnSOR1 -= StartOfRound;
            CombatEventService.Instance.OnSOT -= StartOfTurn;
            SkillService.Instance.OnSkillUsed -= SkillUsed;
        }

        void StartOfCombat()
        {
            CharService.Instance.allCharInCombat
                                     .ForEach(t => t.OnStatChg += HpChg);
            string str = "Combat Start!";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }
        void OnCombatEnd()
        {
            CharService.Instance.allCharInCombat
                               .ForEach(t => t.OnStatChg -= HpChg);
        }
        void StartOfTurn()
        {
            CharController charController = CombatService.Instance.currCharOnTurn;
            if (charController == null) return;
            string turnStr = charController.charModel.charNameStr + "'s Turn";

            if (strPrev.Contains(charController.charModel.charNameStr))
            {
                combatLog.RemoveAt(combatLog.Count-1);
                combatLog.Add(new CombatLogData(LogBackGround.LowHL, turnStr));
                combatLog.Add(new CombatLogData(LogBackGround.LowHL, strPrev));
            }
            else
            {
                combatLog.Add(new CombatLogData(LogBackGround.LowHL, turnStr));
            }            
            RefreshCombatLogUI();
        }

        #region  # COMBAT LOG BUILDERS 
        // **********CHAR STATE
        void CharStateStart(CharStateModData charStateModData)
        {
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(charStateModData.effectedCharID);
            CharNames charName = charController.charModel.charName;
            string str = charName + " is now " + charStateModData.charStateName;
            //+ charStateModData.castTime + " rds";

            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }
        void CharStateEnd(CharStateModData charStateModData)
        {
            string charNameStr = CharService.Instance.GetCharCtrlWithCharID(charStateModData.effectedCharID).charModel.charNameStr;
            string str = charNameStr + " state ENDS " + charStateModData.charStateName + "";

            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }

        void DeathOfCharUpdate(CharController charController)
        {
            string str = charController.charModel.charName + " Died";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }
        void SkillUsed(SkillEventData skillEventData)
        {
            string charNameStr = "";
            string str = ""; 
            if (skillEventData.skillModel.skillInclination == SkillInclination.Move)
            {
                charNameStr = skillEventData.strikerController.charModel.charNameStr;
                DynamicPosData dyna = GridService.Instance.GetDyna4GO(skillEventData.strikerController.gameObject);
                int currPos = dyna.currentPos; 
                str = $"{charNameStr} moves to hex {currPos}";
            }
            else
            {
                charNameStr = skillEventData.strikerController.charModel.charNameStr;
                string skillNameStr = skillEventData.skillModel.skillName.ToString().CreateSpace(); 
                str = $"{charNameStr} uses {skillNameStr}";
            }
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }




        //***********START ROUND **************//
        void StartOfRound(int roundNo)
        {
            string str = $"   # Round {roundNo} starts";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();

        }
        #endregion

        #region IN COMBAT EVENTS

        void OnDodge(DmgAppliedData dmgAppliedData)
        {
            string strikerName = dmgAppliedData.striker.charModel.charNameStr;
            string str = strikerName + " misses target";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }

        #endregion

        void RefreshCombatLogUI()
        {
            int k = containerCombatLog.transform.childCount; 
            if (combatLog.Count > k)
            {
                for (int j =0; j < k; j++)
                {
                    Vector3 pos = Vector3.zero;
                    logPanelGO = Instantiate(logPanelPrefab, pos, Quaternion.identity);
                    logPanelGO.transform.SetParent(containerCombatLog.transform);
                      logPanelGO.transform.localScale = Vector3.one;             
                }
            }
            int i = 0;
            foreach (Transform child in containerCombatLog.transform)
            {
                if(i < combatLog.Count)
                {
                    child.gameObject.SetActive(true);
                    if(strPrev != combatLog[i].logString)
                        child.GetComponentInChildren<TextMeshProUGUI>().text = combatLog[i].logString;
                    SetPanelColor(child.gameObject, combatLog[i].logBackGround);
                    strPrev = combatLog[i].logString; 
                }
                else
                {
                    child.gameObject.SetActive(false);
                }

                i++;
            }
            ResetToPos();
        }

        void ResetToPos()
        {
            ScrollRect scrollRect = transform.GetComponentInChildren<ScrollRect>();
            if (scrollRect != null)
            {
                scrollRect.verticalNormalizedPosition = 0f;
            }
        }
        void SetPanelColor(GameObject panel, LogBackGround _logBG)
        {
            panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            //switch (_logBG)
            //{
            //    case LogBackGround.None:
            //        panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            //        break;
            //    case LogBackGround.LowHL:
            //        panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.4f);

            //        break;
            //    case LogBackGround.MediumHL:
            //        panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6f);
            //        break;
            //    case LogBackGround.HighHL:
            //        panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            //        break;
            //    default:
            //        break;
            //}

        }
    
   
        void HpChg(StatModData statModData)
        {
            StatName statName = statModData.statModified;
            if (statName != StatName.health) return; 
            string str = "";
            CharNames CharName = CharService.Instance.GetCharCtrlWithCharID(statModData.effectedCharNameID)
                                    .charModel.charName;

            if (statModData.modVal == 0) return;

            //string sign = statModData.valChg > 0 ? "+" : "-";
            string str2 = statModData.valChg > 0 ? "gains" : "loses";
            str = $"{CharName} {str2} {Mathf.Abs((int)statModData.valChg)} {statModData.statModified}";

            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExpandLog();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.sizeDelta
                  = new Vector2(rect.sizeDelta.x, MIN_HT);
        }

        void ExpandLog()
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.sizeDelta
                  = new Vector2(rect.sizeDelta.x, MAX_HT);
            ResetToPos();
        }
    }

    public enum LogBackGround
    {
        None,
        LowHL,
        MediumHL,
        HighHL,
    }
    [Serializable]
    public class CombatLogData
    {
        public LogBackGround logBackGround;
        public string logString;

        public CombatLogData(LogBackGround logBackGround, string logString)
        {
            this.logBackGround = logBackGround;
            this.logString = logString;
        }
    }
}


//void PrintStatChgAdded(StatModData statModData)
//{
//    StatName statName = statModData.statModified;
//    string str = "";
//    CharNames CharName = CharService.Instance.GetCharCtrlWithCharID(statModData.effectedCharNameID)
//                            .charModel.charName;

//    if (statModData.modVal == 0) return;

//    if (statName == StatName.health || statName == StatName.stamina
//        || statName == StatName.fortitude)
//    {
//        string str1 = statModData.modVal > 0 ? "gains" : "loses";

//        str = CharName + " " + str1 + " " +
//                    +Mathf.Abs((int)statModData.modVal) + " " + statModData.statModified;
//    }
//}
//void DmgApplied(DmgAppliedData dmgAppliedData)
//{
//    CharNames charName = dmgAppliedData.striker.charModel.charName;
//    SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);

//    CharNames targetName = dmgAppliedData.targetController.charModel.charName;

//    SkillNames skillName = SkillNames.None;
//    if (dmgAppliedData.causeType == CauseType.CharSkill)
//        skillName = (SkillNames)dmgAppliedData.causeName;

//    AttackType attackType = skillDataSO.allSkills.Find(t => t.skillName == skillName).attackType;

//    DamageType damageType = dmgAppliedData.dmgType;

//    if (damageType == DamageType.Physical || damageType == DamageType.Air || damageType == DamageType.Water
//        || damageType == DamageType.Earth || damageType == DamageType.Fire || damageType == DamageType.Light
//        || damageType == DamageType.Dark || damageType == DamageType.FortitudeDmg || damageType == DamageType.StaminaDmg
//        || damageType == DamageType.StaminaDmg)
//    {
//        string str = charName + " " + attackType + " " + damageType + " attack on " + targetName + " with " + skillName;
//        combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
//        RefreshCombatLogUI();
//    }
//    else
//    {
//        string str = charName + " " + attackType + " " + damageType + " " + targetName + " with " + skillName;
//        combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
//        RefreshCombatLogUI();
//    } 
//}
