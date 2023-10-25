using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Combat;
using DG.Tweening;
using Interactables;
using Quest;
using Town; 


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

        float prevHPVal = 0f;
        float prevStaminaVal = 0f; 

        private void OnEnable()
        {
           

        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnEOC -= FortitudeReset2FortOrg;
          //  CombatEventService.Instance.OnSOTactics -= AddControllers_OnCombatStart;
        }
        public CharModel InitiatizeController(CharacterSO _charSO)
        {
            if (SaveService.Instance.slotSelect == SaveSlot.New)
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
                armorController = gameObject.AddComponent<ArmorController>();
                // CombatEventService.Instance.OnSOT += ()=> PopulateOverCharBars(false); 
                CombatEventService.Instance.OnEOC -= FortitudeReset2FortOrg;
                CombatEventService.Instance.OnEOC += FortitudeReset2FortOrg;
            }

            tempTraitController = gameObject.AddComponent<TempTraitController>();
            charStateController = gameObject.AddComponent<CharStateController>();

            skillController = gameObject.AddComponent<SkillController1>();
            SkillService.Instance.allSkillControllers.Add(skillController);
            //CombatEventService.Instance.OnSOTactics -= AddControllers_OnCombatStart;
            //CombatEventService.Instance.OnSOTactics += AddControllers_OnCombatStart;
        }
                

        //void AddControllers_OnCombatStart()
        //{
        //    damageController = gameObject.AddComponent<DamageController>();
        //    damageController.Init();
        //    strikeController = gameObject.AddComponent<StrikeController>();
        //    strikeController.Init(); 
        //}
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
                }        
                if (CombatService.Instance.combatState == CombatState.INCombat_normal
                                 || CombatService.Instance.combatState == CombatState.INTactics)
                     CombatEventService.Instance.On_CharClicked(gameObject);
            }else
            {
                CombatEventService.Instance.On_CharHovered(gameObject);
            }
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
        public AttribData GetAttrib(AttribName _statName)
        {
            List<AttribData> st = charModel.attribList;
            int index = st.FindIndex(x => x.AttribName == _statName);    
            if(index != -1)
            return st[index];
            else
                Debug.Log("Attrib Name " + _statName);
            return null;
        }
        public void SetCurrStat(CauseType causeType, int causeName, int causeByCharID, AttribName _statName, float _newValue, bool toInvoke = true )
        {
            AttribData statData = charModel.attribList.Find(x => x.AttribName == _statName);
            float currentVal = statData.currValue;
            float modCurrValue = Constrain2LimitAttrib(statData.AttribName, currentVal);
            if (statData.isClamped) return;
            statData.currValue = modCurrValue;
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
            statData.currValue = val;
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

        // clamp stat to value
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
        
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                turn = CombatService.Instance.currentTurn;             
                Vector3 fwd = Vector3.zero;
                dyna = GridService.Instance.GetDyna4GO(gameObject);
                if (dyna == null)
                {
                    Debug.Log("ATTEMPTED change in stat" + causeType + "Name" + causeByCharID + "StatName" + statName);
                    return null;
                }
                if (GetHealthValBelow0(value) <= 0)
                {
                    damageController.ApplyDamage(this, CauseType.StatMinMaxLimit, 0, DamageType.FortitudeDmg
                                                                        , GetHealthValBelow0(value), false);
                }
            }
            
            // COMBAT PATCH FIX ENDS 
            // BroadCast the value change thru On_StatCurrValChg
            StatModData statModData = new StatModData(turn, causeType, CauseName, causeByCharID
                                                             , this.charModel.charID, statName, value);

            float currVal = statData.currValue;
            float preConstrainedValue = currVal + value;

            if (statData.isClamped)
            {
                Debug.Log("Value is clamped");  // due to some charstate or trait
                statModData.modVal = currVal;  // no change is executed 
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
                                                                            = modCurrValue;
            statModData.modVal = modCurrValue;            
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
                PopulateOverCharBars(statName);

            if (statName == StatName.health)
                On_DeathBlow();
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
            AttribData statData = GetAttrib(attribName);         
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                turn = CombatService.Instance.currentTurn;
                Debug.Log("GAME OBJECT " + gameObject.name);
                Debug.Log("STAT CHANGE Cause " + causeType + " causebyCharID " + causeByCharID + " Stat " + attribName + " value " + value);
                Vector3 fwd = Vector3.zero;
                dyna = GridService.Instance.GetDyna4GO(gameObject);
                if (dyna == null)
                {
                    Debug.Log("ATTEMPTED change in stat" + causeType + "Name" + causeByCharID + "StatName" + attribName);
                    return null;
                }
               
            }
            // COMBAT PATCH FIX ENDS 
            // BroadCast the value change thru On_StatCurrValChg
            AttribModData charModData = new AttribModData(turn, causeType, CauseName, causeByCharID
                                                             , this.charModel.charID, attribName, value);

            float currVal = statData.currValue;
            float preConstrainedValue = currVal + value;

            if (statData.isClamped)
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
            charModel.attribList.Find(x => x.AttribName == charModData.attribModified).currValue
                                                                            = modCurrValue;
            charModData.modCurrVal = modCurrValue;              
            if (toInvoke)
            {
                OnAttribCurrValSet?.Invoke(charModData);// broadcast the final change              
            }
            //if (attribName == AttribName.vigor)
            //{
            //    //CharModData charModDataBase = new CharModData(turn, CauseType.StatChecks
            //    //    , (int)statName, charModData.causeByCharID, charModData.effectedCharNameID,
            //    //    StatsName.health, modCurrValue * 4f); 

            //    SetMaxValue(StatName.health, modCurrValue*4); 
            //}
            //if (attribName == AttribName.willpower)
            //{
            //    //CharModData charModDataBase = new CharModData(turn, CauseType.StatChecks
            //    //   , (int)statName, charModData.causeByCharID, charModData.effectedCharNameID,
            //    //   StatsName.stamina, modCurrValue * 3f);
            //    SetMaxValue(AttribName.stamina, modCurrValue*3); 
            //}
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
            Debug.Log("MAX VALUE changed" + attribName +"to " + val); 
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

        void On_DeathBlow()
        {       
            StatData statHP = GetStat(StatName.health); 
            if(statHP.currValue <= 0)
            {
                if(charModel.charMode == CharMode.Enemy)
                {
                  CharService.Instance.On_CharDeath(this); 
                    CharService.Instance.charDiedinLastTurn.Add(this); 
                }else
                {
                    Debug.Log("ALLY DEATH CODE TO BE WRITTEN HERE");
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

        void FortitudeReset2FortOrg()
        {
            if(charModel.orgCharMode == CharMode.Ally)
            {
                AttribData fortOrgData = GetAttrib(AttribName.fortOrg);
                ChangeStat(CauseType.StatChecks, (int)AttribName.fortOrg
                        , charModel.charID, StatName.fortitude, fortOrgData.currValue);
            }
        }

        public void RegenStamina()  // for ally and enemies stamina regen is 2 
        {
            StatModData statModData;
            StatData statData = charModel.statList.Find(t=>t.statName == StatName.stamina); 
            int value = (int)statData.currValue;
            if (!statData.isClamped)
                 statModData = ChangeStat(CauseType.StaminaRegen, 0, this.charModel.charID, StatName.stamina, 2);
            else
                Debug.Log("Stamina Clamped" + charModel.charName);
        }
        public float Constrain2LimitAttrib(AttribName _attribName,float _value )
        {
            float value = 0f; 
            AttribData statdata = GetAttrib(_attribName);
            if (_value >= statdata.maxLimit)
                value = statdata.maxLimit;
            else if (_value <= statdata.minLimit)
                value = statdata.minLimit;
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
        public void ChgLevelUp(int finalLvl)
        {
            charModel.skillPts++;
            int initLvl = charModel.charLvl;
            if(charModel.orgCharMode == CharMode.Ally)  // ensure pets or beeastiary don t level up
                LevelService.Instance.AutoLvlUpAlly(this, (Levels)initLvl, (Levels)finalLvl);            
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
            int totalExpPts = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(prevlvl);
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
            int totalExpPts = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(nextlvl); 
            if(charModel.mainExp > totalExpPts)
            {
                ChgLevelUp(nextlvl); 
            }
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

        #endregion
            
        void PopulateOverCharBars(StatName statName)
        {
        //    Transform hpBarsTransform = gameObject.transform.GetChild(2);

        //    Transform HPBarImgTrans = hpBarsTransform.GetChild(0).GetChild(1);
        //    Transform StaminaBarImgTrans = hpBarsTransform.GetChild(1).GetChild(1);

        //    Transform HPBarImgOrange = hpBarsTransform.GetChild(0).GetChild(0);
        //    Transform StaminaBarImgOrange = hpBarsTransform.GetChild(1).GetChild(0);
        //    StatData statData = GetStat(statName);
        //    AttribData willPowerSD = GetAttrib(AttribName.willpower);
        //    AttribData vigorSD = GetAttrib(AttribName.vigor);
        //    //float barVal = statData.currValue / statData.maxLimit;

        //    if (statName == StatName.health)
        //    {
        //        float barVal = statData.currValue / (vigorSD.currValue * 4);
        //        barVal = (barVal > 1) ? 1 : barVal;

        //        if (statData.currValue != prevHPVal)
        //        {
        //            Vector3 barImgScale = new Vector3(barVal, HPBarImgTrans.localScale.y, HPBarImgTrans.localScale.z);
        //            HPBarImgTrans.localScale = barImgScale;
        //            OrangeBarScaleAnim(HPBarImgOrange, barImgScale.x);
        //        }
        //        else return;

        //    }
        //    else if (statName == StatName.stamina)
        //    {
        //        float barVal = statData.currValue / (willPowerSD.currValue * 3);
        //        barVal = (barVal > 1) ? 1 : barVal;
        //        if (statData.currValue != prevStaminaVal)
        //        {
        //            Vector3 staminaScale = new Vector3(barVal, StaminaBarImgTrans.localScale.y, StaminaBarImgTrans.localScale.z);
        //            StaminaBarImgTrans.localScale = staminaScale;
        //            OrangeBarScaleAnim(StaminaBarImgOrange, staminaScale.x);
        //        }
        //        else return;


        //    }
        //    else if (statName == StatName.fortitude)
        //    {


        //    }
        //    prevHPVal = statData.currValue;
        //    prevStaminaVal = statData.currValue;
        //}
        //void OrangeBarScaleAnim(Transform barTrans, float scale)
        //{
        //    barTrans.gameObject.SetActive(true);
        //    Sequence barSeq = DOTween.Sequence();

        //    barSeq
        //        .AppendInterval(0.4f)
        //        .Append(barTrans.DOScaleX(scale, 1f))
        //        ;

        //    barSeq.Play().OnComplete(() => barTrans.gameObject.SetActive(false));
        }

    }
}
