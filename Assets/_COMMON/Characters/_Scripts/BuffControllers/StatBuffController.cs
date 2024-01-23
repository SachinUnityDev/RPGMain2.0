using Combat;
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
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
        }
    }
    public class StatAltData
    {
        public StatName statModified;
        public AttackType attackType = AttackType.None;
        public DamageType damageType = DamageType.None;
        public CultureType cultType = CultureType.None;
        public RaceType raceType = RaceType.None;

        public float valPercent = 0f;

        public StatAltData(StatName statModified, float valPercent, AttackType attackType, DamageType damageType,
                                CultureType cultType, RaceType raceType)
        {
            this.statModified = statModified;

            this.attackType = attackType; 
            this.damageType = damageType;  

            this.cultType = cultType;
            this.raceType = raceType;
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

        #region DAMAGE RECEIVE BUFF ALTERER

        public List<StatAltBuffData> allStatAltBuffData = new List<StatAltBuffData>();

        int statBuffID = 0;
        public int ApplyStatReceivedAltBuff(float valPercent, StatName statModified, CauseType causeType, int causeName, int causeByCharID,
             TimeFrame timeFrame, int netTime, bool isBuff, AttackType attackType =AttackType.None,  DamageType damageType = DamageType.None,          
            CultureType cultType = CultureType.None, RaceType raceType = RaceType.None)
        {
            statBuffID = allStatAltBuffData.Count + 1;
            int startRoundNo = CombatEventService.Instance.currentRound;
      
            StatAltData statAltData = new StatAltData(statModified, valPercent,attackType
                                                        , damageType,  cultType , raceType);


            StatAltBuffData statAltBuffData = new StatAltBuffData(statBuffID, causeType, causeName, causeByCharID, isBuff,true, startRoundNo, timeFrame
                            , netTime, statAltData);
            
            allStatAltBuffData.Add(statAltBuffData);
            return statBuffID;
        }
        public void EOCTickStatAltBuff()
        {
            foreach (StatAltBuffData statAltBuffData in allStatAltBuffData.ToList())
            {
                if (statAltBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveStatAltBuffData(statAltBuffData);
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
                        RemoveStatAltBuffData(statAltBuffData);
                    }
                    statAltBuffData.buffCurrentTime++;
                }
            }
        }

        void RemoveStatAltBuffData(StatAltBuffData statAltBuffData)
        {
            allStatAltBuffData.Remove(statAltBuffData);
        }
        public bool RemoveStatAltBuff(int dmgBuffID)
        {
            int index = allStatAltBuffData.FindIndex(t => t.dmgBuffID == dmgBuffID);
            if (index == -1) return false;
            StatAltBuffData statAltBuffData = allStatAltBuffData[index];
            RemoveStatAltBuffData(statAltBuffData);
            return true;
        }

        public float GetStatReceivedAlt(CharModel strikerModel, StatName statModified, float valChg, AttackType attackType = AttackType.None
                                         , DamageType damageType = DamageType.None)
        {
            // 20% physical attack against beastmen            
            bool isPositive = false; 
            if(valChg >= 0 )
                isPositive= true;
            else isPositive= false;


            foreach (StatAltBuffData statAltBuffData in allStatAltBuffData.ToList())
            {
                StatAltData statAltData = statAltBuffData.altData;
                if (statAltData.damageType != DamageType.None && statAltData.damageType == damageType)// Damage Type Block 
                {
                    float val = 0;
                    if (statAltData.raceType != RaceType.None
                        && statAltData.raceType == strikerModel.raceType
                                && statAltData.cultType != CultureType.None
                                && statAltData.cultType == strikerModel.cultType)
                    {

                        val = statAltData.valPercent; // COMBO RACE AND CULT

                    }
                    else   // NOT A COMBO OF RACE AND CULT
                    {
                        if (statAltData.raceType != RaceType.None
                                  && statAltData.raceType == strikerModel.raceType)
                        {
                            val = statAltData.valPercent;
                        }
                        if (statAltData.cultType != CultureType.None
                                        && statAltData.cultType == strikerModel.cultType)
                        {
                            val = statAltData.valPercent;
                        }
                    }
                    return val;
                }
                else if (statAltData.attackType != AttackType.None && statAltData.attackType == attackType)// Attack type block
                {
                    float val = 0;
                    if (statAltData.raceType != RaceType.None
                        && statAltData.raceType == strikerModel.raceType
                                && statAltData.cultType != CultureType.None
                                && statAltData.cultType == strikerModel.cultType)
                    {

                        val = statAltData.valPercent; // COMBO RACE AND CULT

                    }
                    else   // NOT A COMBO OF RACE AND CULT
                    {
                        if (statAltData.raceType != RaceType.None
                                  && statAltData.raceType == strikerModel.raceType)
                        {
                            val = statAltData.valPercent;
                        }
                        if (statAltData.cultType != CultureType.None
                                        && statAltData.cultType == strikerModel.cultType)
                        {
                            val = statAltData.valPercent;
                        }
                    }
                    return val;
                }
                else if (statAltData.attackType == AttackType.None && statAltData.damageType == DamageType.None)
                {
                    return statAltData.valPercent;
                }
            }
            return 0f;
        
        }

        #endregion

    }
}
