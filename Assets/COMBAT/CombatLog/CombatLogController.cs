using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using TMPro;


namespace Combat
{
    public enum LogBackGround
    {
        None, 
        LowHL, 
        MediumHL, 
        HighHL, 
    }
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
    // reference the log list container in this scripts 
    // revamp log events with all the current data stream events 
    // On hover plank to be revamped too.. with simple highlighting  
    // remove all old complications

    // color plank .. add on hover button
    // check on verbose for the combat log ... 
    // change on damage method and function 




    public class CombatLogController : MonoBehaviour
    {
        [Header("Content and log Prefab")]
        [SerializeField] GameObject containerCombatLog;
        [SerializeField] GameObject logPanel;
        [SerializeField] GameObject logPanelGO; 
        public List<CombatLogData> combatLog = new List<CombatLogData>();
        void Start()
        {
          
            // TEMP TRAITS 
            TempTraitService.Instance.OnTempTraitStart += PrintTempTraitStart;
            TempTraitService.Instance.OnTempTraitEnd += PrintTempTraitEnd;

            // PERMANENT TRAITS 
            PermanentTraitsService.Instance.OnPermaTraitAdded += PrintPermaTraitAdded;
       
            // COMBAT EVENT START EVENTS
            CombatEventService.Instance.OnSOC += StartOfCombat;
            CombatEventService.Instance.OnSOR += StartOfRound;
           // CombatEventService.Instance.OnCharOnTurnSet += StartOfTurn;
            CombatEventService.Instance.OnCharDeath += DeathOfCharUpdate;
            CharStatesService.Instance.OnCharStateStart += CharStateStart;
            CharStatesService.Instance.OnCharStateEnd += CharStateEnd;

            // TO BE CAPTURED 
            // move events push and pull 
           

        }
#region  # COMBAT LOG BUILDERS 
        // **********CHAR STATE
        void CharStateStart(CharStateData charStateData)
        {
            CharNames charName = charStateData.charStateModel.effectedCharID;

            string str = charName + " is now " + charStateData.charStateModel.charStateName +", "
                                 + charStateData.charStateModel.castTime + " rds";
            
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();

        }
        void CharStateEnd(CharStateData charStateData)
        {
            string str = charStateData.charStateModel.effectedChar + " state ENDS " + charStateData.charStateModel.charStateName + ", "
                + charStateData.charStateModel.castTime + " rds";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();

        }


        // ********* CHAR DIED 

        void DeathOfCharUpdate(CharController charController)
        {
            string str = charController.charModel.charName + " Died";
            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }
        //***********START IF COMBAT **************//
        void StartOfCombat()
        {           
            CharService.Instance.charsInPlayControllers
                        .ForEach(t => t.OnStatChg += PrintStatChanged);         
            CharService.Instance.charsInPlayControllers
                        .ForEach(t => t.damageController.OnDamageApplied += PrintDamageApplied); 

            string str = "Combat Start!";
            combatLog.Add(new CombatLogData(LogBackGround.HighHL, str));
            RefreshCombatLogUI();
        }
        //***********START ROUND **************//
        void StartOfRound()
        {
            int roundNo = CombatService.Instance.currentRound; 
            string str = "Round #" + roundNo +"Starts";
            combatLog.Add(new CombatLogData(LogBackGround.HighHL, str));
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
                for (int j = i-1; j < combatLog.Count-i; j++)
                {
                    Vector3 pos = Vector3.zero;
                    logPanelGO = Instantiate(logPanel, pos, Quaternion.identity);
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
        
        void PrintDamageApplied(DmgAppliedData dmgAppliedData)
        {
            CharNames charName = dmgAppliedData.striker.charModel.charName;
            SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);

            CharNames targetName = dmgAppliedData.targetController.charModel.charName; 

            SkillNames skillName = SkillNames.None; 
            if(dmgAppliedData.causeType == CauseType.CharSkill)
                 skillName =(SkillNames)dmgAppliedData.causeName;

            AttackType attackType = skillDataSO.allSkills.Find(t => t.skillName == skillName).attackType; 

            DamageType damageType = dmgAppliedData.dmgType; 

            if(damageType == DamageType.Physical || damageType == DamageType.Air || damageType == DamageType.Water
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

            // Rayyan ranged magical attack on Dire Rat, Dire Rat with Tidal Waves
            // get <AttackType> in Skill Service ... 
            // if charName == EnemyName / target name => "Self"
            // <CharName><AttackType> <DamageType0>  "attack" on <Enemy Name>, <Enemy Name> with <SkillName>

            // use word "ATTACK " Physical, magical(air, water bla bla ) , pure , stamina, fortitude             
        }

        void PrintStatChanged(CharModData charModData)
        {
           
            StatsName statName = charModData.statModified;
            string str = "";
            CharNames CharName = CharService.Instance.GetCharCtrlWithCharID(charModData.effectedCharNameID)
                                    .charModel.charName;


            if (charModData.modCurrVal == 0) return;                                                                               
            if (statName == StatsName.health || statName == StatsName.stamina 
                || statName == StatsName.fortitude)
            {
                string str1 = charModData.modCurrVal > 0 ? "gains" : "loses";
               
                str = CharName + " " + str1 +" " +
                            + Mathf.Abs((int)charModData.modCurrVal) + " " + charModData.statModified;
            }
            else
            {
                string sign = charModData.modCurrVal > 0 ? "+" : "-";
                string str2 = charModData.modCurrVal > 0 ? "gains" : "suffers";
                str = CharName + " " + str2 + " " + sign +
                          + Mathf.Abs((int)charModData.modCurrVal) + " " + charModData.statModified;
            }

            combatLog.Add(new CombatLogData(LogBackGround.LowHL, str));
            RefreshCombatLogUI();
        }

        void PrintPermaTraitAdded(PermaTraitData permaTraitData)
        {

        }
        void PrintTempTraitEnd(TempTraitData tempTraitData)
        {

        }
        void PrintTempTraitStart(TempTraitData tempTraitData)
        {

        }


    }




}

