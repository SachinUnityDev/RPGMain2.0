using Common;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Combat
{
    public class CombatLogView : MonoBehaviour
    {

        [Header("Content and log Prefab TBR")]
        [SerializeField] GameObject containerCombatLog;

        [Header(" Combat Log Panel")]
        [SerializeField] GameObject logPanelPrefab;
        [Header("NTBR GO ")]
        [SerializeField] GameObject logPanelGO;
        public List<CombatLogData> combatLog = new List<CombatLogData>();
        void Start()
        {

            // TEMP TRAITS 
            //TempTraitService.Instance.OnTempTraitStart += PrintTempTraitStart;
            //TempTraitService.Instance.OnTempTraitEnd += PrintTempTraitEnd;



            //// PERMANENT TRAITS 
            //PermanentTraitsService.Instance.OnPermaTraitAdded += PrintPermaTraitAdded;
            // SKILL USED
            SkillService.Instance.OnSkillUsed += SkillUsed; 

            // COMBAT EVENT START EVENTS
            CombatEventService.Instance.OnSOC += StartOfCombat;  

            CharService.Instance.OnCharDeath += DeathOfCharUpdate;
            CharStatesService.Instance.OnCharStateStart += CharStateStart;
            CharStatesService.Instance.OnCharStateEnd += CharStateEnd;
            CombatEventService.Instance.OnEOC += OnCombatEnd;
            CombatEventService.Instance.OnSOR1 += StartOfRound;
            CombatEventService.Instance.OnCharOnTurnSet += StartOfTurn;

            CharService.Instance.charsInPlayControllers
                                     .ForEach(t => t.OnStatChg += HpChg);


        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnSOC -= StartOfCombat;
            CombatEventService.Instance.OnEOC -= OnCombatEnd;
            CharService.Instance.OnCharDeath -= DeathOfCharUpdate;
            CharStatesService.Instance.OnCharStateStart -= CharStateStart;
            CharStatesService.Instance.OnCharStateEnd -= CharStateEnd;

            SkillService.Instance.OnSkillUsed -= SkillUsed;
            CombatEventService.Instance.OnSOR1 -= StartOfRound;
            CombatEventService.Instance.OnCharOnTurnSet -= StartOfTurn;

            CharService.Instance.charsInPlayControllers
                                .ForEach(t => t.OnStatChg -= HpChg);
        }

        void StartOfCombat()
        {
            string str = "Combat Start!";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }
        void OnCombatEnd()
        {

        }
        void StartOfTurn(CharController charController)
        {
            string charNameStr = charController.charModel.charNameStr;
            string str = charNameStr + "'s Turn";

            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }

        #region  # COMBAT LOG BUILDERS 
        // **********CHAR STATE
        void CharStateStart(CharStateModData charStateModData)
        {
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(charStateModData.effectedCharID);
            CharNames charName = charController.charModel.charName;
            string str = charName + " is now " + charStateModData.charStateName + ", ";
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
                charNameStr = skillEventData.targetController.charModel.charNameStr;
                DynamicPosData dyna = GridService.Instance.GetDyna4GO(skillEventData.targetController.gameObject);
                int currPos = dyna.currentPos; 
                str = charNameStr + " moves to "+ currPos;
            }
            else
            {
                charNameStr = skillEventData.strikerController.charModel.charNameStr;
                str = charNameStr + " uses " + skillEventData.skillModel.skillName.ToString().CreateSpace() + "";
            }
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }




        //***********START ROUND **************//
        void StartOfRound(int roundNo)
        {
            string str = "Round #" + roundNo + "Starts";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();

        }
        #endregion


        void RefreshCombatLogUI()
        {
            int i = 0;
            foreach (Transform child in containerCombatLog.transform)
            {
                child.GetComponentInChildren<TextMeshProUGUI>().text = combatLog[i].logString;
                SetPanelColor(child.gameObject, combatLog[i].logBackGround);
                i++;
            }
            if (combatLog.Count > i)
            {
                for (int j = i - 1; j < combatLog.Count - i; j++)
                {
                    Vector3 pos = Vector3.zero;
                    logPanelGO = Instantiate(logPanelPrefab, pos, Quaternion.identity);
                    logPanelGO.transform.SetParent(containerCombatLog.transform);
                    logPanelGO.transform.localScale = Vector3.one;
                    logPanelGO.GetComponentInChildren<TextMeshProUGUI>().text
                                                        = combatLog[j].logString;
                    SetPanelColor(logPanelGO, combatLog[j].logBackGround);
                }
            }
        }

        void SetPanelColor(GameObject panel, LogBackGround _logBG)
        {

            switch (_logBG)
            {
                case LogBackGround.None:
                    panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                    break;
                case LogBackGround.LowHL:
                    panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.4f);

                    break;
                case LogBackGround.MediumHL:
                    panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6f);
                    break;
                case LogBackGround.HighHL:
                    panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                    break;
                default:
                    break;
            }

        }
    
        void DmgApplied(DmgAppliedData dmgAppliedData)
        {
            CharNames charName = dmgAppliedData.striker.charModel.charName;
            SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);

            CharNames targetName = dmgAppliedData.targetController.charModel.charName;

            SkillNames skillName = SkillNames.None;
            if (dmgAppliedData.causeType == CauseType.CharSkill)
                skillName = (SkillNames)dmgAppliedData.causeName;

            AttackType attackType = skillDataSO.allSkills.Find(t => t.skillName == skillName).attackType;

            DamageType damageType = dmgAppliedData.dmgType;

            if (damageType == DamageType.Physical || damageType == DamageType.Air || damageType == DamageType.Water
                || damageType == DamageType.Earth || damageType == DamageType.Fire || damageType == DamageType.Light
                || damageType == DamageType.Dark || damageType == DamageType.FortitudeDmg || damageType == DamageType.StaminaDmg
                || damageType == DamageType.StaminaDmg)
            {
                string str = charName + " " + attackType + " " + damageType + " attack on " + targetName + " with " + skillName;
                combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
                RefreshCombatLogUI();
            }
            else
            {
                string str = charName + " " + attackType + " " + damageType + " " + targetName + " with " + skillName;
                combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
                RefreshCombatLogUI();
            } 
        }

        void HpChg(StatModData statModData)
        {

            StatName statName = statModData.statModified;
            if (statName != StatName.health) return; 
            string str = "";
            CharNames CharName = CharService.Instance.GetCharCtrlWithCharID(statModData.effectedCharNameID)
                                    .charModel.charName;

            if (statModData.modVal == 0) return;

            string sign = statModData.modVal > 0 ? "+" : "-";
            string str2 = statModData.modVal > 0 ? "gains" : "suffers";
            str = CharName + " " + str2 + " " + sign +
                      +Mathf.Abs((int)statModData.modVal) + " " + statModData.statModified;

            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
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
    }
}