using Combat;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public class StatBuffController : MonoBehaviour, ISaveable
    {

        public StatBuffModel statBuffModel; 
        CharController charController;
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
        public void InitOnLoad(StatBuffModel statbuffModel)
        {
            this.statBuffModel = statbuffModel.DeepClone();
        }
        public void Init()
        {
            if (statBuffModel == null)
            {
                charController = GetComponent<CharController>();
                int charID = charController.charModel.charID;
                statBuffModel = new StatBuffModel(charID); //pass in char Id      
            }
        }
        #region  APPLY_BUFFS 

        public int ApplyNInitBuffOnDay(CauseType causeType, int causeName, int causeByCharID
                              , StatName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {

            StatModData statModData = charController.ChangeStat(causeType, causeName, causeByCharID
                                                         , statName, value, true);

            if (CalendarService.Instance.calendarModel.currtimeState == TimeState.Night) // FOR NIGHT CORRECTION
            {
                charController.ChangeStat(causeType, causeName, causeByCharID
                                                        , statName, -value, true);
            }
            int currRd = CombatEventService.Instance.currentRound;
            buffIndex++;

            StatBuffData buffData = new StatBuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
                                                                   statModData);
            if (statBuffModel == null)
            {
                Init();
            }
            //allBuffs.Add(buffData);
            statBuffModel.allDayNightbuffs.Add(buffData);
            return buffIndex;

        }
        void ToggleBuffsOnTimeStateChg(TimeState timeState) // ON start of the day
        {
            if (statBuffModel == null)
            {
                Init();
            }
            if (statBuffModel.allDayNightbuffs.Count>0)
            foreach (StatBuffData buffData in statBuffModel.allDayNightbuffs)
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

        int statBuffID = 0;

        public ServicePath servicePath => ServicePath.BuffService;

        public int ApplyStatRecAltBuff(float valPercent, StatName statModified, CauseType causeType, int causeName, int causeByCharID,
             TimeFrame timeFrame, int netTime, bool isBuff, bool isGain = false, CharStateName charStateName = CharStateName.None
                , TempTraitName tempTraitName = TempTraitName.None)
        {
            statBuffID++; 
            int startRoundNo = CombatEventService.Instance.currentRound;
      
            StatAltData statAltData = new StatAltData(statModified, valPercent, isGain);

            StatAltBuffData statAltBuffData = new StatAltBuffData(statBuffID, causeType, causeName, causeByCharID, isBuff,true, startRoundNo
                                                                 , timeFrame, netTime, statAltData);
            if (statBuffModel == null)
            {
                Init();
            }
            statBuffModel.allStatAltBuffData.Add(statAltBuffData);
            return statBuffID;
        }
        public void EOCTickStatAltBuff()
        {
            foreach (StatAltBuffData statAltBuffData in statBuffModel.allStatAltBuffData.ToList())
            {
                if (statAltBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveStatRecAltBuffData(statAltBuffData);
                }
            }
        }

        public void EORTick(int roundNo)  // to be completed
        {
            foreach (StatAltBuffData statAltBuffData in statBuffModel.allStatAltBuffData.ToList())
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
            statBuffModel. allStatAltBuffData.Remove(statAltBuffData);
        }
        public bool RemoveStatRecAltBuff(int statBuffID)
        {
            int index = statBuffModel.allStatAltBuffData.FindIndex(t => t.dmgBuffID == statBuffID);
            if (index == -1) return false;
            StatAltBuffData statAltBuffData = statBuffModel.allStatAltBuffData[index];
            RemoveStatRecAltBuffData(statAltBuffData);
            return true;
        }

        public float GetStatRecAltData(StatName statModified, bool isGain)
        {
            List<StatAltBuffData> statAltData_StatName = statBuffModel.allStatAltBuffData.Where(t=>t.altData.statModified == statModified).ToList();
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
        #region SAVE_LOAD   
        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string statBuffPath = path + "/StatBuff/";

            ClearState();
            string buffModelJSON = JsonUtility.ToJson(statBuffModel);
            string fileName = statBuffPath + charController.charModel.charName + ".txt";
            File.WriteAllText(fileName, buffModelJSON);
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string statBuffPath = path + "/StatBuff/";
            charController = GetComponent<CharController>();
            if (SaveService.Instance.DirectoryExists(statBuffPath))
            {
                string[] fileNames = Directory.GetFiles(statBuffPath);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    if (fileName.Contains(charController.charModel.charName.ToString()))
                    {
                        string contents = File.ReadAllText(fileName);
                        StatBuffModel statBuffModel = JsonUtility.FromJson<StatBuffModel>(contents);
                        InitOnLoad(statBuffModel);
                    }
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void ClearState()
        {
            // clear only specific file in the given path
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/StatBuff/";
            string[] fileNames = Directory.GetFiles(path);

            foreach (string fileName in fileNames)
            {
                if ((fileName.Contains(".meta")) &&
                 (fileName.Contains(charController.charModel.charName.ToString())))
                    File.Delete(fileName);
            }
        }
        #endregion
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveState();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadState();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                ClearState();
            }
        }
    }
}
