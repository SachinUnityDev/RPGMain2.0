using Combat;
using common;
using Quest;
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

    public class StatBuffController : MonoBehaviour
    {

        List<StatBuffData> allBuffs = new List<StatBuffData>();
        List<StatBuffData> allDayNightbuffs = new List<StatBuffData>();       
        // use array here for the index to work 

        CharController charController; // ref to char Controller 
        [SerializeField] List<string> buffStrs = new List<string>();
        [SerializeField] List<string> deDuffStrs = new List<string>();

        public int buffIndex = 0;

        private void Awake()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick;
            QuestEventService.Instance.OnEOQ += EOQTick;
            CalendarService.Instance.OnChangeTimeState += ToggleBuffsOnTimeStateChg; 
        }
        void Start()
        {
          
          
        }

        #region  APPLY_BUFFS 
        public int ApplyBuff(CauseType causeType, int causeName, int causeByCharID
                                , StatName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {

            StatModData statModData = charController.ChangeStat(causeType, causeName, causeByCharID
                                            , statName, value, true);
            int currRd = GameSupportService.Instance.currentRound;
            buffIndex++;
            StatBuffData statBuffData = new StatBuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
                                                                    statModData);
            allBuffs.Add(statBuffData);
            return buffIndex;
        }

        public bool RemoveStatBuff(int buffID)   // to be revised
        {
            StatBuffData statbuffData = null;
            int index = allBuffs.FindIndex(t => t.buffID == buffID);
            if (index == -1)
            {
                index = allDayNightbuffs.FindIndex(t => t.buffID == buffID);
                if (index == -1)
                {
                    return false; 
                }
                else // remove day buff
                {
                    statbuffData = allDayNightbuffs[index];
                    allDayNightbuffs.Remove(statbuffData);
                    return true;
                }
            }
            else
            {
                statbuffData = allBuffs[index];
                RemoveStatBuffData(statbuffData);
                return true;
            }
        }
        public void RemoveStatBuffData(StatBuffData statbuffData)
        {   
             charController.ChangeStat(statbuffData.statModData.causeType,
                                    statbuffData.statModData.causeName, statbuffData.statModData.causeByCharID
                                    , statbuffData.statModData.statModified, -statbuffData.statModData.modVal, true);
            
            allBuffs.Remove(statbuffData);
        }
        #endregion
     
        public void RoundTick()
        {
            foreach (StatBuffData buffData in allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (buffData.buffCurrentTime >= buffData.buffedNetTime)
                    {
                        RemoveStatBuffData(buffData);
                    }
                    buffData.buffCurrentTime++;
                }
            }
        }

        public void EOCTick()
        {
            foreach (StatBuffData buffData in allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveStatBuffData(buffData);
                }
            }
        }

        public void EOQTick()
        {
            foreach (StatBuffData buffData in allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfQuest)
                {
                    RemoveStatBuffData(buffData);
                }
            }
        }

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
            int currRd = GameSupportService.Instance.currentRound;
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
    }
}