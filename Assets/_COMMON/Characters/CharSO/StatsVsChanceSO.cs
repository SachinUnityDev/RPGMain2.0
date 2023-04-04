﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 


namespace Common
{
    [System.Serializable]
    public class StatsNChances
    {       
        public float statValue;
        public float statChance;
    }

    [System.Serializable]
    public class StatChanceData
    {
       public AttribName statName;
       public List<StatsNChances> allStatsNChances = new List<StatsNChances>(); 
       public float  minLimit;
       public float maxLimit;        

    }

    [CreateAssetMenu(fileName = "StatVsChance", menuName = "Character Service/StatChances")]
    public class StatsVsChanceSO : ScriptableObject
    {
        public List<StatChanceData> allStatChanceData;

        private void Awake() 
        {

            allStatChanceData = new List<StatChanceData>();
            for (int i = 1; i < Enum.GetNames(typeof(AttribName)).Length; i++)
            {
                StatChanceData statChanceData = new StatChanceData();
                statChanceData.statName = (AttribName)i;
                switch ((AttribName)i)
                {
                    case AttribName.None:
                        break;
                    case AttribName.health:
                        statChanceData.minLimit = 0f;  // char Specific 
                        statChanceData.maxLimit = 100f; // this is not used
                        break;
                    case AttribName.stamina:
                        statChanceData.minLimit = 0f; // char specific
                        statChanceData.maxLimit = 100f; // this is not used
                        break;
                    case AttribName.fortitude:
                        statChanceData.minLimit = -30f;
                        statChanceData.maxLimit = 30f; break; 

                    case AttribName.hunger:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 100f; break;

                    case AttribName.thirst:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 100f; break;

                    case AttribName.damage:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 100f; break;

                    case AttribName.acc:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 12f;
                        FillLinearChanceValues(12, 66, 1, 12, 6, statChanceData.allStatsNChances);
                        break;
                    case AttribName.focus:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 12f;
                        FillLinearChanceValues(0, 8, 1, 12, 8, statChanceData.allStatsNChances); 
                        
                        break;

                    case AttribName.luck:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 12f;
                       statChanceData.allStatsNChances = FillCurveChanceValues(9f, 6f, 0f, 6f, 12f, 9f, 6f,  statChanceData.allStatsNChances); 
                        break;

                    case AttribName.morale:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 12f;
                        statChanceData.allStatsNChances = FillCurveChanceValues(9f, 6f, 0f, 6f, 12f, 9f, 6f, statChanceData.allStatsNChances);
                        break;

                    case AttribName.haste:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 12f; 
                        FillLinearChanceValues(0f, 1f, 1f, 12f, 1f, statChanceData.allStatsNChances); break;

                    case AttribName.vigor:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 100f; break;

                    case AttribName.willpower:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 100f; break;

                    case AttribName.armor:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 100f; break;

                    case AttribName.dodge:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 12f; 
                        FillLinearChanceValues(0, 6,1, 12,6, statChanceData.allStatsNChances);
                        break;
                    case AttribName.fireRes:
                        statChanceData.minLimit = -30f;
                        statChanceData.maxLimit = 90f; break;

                    case AttribName.earthRes:
                        statChanceData.minLimit = -30f;
                        statChanceData.maxLimit = 90f; break;
                    case AttribName.waterRes:
                        statChanceData.minLimit = -30f;
                        statChanceData.maxLimit = 90f; break;

                    case AttribName.airRes:
                        statChanceData.minLimit = -30f;
                        statChanceData.maxLimit = 90f; break;

                    case AttribName.lightRes:
                        statChanceData.minLimit = -20f;
                        statChanceData.maxLimit = 60f; break;

                    case AttribName.darkRes:
                        statChanceData.minLimit = -20f;
                        statChanceData.maxLimit = 60f; break;

                    case AttribName.fortOrg:
                        statChanceData.minLimit = -20f;
                        statChanceData.maxLimit = 20f; break;
                    case AttribName.hpRegen:
                        statChanceData.minLimit = -2f;
                        statChanceData.maxLimit = 6f; break;
                    case AttribName.staminaRegen:
                        statChanceData.minLimit = 0f;
                        statChanceData.maxLimit = 6f; break;


                    default:
                        break;
                }

                allStatChanceData.Add(statChanceData); 

            }
        }

      void FillLinearChanceValues(float _min, float  _minPlusOne, float _startPtr, float _endPtr, float _changeVal,   List<StatsNChances> _allStatNChances)
        {
            float prevValue = _minPlusOne;  
            StatsNChances _statNChance = new StatsNChances();
            _statNChance.statValue = 0; _statNChance.statChance = _min;
            _allStatNChances.Add(_statNChance);
            
            for (int i = (int)_startPtr; i <= (int)_endPtr; i++)
            {
              
                StatsNChances _statNChance1 = new StatsNChances();
                _statNChance1.statValue = i;
                if (i == _startPtr)
                    _statNChance1.statChance = _minPlusOne; 
                else
                    _statNChance1.statChance = prevValue +_changeVal ;
                
                _allStatNChances.Add(_statNChance1);
                prevValue = _statNChance1.statChance; 

            }

        }

    List<StatsNChances>  FillCurveChanceValues(float _minValUp, float _minValDown, float _centerVal, float _centerPtr, 
            float _endPtr, float _upChgVal, float _downChgVal,  List<StatsNChances> _allStatNChances)

    {
            //  Center value fill
            StatsNChances _statNChance = new StatsNChances();
            _statNChance.statValue = _centerPtr; _statNChance.statChance = _centerVal;
            _allStatNChances.Add(_statNChance);

            // move up 

            float prevValueUp = _minValUp;
            
            for (int i = (int)_centerPtr-1; i >=0  ; i--)
            {
                StatsNChances _statNChance1 = new StatsNChances();
                _statNChance1.statValue = i;
                if (i == _centerPtr - 1)
                {
                    _statNChance1.statChance = _minValUp;
                    Debug.Log("StatValue" + _statNChance1.statValue + "StatChance" + _statNChance1.statChance);
                }
                   
                else
                    _statNChance1.statChance = prevValueUp + _upChgVal;

                _allStatNChances.Add(_statNChance1);
                prevValueUp = _statNChance1.statChance;
            }

            // DOWN TRAVEL 
            float prevValueDn = _minValDown;
            for (int i = (int)_centerPtr + 1; i <= _endPtr; i++)
            {
                StatsNChances _statNChance2 = new StatsNChances();
                _statNChance2.statValue = i;
                if (i == (int)_centerPtr +1)
                    _statNChance2.statChance = _minValDown;
                else
                    _statNChance2.statChance = prevValueDn + _downChgVal;
                _allStatNChances.Add(_statNChance2);
                prevValueDn = _statNChance2.statChance;
            }          

           return _allStatNChances.OrderBy(o => o.statValue).ToList();

        }
    }
}