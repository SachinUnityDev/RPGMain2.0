using Combat;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{

    public class StatBuffData
    {
        public int buffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public StatModData statModData;
        public TimeState timeState; // FOR DAY AND NIGHT BUFFS

        public StatBuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame,
            int buffedNetTime, StatModData statModData, TimeState timeState = TimeState.None)
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.statModData = statModData;
            this.timeState = timeState;
        }
    }

    [Serializable]
    public class StatAltBuffData
    {
        public int dmgBuffID;
        public CauseType causeType;
        public int causeName;
        public int causeByCharID;
        public bool isPositive = true; 

        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public StatAltData altData;  // contains value for the buff        

        public StatAltBuffData(int dmgBuffID, CauseType causeType, int causeName, int causeByCharID, 
            bool isBuff,bool isPositive,  int startRoundNo, TimeFrame timeFrame
                            , int buffedNetTime, StatAltData altData )
        {
            this.dmgBuffID = dmgBuffID;
            this.isBuff = isBuff;
            this.isPositive= isPositive;
            this.causeType= causeType;
            this.causeName = causeName;
            this.causeByCharID= causeByCharID;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;// time counter for the dmgBuff
            this.altData = altData;       
        }
    }
    [Serializable]
    public class StatAltData
    {
        public StatName statModified;
        
        public bool isGain = false;


        public float valPercent = 0f;
        public StatAltData(StatName statModified, float valPercent, bool isGain)
        {
            this.statModified = statModified;                        
            this.isGain = isGain;         

            this.valPercent = valPercent;
        }
    }

    public class StatBuffController : MonoBehaviour
    {

        List<StatBuffData> allDayNightbuffs = new List<StatBuffData>();       
        // use array here for the index to work 

        CharController charController;
        [SerializeField] List<string> buffStrs = new List<string>();
        [SerializeField] List<string> deDuffStrs = new List<string>();

        public int buffIndex = 0;

  
        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR1 += EORTick;
            CombatEventService.Instance.OnEOC += EOCTickStatAltBuff;
            CalendarService.Instance.OnChangeTimeState += ToggleBuffsOnTimeStateChg;
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnEOR1 -= EORTick;
            CombatEventService.Instance.OnEOC -= EOCTickStatAltBuff;            
            CalendarService.Instance.OnChangeTimeState -= ToggleBuffsOnTimeStateChg;
        }

        #region  APPLY_BUFFS 
     
        public int ApplyNInitBuffOnDay(CauseType causeType, int causeName, int causeByCharID
                              , StatName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {

            StatModData statModData = charController.ChangeStat(causeType, causeName, causeByCharID
                                                         , statName, value, true);

            if (CalendarService.Instance.currtimeState == TimeState.Night) // FOR NIGHT CORRECTION
            {
                charController.ChangeStat(causeType, causeName, causeByCharID
                                                        , statName, -value, true);
            }
            int currRd = CombatEventService.Instance.currentRound;
            buffIndex++;

            StatBuffData buffData = new StatBuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
                                                                   statModData);

            //allBuffs.Add(buffData);
            allDayNightbuffs.Add(buffData);
            return buffIndex;

        }
        void ToggleBuffsOnTimeStateChg(TimeState timeState) // ON start of the day
        {  
            foreach (StatBuffData buffData in allDayNightbuffs)
            {
                if(buffData.timeState == timeState)
                {  // APPLY temporarily
                StatModData statModData = buffData.statModData;
                    charController.ChangeStat(statModData.causeType, statModData.causeName
                    , statModData.causeByCharID, statModData.statModified, statModData.modVal, true);
                }
                else
                {  // REMOVE temporarily
                    StatModData charModData = buffData.statModData;
                    charController.ChangeStat(charModData.causeType, charModData.causeName
                    , charModData.causeByCharID, charModData.statModified, -charModData.modVal, true);
                }
            }       
        }

        #endregion

        #region STAT ALT BUFF

        public List<StatAltBuffData> allStatAltBuffData = new List<StatAltBuffData>();

        int statBuffID = 0;
        public int ApplyStatRecAltBuff(float valPercent, StatName statModified, CauseType causeType, int causeName, int causeByCharID,
             TimeFrame timeFrame, int netTime, bool isBuff, bool isGain = false, CharStateName charStateName = CharStateName.None
                , TempTraitName tempTraitName = TempTraitName.None)
        {
            statBuffID++; 
            int startRoundNo = CombatEventService.Instance.currentRound;
      
            StatAltData statAltData = new StatAltData(statModified, valPercent, isGain);

            StatAltBuffData statAltBuffData = new StatAltBuffData(statBuffID, causeType, causeName, causeByCharID, isBuff,true, startRoundNo
                                                                 , timeFrame, netTime, statAltData);
            
            allStatAltBuffData.Add(statAltBuffData);
            return statBuffID;
        }
        public void EOCTickStatAltBuff()
        {
            foreach (StatAltBuffData statAltBuffData in allStatAltBuffData.ToList())
            {
                if (statAltBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveStatRecAltBuffData(statAltBuffData);
                }
            }
        }

        public void EORTick(int roundNo)  // to be completed
        {
            foreach (StatAltBuffData statAltBuffData in allStatAltBuffData.ToList())
            {
                if (statAltBuffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (statAltBuffData.buffCurrentTime >= statAltBuffData.buffedNetTime)
                    {
                        RemoveStatRecAltBuffData(statAltBuffData);
                    }
                    statAltBuffData.buffCurrentTime++;
                }
            }
        }

        void RemoveStatRecAltBuffData(StatAltBuffData statAltBuffData)
        {
            allStatAltBuffData.Remove(statAltBuffData);
        }
        public bool RemoveStatRecAltBuff(int statBuffID)
        {
            int index = allStatAltBuffData.FindIndex(t => t.dmgBuffID == statBuffID);
            if (index == -1) return false;
            StatAltBuffData statAltBuffData = allStatAltBuffData[index];
            RemoveStatRecAltBuffData(statAltBuffData);
            return true;
        }

        public float GetStatRecAltData(StatName statModified, bool isGain)
        {
            List<StatAltBuffData> statAltData_StatName = allStatAltBuffData.Where(t=>t.altData.statModified == statModified).ToList();
            List<StatAltBuffData> statAltData_IsPos = statAltData_StatName.Where(t=>t.altData.isGain == isGain).ToList();         
            
            float val = 0f; 
            if(statAltData_IsPos.Count > 0)
                foreach (StatAltBuffData statAltBuffData in statAltData_IsPos)
                {
                    val += statAltBuffData.altData.valPercent; 
                }
            return val; 
        }


        #endregion

    }
}
