using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Combat;
using DG.Tweening;
using Interactables;
using System.Windows.Forms.DataVisualization.Charting;

namespace Common
{   

    public class CharController : MonoBehaviour
    {
        public event Action<int,CharNames> OnCharSpawned;  // charID only to be broadcasted
        public event Action<CharModData> OnStatChg; // return the current modified value 
        public event Action<CharModData> OnStatCurrValSet;  // curr values 
        public event Action<CharModData> OnBaseValueChg;
        public event Action<float> OnExpGainedOrLoss;

        public CharModel charModel;     

        [Header("Combat Controllers")]

       // public PostStatChgApplied postStatChgApplied;   // Deprecated 
        public DamageController damageController;   
        public StrikeController strikeController;

        [Header("Common Controller")]
        public BuffController buffController;
        public CharStateController charStateController;
       
        float prevHPVal = 0f;
        float prevStaminaVal = 0f; 

        private void Start()
        {          
           // charController = gameObject?.GetComponent<CharController>(); // deprecated 
            buffController=  gameObject.AddComponent<BuffController>();
            charStateController = gameObject.AddComponent<CharStateController>();
            
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
            //postStatChgApplied = gameObject.AddComponent<PostStatChgApplied>(); Deprecated 
            damageController = gameObject.AddComponent<DamageController>();
            strikeController = gameObject.AddComponent<StrikeController>();
        }
        public StatChanceData GetStatChanceData(StatsName _statName)
        {
            foreach (StatChanceData statChanceData in CharService.Instance.statChanceSO.allStatChanceData)
            {
                if (statChanceData.statName == _statName)
                {
                    return statChanceData; 
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
        public float GetStatChance(StatsName _statName, float _statValue)
        {
            foreach (StatChanceData  statChanceData in CharService.Instance.statChanceSO.allStatChanceData)
            {
                if (statChanceData.statName == _statName)
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

        public StatData GetStat(StatsName _statName)
        {
            List<StatData> st = charModel.statsList;
            int index = st.FindIndex(x => x.statsName == _statName);           
            return st[index];
        }

        // no change but directly setting value 
        public void SetCurrStat(CauseType causeType, int causeName, int causeByCharID, StatsName _statName, float _newValue, bool toInvoke = true )
        {
            
            StatData statData = charModel.statsList.Find(x => x.statsName == _statName);
            float currentVal = statData.currValue;
            float modCurrValue = Constrain2Limit(statData.statsName, currentVal);
            if (statData.isClamped) return;
            statData.currValue = modCurrValue;
            int turn = -1;
            CharModData charModData = new CharModData(turn, causeType, causeName, causeByCharID,
                                         charModel.charID, _statName, modCurrValue);
            if (toInvoke)
            {
                OnStatCurrValSet?.Invoke(charModData);                
            }
               
        }
        public bool IsClamped(StatsName statName)
        {
            StatData statData = GetStat(statName);
            if (statData.isClamped)
                return true; 
            else 
                return false;
        }
        public void ClampStatToggle(StatsName statName, bool toClamp)
        {
            StatData statData = charModel.statsList.Find(x => x.statsName == statName);
            statData.isClamped = toClamp; 
        }

        public float GetDisplayStat(StatsName _statName)
        {
            float actualVal = GetStat(_statName).currValue;
            float minL = GetStatChanceData(_statName).minLimit;
            float maxL = GetStatChanceData(_statName).maxLimit;
            if (actualVal < minL)
                return minL;
            else if (actualVal > maxL)
                return maxL;
            else
                return actualVal; 

        }


        // clamp stat to value
        public CharModData ClampStat(CauseType causeType, int CauseName, int causeByCharID
                                                , StatsName statName, float value, bool toInvoke = true, bool isClamp =true)
        {
            StatData statData = GetStat(statName);
        
            if (isClamp)
                statData.isClamped = true;
            else
                statData.isClamped = false;

            CharModData charModData = ChangeStat(causeType, CauseName, causeByCharID, statName, value, toInvoke); 

            return charModData; 
        }

        public CharModData ChangeStat(CauseType causeType, int CauseName, int causeByCharID, StatsName statName
                                                                                , float value,  bool toInvoke = true) 
        {
            // COMBAT PATCH FIX BEGINS 
            int turn = -1;
            DynamicPosData dyna = null; 
            StatData statData = GetStat(statName);
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
            CharModData charModData = new CharModData(turn, causeType, CauseName, causeByCharID
                                                             , this.charModel.charID, statName, value);

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
                OnStatChg?.Invoke(charModData);
            }


            float modCurrValue = Constrain2Limit(charModData.statModified, preConstrainedValue);
            // ACTUAL VALUE UPDATED HERE
            charModel.statsList.Find(x => x.statsName == charModData.statModified).currValue
                                                                            = modCurrValue;
            charModData.modCurrVal = modCurrValue;              
            if (toInvoke)
            {
                OnStatCurrValSet?.Invoke(charModData);// broadcast the final change              
            }
            if (statName == StatsName.vigor)
            {
                //CharModData charModDataBase = new CharModData(turn, CauseType.StatChecks
                //    , (int)statName, charModData.causeByCharID, charModData.effectedCharNameID,
                //    StatsName.health, modCurrValue * 4f); 

                SetMaxValue(StatsName.health, modCurrValue*4); 
            }
            if (statName == StatsName.willpower)
            {
                //CharModData charModDataBase = new CharModData(turn, CauseType.StatChecks
                //   , (int)statName, charModData.causeByCharID, charModData.effectedCharNameID,
                //   StatsName.stamina, modCurrValue * 3f);
                SetMaxValue(StatsName.stamina, modCurrValue*3); 
            }
            // TBD: Following to be made event based 
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
                PopulateOverCharBars(statName);
           
            if(statName == StatsName.health)
                CheckHealth();

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

        public void SetMaxValue(StatsName statName, float val)
        {
            StatData statData = GetStat(statName);
            Debug.Log("MAX VALUE changed" + statName +"to " + val); 
            statData.maxLimit = val; 

        }



        void CheckHealth()
        {       
            StatData StatHP = GetStat(StatsName.health); 
            if(StatHP.currValue <= 0)
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
        public CharModData ChangeStatRange(CauseType causeType, int name, int causeByCharID, StatsName statName
            , float minChgR, float maxChgR, bool toInvoke = true)
        {

            int turn = -1; Vector3 fwd = Vector3.zero; 
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                 turn = CombatService.Instance.currentTurn;
                fwd = GridService.Instance.GetDyna4GO(gameObject).FwdtilePos;
            }
            CharModData charModData = new CharModData(turn, causeType, name, causeByCharID
                 , this.charModel.charID, statName,0,0 );

            StatData statData = GetStat(statName);
            float minStatNet = statData.minRange + minChgR;
            float maxStatNet = statData.maxRange + maxChgR;


            charModData.modChgMinR = minStatNet;
            charModData.modChgMaxR = maxStatNet;
            charModel.statsList.Find(x => x.statsName == statName).minRange = minStatNet;
            charModel.statsList.Find(x => x.statsName == statName).maxRange = maxStatNet;
            if (toInvoke)
            {
                // to be checked
                OnStatChg?.Invoke(charModData);
            }

            return charModData;
        }       

        public void HPRegen()  // linked to charController as Stamina regen
        {
            StatData statData = GetStat(StatsName.hpRegen); 
            if(statData.currValue > 0)
            {
                ChangeStat(CauseType.HealthRegen, (int)StatsName.hpRegen, charModel.charID, StatsName.health, statData.currValue); 
            }
        }

        void FortitudeReset2FortOrg()
        {
            if(charModel.orgCharMode == CharMode.Ally)
            {
                StatData fortOrgData = GetStat(StatsName.fortOrg);
                ChangeStat(CauseType.StatChecks, (int)StatsName.fortOrg
                        , charModel.charID, StatsName.fortitude, fortOrgData.currValue);
            }

        }

        public void RegenStamina()  // for ally and enemies stamina regen is 2 
        {
            CharModData charModData;
            StatData staminaData = charModel.staminaRegen; 
            int value = (int)staminaData.currValue;
            if (!staminaData.isClamped)
                 charModData = ChangeStat(CauseType.StaminaRegen, 0, this.charModel.charID, StatsName.stamina, 2);
            else
                Debug.Log("Stamina Clamped" + charModel.charName);
        }
        public float Constrain2Limit(StatsName _statName,float _value )
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
            float addExp = charModel.expBonusModPercent * val;           
            float valNew = val + addExp; 

            charModel.expPoints -= (int)valNew;
            int prevlvl = charModel.charLvl--;
            int totalExpPts = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(prevlvl);
            if (charModel.expPoints < totalExpPts)
            {
                ChgLevelDown();
            }
        }
        public void ExpPtsGain(int val)
        {
            float addExp = charModel.expBonusModPercent * val;
            float valNew = val + addExp;

            charModel.expPoints += (int)valNew;
            int nextlvl = charModel.charLvl++; 
            int totalExpPts = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(nextlvl); 
            if(charModel.expPoints > totalExpPts)
            {
                ChgLevelUp(nextlvl); 
            }
        }

#endregion

        void PopulateOverCharBars(StatsName statName)
        {
            Transform hpBarsTransform = gameObject.transform.GetChild(2);

            Transform HPBarImgTrans = hpBarsTransform.GetChild(0).GetChild(1);
            Transform StaminaBarImgTrans = hpBarsTransform.GetChild(1).GetChild(1);

            Transform HPBarImgOrange = hpBarsTransform.GetChild(0).GetChild(0);
            Transform StaminaBarImgOrange = hpBarsTransform.GetChild(1).GetChild(0);
            StatData statData = GetStat(statName);
            StatData willPowerSD = GetStat(StatsName.willpower);
            StatData vigorSD = GetStat(StatsName.vigor);
            //float barVal = statData.currValue / statData.maxLimit;
           
            if (statName == StatsName.health)
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

            }else if(statName == StatsName.stamina)
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


            }else if(statName == StatsName.fortitude)
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