using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Combat;
using DG.Tweening;
using Interactables;
using Quest;
using Town;
using System.Linq;
using Spine.Unity;
using TMPro;
using UnityEngine.UIElements;


namespace Common
{   

    public class CharController : MonoBehaviour
    {
        public event Action<int,CharNames> OnCharSpawned;  // charID only to be broadcasted

        public event Action<StatModData> OnStatChg; 
        public event Action<AttribModData> OnAttribChg; // return the current modified value 
        public event Action<AttribModData> OnAttribCurrValSet;  // curr values 
        public event Action<AttribModData> OnAttribBaseValChg;
        public event Action<float> OnExpGainedOrLoss;

        public CharModel charModel;     

        [Header("Combat Controllers")]
        public DamageController damageController;   
        public StrikeController strikeController;
        public CombatController combatController; 

        [Header("Buff Controller")]
        public BuffController buffController;
        public CharTypeBuffController charTypeBuffController;
        public TimeBuffController timeBuffController;
        public StatBuffController statBuffController;

        [Header("States and traits controller")]
        public CharStateController charStateController;
        public TempTraitController tempTraitController;
        public PermaTraitController permaTraitController;


        [Header("Item Controller")]
        public ItemController  itemController;

        [Header("LanscapeController")]
        public LandscapeController landscapeController;

        [Header("SkillController")]
        public SkillController1 skillController;

        [Header("Weapon Controller")]
        public WeaponController weaponController;

        [Header("Armor Controller")]
        public ArmorController armorController;

        [Header("Flee Controller")]
        public FleeController fleeController;

        float prevHPVal = 0f;
        float prevStaminaVal = 0f;
        [Header("for shader switch")]
        [SerializeField] Shader shaderN;
        [SerializeField] Shader shaderOnHover;
        [SerializeField] bool isCharHovered = false;
        [SerializeField] float prevTime = 0f;
        [SerializeField] CharacterSO charSO; 

        private void OnEnable()
        {
            shaderN = Shader.Find("Spine/Skeleton");
          //  shaderN = Shader.Find("Shader Graphs/ShaderGraph_Particles_Lit");
            shaderOnHover = Shader.Find("Spine/Special/Skeleton Grayscale");
            //shaderOnHover = Shader.Find("Spine/Blend Modes/Skeleton PMA Screen");
            isCharHovered = false; 
            
        }
        private void Start()
        {
            CombatEventService.Instance.OnSOC += FortReset2FortOrg;
            CombatEventService.Instance.OnEOC += FortReset2FortOrg;
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnSOC -= FortReset2FortOrg;
            CombatEventService.Instance.OnEOC -= FortReset2FortOrg;
        }
        public CharModel InitiatizeController(CharacterSO _charSO)
        {
            charSO = _charSO;
            if (SaveService.Instance.slotSelected == SaveSlot.AutoSave)
            {
                charModel = new CharModel(_charSO);
                if(charModel.orgCharMode == CharMode.Ally)
                {
                    charModel.charID = CharService.Instance.allCharModels.Count + 1; 
                }    
            }
            else
            {
                charModel = CharService.Instance.LoadCharModel(_charSO.charName); 
            }

            OnCharSpawned?.Invoke(charModel.charID, charModel.charName);
            AddController_OnCharSpawn(); 
            return charModel; 
        }
        void AddController_OnCharSpawn()
        {
            buffController = gameObject.AddComponent<BuffController>();
            timeBuffController = gameObject.AddComponent<TimeBuffController>();
            if (charModel.orgCharMode == CharMode.Ally)
            {
                charTypeBuffController = gameObject.AddComponent<CharTypeBuffController>();
                itemController = gameObject.AddComponent<ItemController>();
                weaponController = gameObject.AddComponent<WeaponController>();
                landscapeController = gameObject.AddComponent<LandscapeController>();
                permaTraitController = gameObject.GetComponent<PermaTraitController>();
               // armorController = gameObject.AddComponent<ArmorController>();
                
                // CombatEventService.Instance.OnSOT += ()=> PopulateOverCharBars(false); 
                //CombatEventService.Instance.OnEOC -= FortitudeReset2FortOrg;
                //CombatEventService.Instance.OnEOC += FortitudeReset2FortOrg;

                fleeController = gameObject.AddComponent<FleeController>();
            }
            statBuffController = gameObject.AddComponent<StatBuffController>();
            permaTraitController = gameObject.AddComponent<PermaTraitController>();
            tempTraitController = gameObject.AddComponent<TempTraitController>();
            charStateController = gameObject.AddComponent<CharStateController>();

            skillController = gameObject.AddComponent<SkillController1>();
            SkillService.Instance.allSkillControllers.Add(skillController);

        }   
        public AttribChanceData GetAttribChanceData(AttribName _statName)
        {
            foreach (AttribChanceData attribChanceData in CharService.Instance.statChanceSO.allStatChanceData)
            {
                if (attribChanceData.attribName == _statName)
                {
                    return attribChanceData; 
                }
            }
            return null; 
        }

        // THIS IS PRIMARILY USED IN THE COMBAT AND THEREFORE SHOULD EVENTUALLY BE SHIFTED THERE 
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                CombatEventService.Instance.On_CharRightClicked(gameObject);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                DynamicPosData charDyna = GridService.Instance.GetDyna4GO(gameObject);
                if (charDyna == null) return;
                if (CombatService.Instance.combatState == CombatState.INCombat_InSkillSelected)
                {
                    if (GridService.Instance.IsTileHLOne(charDyna.FwdtilePos))   // if the clicked char is targeted 
                    {
                        CombatEventService.Instance.On_targetClicked(charDyna, null);
                    }
                    else return; // to fix null error on wrong target click
                }else if (CombatService.Instance.combatState == CombatState.INCombat_normal
                                 || CombatService.Instance.combatState == CombatState.INTactics)
                {
                    CombatEventService.Instance.On_CharClicked(gameObject);
                }
                    
            }else
            {
                CombatEventService.Instance.On_CharHovered(gameObject);
            }
        }
        private void OnMouseEnter()
        {
            SkeletonAnimation skeletonAnim = gameObject.GetComponentInChildren<SkeletonAnimation>();
            if (skeletonAnim == null) return; // can happen during skill use
            skeletonAnim.skeleton.SetColor(new Color(224/255f,232/255f,255/255f,1f)); 
            //foreach (Material mat in skeletonAnim.gameObject.GetComponent<MeshRenderer>().sharedMaterials)
            //{
            //    mat.shader = shaderOnHover;
            //}
        }
        private void OnMouseExit()
        {
            SkeletonAnimation skeletonAnim = gameObject.GetComponentInChildren<SkeletonAnimation>();
            if (skeletonAnim == null) return; // can happen during skill use
            skeletonAnim.skeleton.SetColor(new Color(1, 1, 1f));

            //foreach (Material mat in skeletonAnim.gameObject.GetComponent<MeshRenderer>().sharedMaterials)
            //{
            //    mat.shader = shaderN;
            //    //int id = mat.shader.FindPropertyIndex("_Cutoff");
            //    //Color color = new Color(0, 0, 0, 1);
            //    //mat.SetColor(ShaderUtilities.ID_FaceColor, color); 
            //    //Debug.Log("PROPERTY" + mat.shader.GetPropertyDefaultFloatValue(id));
            //}
        }


        public float GetStatChance(AttribName _attribName, float _statValue)
        {
            foreach (AttribChanceData  statChanceData in CharService.Instance.statChanceSO.allStatChanceData)
            {
                if (statChanceData.attribName == _attribName)
                {
                    int len = statChanceData.allStatsNChances.Count; 

                    for( int i=0; i < len; i++)
                    {
                         
                        if (_statValue == statChanceData.allStatsNChances[i].statValue)
                        {
                            return statChanceData.allStatsNChances[i].statChance; 

                        } 
                        //else if (_statValue < statChanceData.allStatsNChances[i+1].statValue && 
                        //    _statValue > statChanceData.allStatsNChances[i].statValue)
                        //{
                        //        float avg = (statChanceData.allStatsNChances[i + 1].statChance
                        //        + statChanceData.allStatsNChances[i].statChance) / 2; 

                        //        return avg; 
                        //}
                    }
                } 
            }
            return -1f;  
        }
        public StatData GetStat(StatName _statName)
        {
            List<StatData> st = charModel.statList;
            int index = st.FindIndex(x => x.statName == _statName);
            return st[index];
        }
        public AttribData GetAttrib(AttribName attribName)
        {
            List<AttribData> st = charModel.attribList;
            int index = st.FindIndex(x => x.AttribName == attribName);    
            if(index != -1)
            return st[index];
            else
                Debug.Log("Attrib Name " + attribName);
            return null;
        }
        public void SetCurrStat(CauseType causeType, int causeName, int causeByCharID, AttribName _statName, float _newValue, bool toInvoke = true )
        {
            AttribData statData = charModel.attribList.Find(x => x.AttribName == _statName);
            float currentVal = statData.currValue;
            float modCurrValue = Constrain2LimitAttrib(statData.AttribName, currentVal);
            if (statData.isClamped) return;
            statData.currValue = (int)modCurrValue;
            int turn = -1;
            AttribModData charModData = new AttribModData(turn, causeType, causeName, causeByCharID,
                                         charModel.charID, _statName, modCurrValue);
            if (toInvoke)
            {
                OnAttribCurrValSet?.Invoke(charModData);                
            }               
        }
        public bool IsClamped(AttribName attribName)
        {
            AttribData attribData = GetAttrib(attribName);
            if (attribData.isClamped)
                return true; 
            else 
                return false;
        }
        public void ClampAttribToggle(AttribName attribName, bool toClamp)
        {
            AttribData statData = charModel.attribList.Find(x => x.AttribName == attribName);
            statData.isClamped = toClamp; 
        }
        public void ClampStatToggle(StatName statName, bool toClamp)
        {
            StatData statData = charModel.statList.Find(x => x.statName == statName);
            statData.isClamped = toClamp;
        }
        public void ClampStatToggle2Val(StatName statName, bool toClamp, float val)
        {
            StatData statData = charModel.statList.Find(x => x.statName == statName);
            statData.currValue = (int)val;
            statData.isClamped = toClamp;
        }
        public float GetDisplayAttrib(AttribName _statName)
        {
            float actualVal = GetAttrib(_statName).currValue;
            float minL = GetAttrib(_statName).minLimit;
            float maxL = GetAttrib(_statName).maxLimit;
            if (actualVal < minL)
                return minL;
            else if (actualVal > maxL)
                return maxL;
            else
                return actualVal; 

        }
        
        public AttribModData ClampStat(CauseType causeType, int CauseName, int causeByCharID
                                                , AttribName statName, float value, bool toInvoke = true, bool isClamp =true)
        {
            AttribData statData = GetAttrib(statName);
        
            if (isClamp)
                statData.isClamped = true;
            else
                statData.isClamped = false;

            AttribModData charModData = ChangeAttrib(causeType, CauseName, causeByCharID, statName, value, toInvoke); 

            return charModData; 
        }
        public StatModData ChangeStat(CauseType causeType, int CauseName, int causeByCharID, StatName statName
                                                                              , float value, bool toInvoke = true)
        {
            // COMBAT PATCH FIX BEGINS 
            int turn = -1;
            DynamicPosData dyna = null;
            StatData statData = GetStat(statName);
        
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                turn = CombatService.Instance.currentTurn;             
                Vector3 fwd = Vector3.zero;
                dyna = GridService.Instance.GetDyna4GO(gameObject);
                if (dyna == null)
                {
                    Debug.Log("ATTEMPTED change in stat" + causeType + "Name" + causeByCharID + "StatName" + statName);
                    return null;
                }
                if(statName == StatName.health)  
                {
                    float belowZero = GetHealthValBelow0(value);
                    if (belowZero <= 0)
                    {
                        StatModData statModDataFort = ChangeStat(CauseType.StatMinMaxLimit, 0, charModel.charID
                                     , StatName.fortitude, belowZero);
                        return statModDataFort;
                    }
                }               
                if(statName == StatName.health  
                            && charStateController.HasCharState(CharStateName.LastDropOfBlood)
                            && causeType == CauseType.CharSkill)
                {// saves chck 

                    SkillModel skillModel = SkillService.Instance.GetSkillModel(causeByCharID, (SkillNames)CauseName); 
                    if(skillModel.skillInclination == SkillInclination.Guard 
                            || skillModel.skillInclination == SkillInclination.Heal)
                        CombatEventService.Instance.combatModel.AddOn_CharSaved(causeByCharID, charModel); 
                }
            }
            bool isGain = value >= 0 ? true : false; 
            float altVal = statBuffController.GetStatRecAltData(statName, isGain);
            value = value*(1 + altVal / 100); 
        
            StatModData statModData = new StatModData(turn, causeType, CauseName, causeByCharID
                                                    , this.charModel.charID, statName,(int)value, (int)value);

            float currVal = statData.currValue;
            float preConstrainedValue = currVal + value;

            if (statData.isClamped)
            {
                Debug.Log("Value is clamped");  // due to some charstate or trait
                statModData.modVal = (int)currVal;  // no change is executed 
                return statModData;
            }
          
            if(statName == StatName.hunger || statName == StatName.thirst)
            {// HUNGER AND THIRST SPECIAL CASE HANDLED HERE>>>>
                return 
                ChangeHungerNThirst(causeType, (int)CauseName, causeByCharID, statName, value); 
            }
            float modCurrValue = Constrain2LimitStat(statModData.statModified, preConstrainedValue);
            // ACTUAL VALUE UPDATED HERE
            charModel.statList.Find(x => x.statName == statModData.statModified).currValue
                                                                            = (int)modCurrValue;
            statModData.modVal = (int)modCurrValue;
            
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
                PopulateOverCharBars();

            if (statName == StatName.health)
            {
                Debug.Log("CharHealth" + charModel.charNameStr + " HEALTH" + modCurrValue);            
                On_DeathChk(causeByCharID, causeType);
            }
            if (toInvoke)
            {   // IF NO CHANGE IN VALUE HAS HAPPENED DUE TO CLAMPING THIS NEEDS TO BE KEPT HERE
                OnStatChg?.Invoke(statModData);
            }
            return statModData;
        }
        public AttribModData ChangeAttrib(CauseType causeType, int CauseName, int causeByCharID, AttribName attribName
                                                                                , float value,  bool toInvoke = true) 
        {
            // COMBAT PATCH FIX BEGINS 
            int turn = -1;
            DynamicPosData dyna = null; 
            AttribData attribData = GetAttrib(attribName);         
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                turn = CombatService.Instance.currentTurn;
                //Debug.Log("attrib Change for  " + gameObject.name);
                //Debug.Log("Attrib CHANGE Cause " + causeType + "causeNAME "+ CauseName + " causebyCharID " + causeByCharID + " Stat " + attribName + " value " + value);
                Vector3 fwd = Vector3.zero;
                dyna = GridService.Instance.GetDyna4GO(gameObject);
                if (dyna == null)
                {
                   // Debug.Log("ATTEMPTED change in stat" + causeType + "Name" + causeByCharID + "StatName" + attribName);
                    return null;
                }
            }
     
            float currVal = attribData.currValue;
            float preConstrainedValue = currVal + value;
            // COMBAT PATCH FIX ENDS 
            // BroadCast the value change thru On_StatCurrValChg
            AttribModData charModData = new AttribModData(turn, causeType, CauseName, causeByCharID
                                         , this.charModel.charID, attribName, currVal, (int)value);
            if (attribData.isClamped)
            {
                Debug.Log("Value is clamped");  // due to some charstate or trait
                charModData.modCurrVal = currVal;  // no change is executed 
                return charModData;
            }
            if (toInvoke)
            {   // IF NO CHANGE IN VALUE HAS HAPPENED DUE TO CLAMPING THIS NEEDS TO BE KEPT HERE
                OnAttribChg?.Invoke(charModData);
            }


            float modCurrValue = Constrain2LimitAttrib(charModData.attribModified, preConstrainedValue);
            // ACTUAL VALUE UPDATED HERE
            //charModel.attribList.Find(x => x.AttribName == charModData.attribModified).currValue
            //                                                                = Mathf.RoundToInt(modCurrValue);
            if(attribName == AttribName.airRes)
            {
                Debug.Log(" value " + modCurrValue); 
            }
            int index = charModel.attribList.FindIndex(t => t.AttribName == attribName);
            if(index != -1)
            {
                charModel.attribList[index].currValue = Mathf.RoundToInt(modCurrValue); 
            }
            else
            {
                Debug.Log("attrib not found in list"); 
            }
            charModData.modCurrVal = modCurrValue;
            if (attribName == AttribName.vigor)
            {
                StatData statDataHP = GetStat(StatName.health);
                statDataHP.maxLimit = modCurrValue * 4;

                if (statDataHP.currValue >= statDataHP.maxLimit)
                    statDataHP.currValue = (int)statDataHP.maxLimit; 
            }
            if (attribName == AttribName.willpower)
            {
                StatData statDataStm = GetStat(StatName.stamina);
                statDataStm.maxLimit = modCurrValue * 3;

                if (statDataStm.currValue >= statDataStm.maxLimit)
                    statDataStm.currValue = (int)statDataStm.maxLimit;
            }

            if (toInvoke)
            {
                OnAttribCurrValSet?.Invoke(charModData);// broadcast the final change              
            }
            
            //// TBD: Following to be made event based 
            //if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            //    PopulateOverCharBars(attribName);

            //if(attribName == AttribName.health)
            //    CheckHealth();

            return charModData; 
        }
        public void SetMaxAttribValue(AttribName attribName, float val)
        {
            AttribData statData = GetAttrib(attribName);
            Debug.Log("MAX VALUE changed " + attribName +"to " + val); 
            statData.maxLimit = val; 
        }
        public void SetMaxStatValue(StatName statName, float val)
        {
            StatData statData = GetStat(statName);
            Debug.Log("MAX VALUE changed" + statName + "to " + val);
            statData.maxLimit = val;
        }
        public void ChangeAttribBaseVal(CauseType causeType, int name, int causeByCharID, AttribName statName
            , float maxChgR, bool toInvoke = true)
        {

        }
         float GetHealthValBelow0(float val)
         {
            StatData healthdata = GetStat(StatName.health); 
            if(healthdata.currValue + val <= healthdata.minLimit)
            {
                return healthdata.currValue + val; 
            }
            return 1f; 
         }
        void On_DeathChk(int causeByCharID, CauseType causeType)
        {       
            StatData statHP = GetStat(StatName.health); 
            if(statHP.currValue <= 0)
            {
                if(charModel.orgCharMode == CharMode.Enemy)
                {
                    if(causeType == CauseType.CharSkill)
                        CombatEventService.Instance.combatModel.AddOn_Kill(causeByCharID, charModel); 
                  CharService.Instance.On_CharDeath(this, causeByCharID); 
                }else
                {  
                    if(!charStateController.HasCharState(CharStateName.LastDropOfBlood))
                        charStateController.ApplyCharStateBuff(CauseType.StatMinMaxLimit, (int)0,
                                                        charModel.charID, CharStateName.LastDropOfBlood);                    
                }
            }
        }    
        public void HPRegen()  // linked to charController as Stamina regen
        {
            AttribData statData = GetAttrib(AttribName.hpRegen); 
            if(statData.currValue > 0)
            {
                ChangeStat(CauseType.HealthRegen, (int)AttribName.hpRegen, charModel.charID, StatName.health, statData.currValue); 
            }
        }
        void FortReset2FortOrg()
        {
            if (CharService.Instance.allCharsInPartyLocked.Any(t => t.charModel.charID != charModel.charID)) return;
            if (charModel.orgCharMode == CharMode.Ally)
            {
                AttribData fortOrgData = GetAttrib(AttribName.fortOrg);
                StatData fortData = GetStat(StatName.fortitude);
                fortData.currValue = fortOrgData.currValue; 
            }
        }
        public void RegenStamina()  // for ally and enemies stamina regen is 2 
        {
            StatModData statModData;
            StatData statData = charModel.statList.Find(t=>t.statName == StatName.stamina);
            AttribData stmRegenVal = GetAttrib(AttribName.staminaRegen); 

            int value = (int)statData.currValue;
            if (!statData.isClamped)
                 statModData = ChangeStat(CauseType.StaminaRegen, 0, this.charModel.charID, StatName.stamina, stmRegenVal.currValue);
            else
                Debug.Log("Stamina Clamped" + charModel.charName);
        }
        public float Constrain2LimitAttrib(AttribName attribName,float _value )
        {
            float value = 0f;
            AttribData attribData1 = GetAttrib(attribName);
            if (attribName == AttribName.dmgMin)
            {
                AttribData attribDataMax = GetAttrib(AttribName.dmgMax); 
                if(_value > attribDataMax.currValue)
                {
                    value = attribDataMax.currValue;
                    return value;
                }
            }
            if(attribName== AttribName.dmgMax)
            {
                AttribData attribDataMin = GetAttrib(AttribName.dmgMin);
                if (_value < attribDataMin.currValue)
                {
                    value = attribDataMin.currValue;
                    return value;
                }
            }
            if (attribName == AttribName.armorMin)
            {
                AttribData attribDataMax = GetAttrib(AttribName.armorMax);
                if (_value > attribDataMax.currValue)
                {
                    value = attribDataMax.currValue;
                    return value;
                }
            }
            if (attribName == AttribName.armorMax)
            {
                AttribData attribDataMin = GetAttrib(AttribName.armorMin);
                if (_value < attribDataMin.currValue)
                {
                    value = attribDataMin.currValue;
                    return value;
                }
            }


            if (_value >= attribData1.maxLimit)
                value = attribData1.maxLimit;
            else if (_value <= attribData1.minLimit)
                value = attribData1.minLimit;
            else
                value = _value; 
            return value; 
        }
        public float Constrain2LimitStat(StatName _statName, float _value)
        {
            float value = 0f;
            StatData statdata = GetStat(_statName);
            if (_value >= statdata.maxLimit)
                value = statdata.maxLimit;
            else if (_value <= statdata.minLimit)
                value = statdata.minLimit;
            else
                value = _value;
            return value;
        }

        #region   LVL AND EXPERIENCE CONTROLS
        public void ChgLevelUp(int finalLvl, int initlvl)
        {
            charModel.skillPts++;
            if(charModel.orgCharMode == CharMode.Ally)  // ensure pets or beeastiary don t level up
                LevelService.Instance.AutoLvlUpAlly(this, (int)initlvl, (int)finalLvl);            
        }
        public void ChgLevelDown()
        {
            // reset all skill top lvl 0 and accumulate to skill Points 
            // and subtract 1 from available skill points if ulti was selected in skills 
            // lose last Manual and Auto FX s..             


        }
        public void ExpPtsLoss(int val) 
        {
            charModel.mainExp -= (int)val;
            int prevlvl = charModel.charLvl-1;
            int totalExpPts = CharService.Instance.lvlNExpSO.GetThresholdExpPts4Lvl(prevlvl);
            if (charModel.mainExp < totalExpPts)
            {
                ChgLevelDown();
            }
        }
        public void ExpPtsGain(int val)
        {
            float addExp = (1+(charModel.expBonusModPercent/100)) * val;
            float valNew = val + addExp;

            charModel.mainExp += (int)valNew;
            int nextlvl = charModel.charLvl+1; 
            int totalExpPts = CharService.Instance.lvlNExpSO.GetThresholdExpPts4Lvl(nextlvl); 
            if(charModel.mainExp > totalExpPts)
            {
                int initlvl = charModel.charLvl; 
                ChgLevelUp(nextlvl, initlvl); 
            }
        }

        public void LvlUpOnCharSpawn()
        {
            // get final lvl , and spawn lvl from the so 
            int initLvl = charSO.charLvl;
            int finalLvl = charSO.spawnlvl;
            ChgLevelUp(finalLvl, initLvl); 
        }

        #endregion

        #region HUNGER AND THRIST

         StatModData ChangeHungerNThirst(CauseType causeType, int name, int causeByCharID, StatName statName
                                                                                                , float val)
        {
            if(statName == StatName.hunger)
            {
                float hungerVal = ((100 + charModel.hungerMod)/100) * val;
                StatModData statModData = 
                    ChangeStat(causeType, name, causeByCharID, StatName.hunger, hungerVal);
                return statModData; 
            }
            if (statName == StatName.thirst)
            {
                float thirstVal = ((100 + charModel.thirstMod) / 100)  * val;
                StatModData statModData =
                  ChangeStat(causeType, name, causeByCharID, StatName.thirst, thirstVal);
                return statModData;
            }

            return null; 
        }


        public void HealingAsPercentOfMaxHP(CauseType causeType, int causeName, float val)
        {
            StatData statData = GetStat(StatName.health);
            float healVal = ((val / 100) * statData.maxLimit);
            ChangeStat(causeType, (int)causeName, charModel.charID,  StatName.health, healVal);
        }


        #endregion

        void PopulateOverCharBars()
        {
            transform.GetChild(2).GetComponent<HPBarView>().FillHPBar(this);
        }

    
    }
}
