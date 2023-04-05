using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 


namespace Common
{
    [System.Serializable]
    public class AttribNChances
    {       
        public float statValue;
        public float statChance;
    }

    [System.Serializable]
    public class AttribChanceData
    {
       public AttribName attribName;
       public List<AttribNChances> allStatsNChances = new List<AttribNChances>(); 
    }

    [CreateAssetMenu(fileName = "StatVsChance", menuName = "Character Service/StatChances")]
    public class StatsVsChanceSO : ScriptableObject
    {
        public List<AttribChanceData> allStatChanceData;

        private void Awake() 
        {

            allStatChanceData = new List<AttribChanceData>();
            for (int i = 1; i < Enum.GetNames(typeof(AttribName)).Length; i++)
            {
                AttribChanceData attribChanceData = new AttribChanceData();
                attribChanceData.attribName = (AttribName)i;
                switch ((AttribName)i)
                {
                    case AttribName.damage:
                        break;
                    case AttribName.acc:                       
                        FillLinearChanceValues(12, 66, 1, 12, 6, attribChanceData.allStatsNChances);
                        break;
                    case AttribName.focus:                       
                        FillLinearChanceValues(0, 8, 1, 12, 8, attribChanceData.allStatsNChances);                         
                        break;
                    case AttribName.luck:                     
                       attribChanceData.allStatsNChances = FillCurveChanceValues(9f, 6f, 0f, 6f, 12f, 9f, 6f,  attribChanceData.allStatsNChances); 
                        break;
                    case AttribName.morale:                       
                        attribChanceData.allStatsNChances = FillCurveChanceValues(9f, 6f, 0f, 6f, 12f, 9f, 6f, attribChanceData.allStatsNChances);
                        break;
                    case AttribName.haste:                       
                        FillLinearChanceValues(0f, 1f, 1f, 12f, 1f, attribChanceData.allStatsNChances); 
                        break;                        
                    case AttribName.vigor:
                        break; 
                    case AttribName.willpower:
                        break;
                    case AttribName.armor:
                        break; 
                    case AttribName.dodge:                         
                        FillLinearChanceValues(0, 6,1, 12,6, attribChanceData.allStatsNChances);
                        break;
                    case AttribName.fireRes:
                        break;
                    case AttribName.earthRes:
                        break;
                    case AttribName.waterRes:
                        break;
                    case AttribName.airRes:
                        break;
                    case AttribName.lightRes:
                        break;
                    case AttribName.darkRes:
                       break;
                    case AttribName.fortOrg:                       
                       break;
                    case AttribName.hpRegen:                      
                       break;
                    case AttribName.staminaRegen:                       
                       break;
                    default:
                        break;
                }
                allStatChanceData.Add(attribChanceData); 
            }
        }

      void FillLinearChanceValues(float _min, float  _minPlusOne, float _startPtr, float _endPtr, float _changeVal,   List<AttribNChances> _allStatNChances)
        {
            float prevValue = _minPlusOne;  
            AttribNChances _statNChance = new AttribNChances();
            _statNChance.statValue = 0; _statNChance.statChance = _min;
            _allStatNChances.Add(_statNChance);
            
            for (int i = (int)_startPtr; i <= (int)_endPtr; i++)
            {
              
                AttribNChances _statNChance1 = new AttribNChances();
                _statNChance1.statValue = i;
                if (i == _startPtr)
                    _statNChance1.statChance = _minPlusOne; 
                else
                    _statNChance1.statChance = prevValue +_changeVal ;
                
                _allStatNChances.Add(_statNChance1);
                prevValue = _statNChance1.statChance; 

            }

      }

    List<AttribNChances>  FillCurveChanceValues(float _minValUp, float _minValDown, float _centerVal, float _centerPtr, 
            float _endPtr, float _upChgVal, float _downChgVal,  List<AttribNChances> _allStatNChances)

    {
            //  Center value fill
            AttribNChances _statNChance = new AttribNChances();
            _statNChance.statValue = _centerPtr; _statNChance.statChance = _centerVal;
            _allStatNChances.Add(_statNChance);

            // move up 

            float prevValueUp = _minValUp;
            
            for (int i = (int)_centerPtr-1; i >=0  ; i--)
            {
                AttribNChances _statNChance1 = new AttribNChances();
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
                AttribNChances _statNChance2 = new AttribNChances();
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