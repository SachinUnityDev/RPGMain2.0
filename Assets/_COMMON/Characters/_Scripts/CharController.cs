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

        [Header("Common Controller")]
        public BuffController buffController;
        public CharTypeBuffController charTypeBuffController; 

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

        private void Start()
        {          
            buffController=  gameObject.AddComponent<BuffController>();
            charTypeBuffController= gameObject.AddComponent<CharTypeBuffController>();
            itemController = gameObject.AddComponent<ItemController>();
            skillController = gameObject.AddComponent<SkillController1>();
            weaponController= gameObject.AddComponent<WeaponController>();
            landscapeController= gameObject.AddComponent<LandscapeController>();    
            
            tempTraitController= gameObject.AddComponent<TempTraitController>();
            permaTraitController= gameObject.GetComponent<PermaTraitController>();  

            charStateController = gameObject.AddComponent<CharStateController>();
            armorController= gameObject.AddComponent<ArmorController>();
            SkillService.Instance.allSkillControllers.Add(skillController);
            // CombatEventService.Instance.OnSOT += ()=> PopulateOverCharBars(false); 
            CombatEventService.Instance.OnEOC += FortitudeReset2FortOrg;
            CombatEventService.Instance.OnSOTactics += AddControllerOnCombatStart; 

        }
        public CharModel InitiatizeController(CharacterSO _charSO)
        {
            if (SaveService.Instance.slotSelect == SaveSlot.New)
            {
                charModel = new CharModel(_charSO);
                CharService.Instance.lastCharID++;
                charModel.charID = CharService.Instance.lastCharID; 
            }
            else
            {
                charModel = CharService.Instance.LoadCharModel(_charSO.charName); 
            }
            OnCharSpawned?.Invoke(charModel.charID, charModel.charName);
            return charModel; 
        }
        void AddControllerOnCombatStart()
        {
            damageController = gameObject.AddComponent<DamageController>();
            strikeController = gameObject.AddComponent<StrikeController>();
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
                        CombatEventService.Instance.On_targetClicked(charDyna);
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
            return st[index];
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
            StatData StatData = GetStat(statName);
            Debug.Log("Game event" + GameEventService.Instance.isGame);
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                turn = CombatService.Instance.currentTurn;
                Debug.Log("GAME OBJECT " + gameObject.name);
                Debug.Log("STAT CHANGE Cause " + causeType + " causebyCharID " + causeByCharID + " Stat " + statName + " value " + value);
                Vector3 fwd = Vector3.zero;
                dyna = GridService.Instance.GetDyna4GO(gameObject);
                if (dyna == null)
                {
                    Debug.Log("ATTEMPTED change in stat" + causeType + "Name" + causeByCharID + "StatName" + statName);
                    return null;
                }
            }
            // COMBAT PATCH FIX ENDS 
            // BroadCast the value change thru On_StatCurrValChg
            StatModData statModData = new StatModData(turn, causeType, CauseName, causeByCharID
                                                             , this.charModel.charID, statName, value);

            float currVal = StatData.currValue;
            float preConstrainedValue = currVal + value;

            if (StatData.isClamped)
            {
                Debug.Log("Value is clamped");  // due to some charstate or trait
                statModData.modVal = currVal;  // no change is executed 
                return statModData;
            }
            if (toInvoke)
            {   // IF NO CHANGE IN VALUE HAS HAPPENED DUE TO CLAMPING THIS NEEDS TO BE KEPT HERE
                OnStatChg?.Invoke(statModData);
            }


            float modCurrValue = Constrain2LimitStat(statModData.statModified, preConstrainedValue);
            // ACTUAL VALUE UPDATED HERE
            charModel.statList.Find(x => x.statName == statModData.statModified).currValue
                                                                            = modCurrValue;
            statModData.modVal = modCurrValue;            
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
                PopulateOverCharBars(statName);

            if (statName == StatName.health)
                CheckHealth();

            return statModData;
        }

        public AttribModData ChangeAttrib(CauseType causeType, int CauseName, int causeByCharID, AttribName attribName
                                                                                , float value,  bool toInvoke = true) 
        {
            // COMBAT PATCH FIX BEGINS 
            int turn = -1;
            DynamicPosData dyna = null; 
            AttribData statData = GetAttrib(attribName);
            Debug.Log("Game event" + GameEventService.Instance.isGame);
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
        //public void SetBaseValue(StatsName statName, float val)
        //{
        //    //StatData statData = GetStat(charModData.statModified);
        //    //float orgValue = statData.baseValue;
        //    //float diffAmt = charModData.baseVal - statData.baseValue;
            
        //    //statData.currValue += diffAmt;

        //    //statData.baseValue = charModData.baseVal; 
            
        //}

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

        void CheckHealth()
        {       
            StatData statHP = GetStat(StatName.health); 
            if(statHP.currValue <= 0)
            {
                if(charModel.charMode == CharMode.Enemy)
                {
                  // CombatEventService.Instance.On_CharDeath(this); 
                    CharService.Instance.charDiedinLastTurn.Add(this); 
                }else
                {
                    Debug.Log("ALLY DEATH CODE TO BE WRITTEN HERE"); 
                    // LAST DROP of blood char State 
                    // has three chances, Cheated death is one of them
                    //ONce health is 0 DOT FX are blocked and do not effect here 


                }
            }
        }
        public AttribModData ChangeAttribRange(CauseType causeType, int name, int causeByCharID, AttribName attribName
            , float minChgR, float maxChgR, bool toInvoke = true)
        {

            int turn = -1; Vector3 fwd = Vector3.zero; 
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                 turn = CombatService.Instance.currentTurn;
                fwd = GridService.Instance.GetDyna4GO(gameObject).FwdtilePos;
            }
            AttribModData charModData = new AttribModData(turn, causeType, name, causeByCharID
                 , this.charModel.charID, attribName,0,0 );

            AttribData statData = GetAttrib(attribName);
            float minStatNet = statData.minRange + minChgR;
            float maxStatNet = statData.maxRange + maxChgR;


            charModData.modChgMinR = minStatNet;
            charModData.modChgMaxR = maxStatNet;
            charModel.attribList.Find(x => x.AttribName == attribName).minRange = minStatNet;
            charModel.attribList.Find(x => x.AttribName == attribName).maxRange = maxStatNet;
            if (toInvoke)
            {
                // to be checked
                OnAttribChg?.Invoke(charModData);
            }

            return charModData;
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
           // LevelService.Instance.LevelUpStats(this, (Levels)finalLvl);
            
            // CONNECT TO LEVEL SERVICE
            // attribute choice for vigor , Wp Manual clicks inventory Panel
            // Attribute up Auto  
        }
        public void ChgLevelDown()
        {
            // reset all skill top lvl 0 and accumulate to skill Points 
            // and subtract 1 from available skill points if ulti was selected in skills 
            // lose last Manual and Auto FX s..             


        }
        public void ExpPtsLoss(int val) 
        {
            charModel.expPoints -= (int)val;
            int prevlvl = charModel.charLvl-1;
            int totalExpPts = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(prevlvl);
            if (charModel.expPoints < totalExpPts)
            {
                ChgLevelDown();
            }
        }
        public void ExpPtsGain(int val)
        {
            float addExp = (1+(charModel.expBonusModPercent/100)) * val;
            float valNew = val + addExp;

            charModel.expPoints += (int)valNew;
            int nextlvl = charModel.charLvl+1; 
            int totalExpPts = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(nextlvl); 
            if(charModel.expPoints > totalExpPts)
            {
                ChgLevelUp(nextlvl); 
            }
        }
        #endregion

        #region HUNGER AND THRIST

        public StatModData ChangeHungerNThirst(CauseType causeType, int name, int causeByCharID, StatName statName
            , float val, bool toInvoke = true)
        {
            if(statName == StatName.hunger)
            {
                float hungerVal = ((100 + charModel.hungerMod)/100) + val;
                StatModData charModData = 
                    ChangeStat(causeType, name, causeByCharID, StatName.hunger, hungerVal);
                return charModData; 
            }
            if (statName == StatName.thirst)
            {
                float thirstVal = ((100 + charModel.thirstMod) / 100) + val;
                StatModData charModData =
                  ChangeStat(causeType, name, causeByCharID, StatName.thirst, thirstVal);
                return charModData;
            }

            return null; 
        }

        #endregion


        void PopulateOverCharBars(StatName statName)
        {
            Transform hpBarsTransform = gameObject.transform.GetChild(2);

            Transform HPBarImgTrans = hpBarsTransform.GetChild(0).GetChild(1);
            Transform StaminaBarImgTrans = hpBarsTransform.GetChild(1).GetChild(1);

            Transform HPBarImgOrange = hpBarsTransform.GetChild(0).GetChild(0);
            Transform StaminaBarImgOrange = hpBarsTransform.GetChild(1).GetChild(0);
            StatData statData = GetStat(statName);
            AttribData willPowerSD = GetAttrib(AttribName.willpower);
            AttribData vigorSD = GetAttrib(AttribName.vigor);
            //float barVal = statData.currValue / statData.maxLimit;
           
            if (statName == StatName.health)
            {
                float barVal = statData.currValue / (vigorSD.currValue * 4);
                barVal = (barVal > 1) ? 1 : barVal; 

                if (statData.currValue != prevHPVal)
                {  
                    Vector3 barImgScale = new Vector3(barVal, HPBarImgTrans.localScale.y, HPBarImgTrans.localScale.z);
                    HPBarImgTrans.localScale = barImgScale;
                    OrangeBarScaleAnim(HPBarImgOrange, barImgScale.x); 
                }
                else return; 

            }else if(statName == StatName.stamina)
            {
                float barVal = statData.currValue / (willPowerSD.currValue * 3);
                barVal = (barVal > 1) ? 1 : barVal;
                if (statData.currValue != prevStaminaVal)
                {                  
                    Vector3 staminaScale = new Vector3(barVal, StaminaBarImgTrans.localScale.y, StaminaBarImgTrans.localScale.z);
                    StaminaBarImgTrans.localScale = staminaScale;
                    OrangeBarScaleAnim(StaminaBarImgOrange, staminaScale.x);
                }
                else return;


            }else if(statName == StatName.fortitude)
            {


            }
            prevHPVal = statData.currValue;
            prevStaminaVal = statData.currValue;
        }        
        void OrangeBarScaleAnim(Transform barTrans, float scale)
        {
            barTrans.gameObject.SetActive(true);
            Sequence barSeq = DOTween.Sequence();

            barSeq
                .AppendInterval(0.4f)
                .Append(barTrans.DOScaleX(scale, 1f))
                ;

            barSeq.Play().OnComplete(() => barTrans.gameObject.SetActive(false)); 
        }
    }
}

// changes stat by a given  value 
//public void ChangeStat(StatsName _statName, float _valueChange = 0.0f, 
//    float _minRangeChange = 0.0f, float _maxRangeChange = 0.0f, bool toInvoke = true)
//{


//}
//_valueChange = Mathf.Round(_valueChange); _maxRangeChange = Mathf.Round(_maxRangeChange); _minRangeChange = Mathf.Round(_minRangeChange);

//if (_statName != StatsName.None)
//{
//    if (_valueChange != 0.0f)
//    {

//        float preConstrainedValue = charModel.statsList.Find(x => x.statsName == _statName).currValue + _valueChange; 
//        charModel.statsList.Find(x => x.statsName == _statName).currValue = Constrain2Limit(_statName,preConstrainedValue);
//        OnStatMinMax(_statName); 
//        if (toInvoke)
//        {
//          //  OnStatChanged?.Invoke(_statName, _valueChange);// if in all cases final values r valued no point in chasing
//          //  change go with setStat
//          //  OnStatSet(_statName, _valueChange);
//        }

//    }
//    if (_minRangeChange != 0.0f)
//    {
//        float fMinRange = charModel.statsList.Find(x => x.statsName == _statName).minRange + _valueChange;
//        charModel.statsList.Find(x => x.statsName == _statName).minRange = Constrain2Limit(_statName, fMinRange);                    
//        if (toInvoke)
//            StatMinRSet?.Invoke(_statName, _minRangeChange); 

//    }
//    if (_maxRangeChange != 0.0f)
//    {
//        float fMaxRange = charModel.statsList.Find(x => x.statsName == _statName).maxRange + _valueChange;
//        charModel.statsList.Find(x => x.statsName == _statName).maxRange = Constrain2Limit(_statName, fMaxRange);
//        if (toInvoke)
//            StatMaxRSet?.Invoke(_statName, _maxRangeChange); 

//    }


// fleeingC exp/ fortitude Origin loss ..
// C-> E    CauseType: Skill, Permanent traits, temp traits,charStates, PotionItem, 
//Gewgaws, otherItems, HealthRegen, StaminaRegen, FleeingQ, FleeCombat,  
// yes lets build. .. it FXDatapack(ChangeType, )

// TurnNo, Causetype , CauseByCharName,  EffectedCharName, StatName, ChangeValue, castTime,
// fort org to be added to stats.. 
// 
// Turn No CuasedType


//switch.. case for each type 

// call respective methods that create individual data packs for each change
// TurnModel will contain list of data Packs that 
// each data pack will contain causeGO,List<GO> effectedGOs, ChangeType,  



// ############################################################# POPULATE CHAR ######################################

//public void PopulateOverCharBars(bool statChg)
//{
//    StatData HPData = charController.GetStat(StatsName.health);
//    StatData StaminaData = charController.GetStat(StatsName.stamina);

//    StatData fortData = charController.GetStat(StatsName.fortitude);
//    // Debug.Log("CHAR CONTROLLER " + charController.name);
//    Transform hpBarsTransform = charController.gameObject.transform.GetChild(2);


//    Transform HPBarImgTrans = hpBarsTransform.GetChild(0).GetChild(1);
//    Transform StaminaBarImgTrans = hpBarsTransform.GetChild(1).GetChild(1);

//    Transform HPBarImgOrange = hpBarsTransform.GetChild(0).GetChild(0);
//    Transform StaminaBarImgOrange = hpBarsTransform.GetChild(1).GetChild(0);

//    float HPbarVal = HPData.currValue / HPData.maxLimit;
//    float staminaBarVal = StaminaData.currValue / StaminaData.maxLimit;
//    Vector3 HPbarImgScale = new Vector3(HPbarVal, HPBarImgTrans.localScale.y, HPBarImgTrans.localScale.z);
//    HPBarImgTrans.localScale = HPbarImgScale;
//    Vector3 staminaScale = new Vector3(staminaBarVal, StaminaBarImgTrans.localScale.y, StaminaBarImgTrans.localScale.z);
//    StaminaBarImgTrans.localScale = staminaScale;


//    if (statChg)
//    {
//        if (prevHPVal == HPbarVal)
//            HPBarImgOrange.DOScaleX(1, 0.01f);
//        else
//            prevHPVal = HPbarVal;
//        if (prevStaminaVal == staminaBarVal)
//            StaminaBarImgOrange.DOScale(1, 0.01f);
//        else
//            prevStaminaVal = HPbarVal;

//        HPBarImgOrange.gameObject.SetActive(true);
//        StaminaBarImgOrange.gameObject.SetActive(true);

//        Sequence barSeq = DOTween.Sequence();
//        //   Debug.Log("CHAR  " + charModel.charName);
//        barSeq.AppendInterval(0.25f);
//        barSeq.Append(HPBarImgOrange.DOScaleX(HPbarVal, 1f));
//        barSeq.Append(StaminaBarImgOrange.DOScaleX(staminaBarVal, 1f));

//        barSeq.Play().OnComplete(() => {
//            HPBarImgOrange.gameObject.SetActive(false);
//            StaminaBarImgOrange.gameObject.SetActive(false);
//        });
//    }
//}



//StatData HPData = charController.GetStat(StatsName.health);
//StatData StaminaData = charController.GetStat(StatsName.stamina);

//StatData fortData = charController.GetStat(StatsName.fortitude);
// Debug.Log("CHAR CONTROLLER " + charController.name);
//Transform hpBarsTransform = charController.gameObject.transform.GetChild(2);


//Transform HPBarImgTrans = hpBarsTransform.GetChild(0).GetChild(1);
//Transform StaminaBarImgTrans = hpBarsTransform.GetChild(1).GetChild(1);

//Transform HPBarImgOrange = hpBarsTransform.GetChild(0).GetChild(0);
//Transform StaminaBarImgOrange = hpBarsTransform.GetChild(1).GetChild(0);

//float HPbarVal = HPData.currValue / HPData.maxLimit;
//float staminaBarVal = StaminaData.currValue / StaminaData.maxLimit;
//Vector3 HPbarImgScale = new Vector3(HPbarVal, HPBarImgTrans.localScale.y, HPBarImgTrans.localScale.z);
//HPBarImgTrans.localScale = HPbarImgScale;



//if (statChg)
//{
//    if (prevHPVal == HPbarVal)
//        HPBarImgOrange.DOScaleX(1, 0.01f);
//    else
//        prevHPVal = HPbarVal;
//    if (prevStaminaVal == staminaBarVal)
//        StaminaBarImgOrange.DOScale(1, 0.01f);
//    else
//        prevStaminaVal = HPbarVal;

//    //HPBarImgOrange.gameObject.SetActive(true);
//    //StaminaBarImgOrange.gameObject.SetActive(true);

//    Sequence barSeq = DOTween.Sequence();
//    //   Debug.Log("CHAR  " + charModel.charName);
//    barSeq.AppendInterval(0.25f);
//    barSeq.Append(HPBarImgOrange.DOScaleX(HPbarVal, 1f));
//    barSeq.Append(StaminaBarImgOrange.DOScaleX(staminaBarVal, 1f));

//    barSeq.Play().OnComplete(() =>
//    {
//        HPBarImgOrange.gameObject.SetActive(false);
//        StaminaBarImgOrange.gameObject.SetActive(false);
//    });
//}