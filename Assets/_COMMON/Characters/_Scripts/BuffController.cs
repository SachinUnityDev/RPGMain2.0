using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using common;
using Quest;

namespace Combat
{
    [System.Serializable]
    public class BuffData
    {
        public int buffID; 
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;  
        public AttribModData attribModData;
        public TimeState timeState; 

        public BuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame,
            int buffedNetTime, AttribModData attribModData, TimeState timeState =  TimeState.None)
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.attribModData = attribModData;
            this.timeState = timeState; 
           
        }
    }

    public class BuffController : MonoBehaviour
    {
         List<BuffData> allBuffs = new List<BuffData>();  
         List<BuffData> allDayNightbuffs = new List<BuffData>(); 
      
        CharController charController; // ref to char Controller 
        [SerializeField]List<string> buffStrs = new List<string>();
        [SerializeField]List<string> deDuffStrs = new List<string>();

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
                                , AttribName attribName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {
           
            AttribModData attribModVal =  charController.ChangeAttrib( causeType,  causeName, causeByCharID
                                            ,  attribName,  value, true);
            int currRd = GameSupportService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex,isBuff, currRd, timeFrame, netTime,
                                                                    attribModVal);                
                allBuffs.Add(buffData);               
                return buffIndex;         
        }

        public void IncrBuffCastTime(int buffID, int incrBy)
        {
            foreach (BuffData buff in allBuffs)
            {
                if(buff.buffID == buffID)
                {
                    buff.buffedNetTime += incrBy; 
                }
            }
        }
   
        bool IsDmgArmorChg(BuffData buffData)
        {
            if (buffData.attribModData.attribModified == AttribName.dmgMin ||
                buffData.attribModData.attribModified == AttribName.dmgMax ||
                buffData.attribModData.attribModified == AttribName.armorMin||
                buffData.attribModData.attribModified == AttribName.armorMax)
                return true;
            else
                return false;                 
        }

        #endregion

        #region REMOVE BUFFS 
        public bool RemoveBuff(int buffID)   // to be revised
        {
            BuffData buffData = null; 
            int index = allBuffs.FindIndex(t => t.buffID == buffID);
            if (index == -1)
            {
                index = allDayNightbuffs.FindIndex(t => t.buffID == buffID); 
                if(index == -1)
                {
                    return false;
                }
                else // remove day buff
                {
                    buffData = allDayNightbuffs[index];
                    allDayNightbuffs.Remove(buffData);
                    return true; 
                } 
            }
            else
            {
                buffData = allBuffs[index];
                RemoveBuffData(buffData);
                return true; 
            }                
        }
        public void RemoveBuffData(BuffData buffData)
        {        
                charController.ChangeAttrib(buffData.attribModData.causeType,
                                        buffData.attribModData.causeName, buffData.attribModData.causeByCharID
                                        , buffData.attribModData.attribModified, -buffData.attribModData.modCurrVal, true);
                   
                allBuffs.Remove(buffData);
        }
        #endregion
        public List<string> GetBuffList()
        {  
            //foreach (BuffData buffData in allBuffs)
            //{  
            //    if(buffData.isBuff)
            //        buffStrs.Add(buffData.directString);  
            //}
            return buffStrs;            
        }
        public List<string> GetDeBuffList()
        {
            //foreach (BuffData buffData in allBuffs)
            //{
            //    if (!buffData.isBuff)
            //        deDuffStrs.Add(buffData.directString); 
            //}
            return deDuffStrs;          
        }
        public void RoundTick()
        {
            foreach (BuffData buffData in allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (buffData.buffCurrentTime >= buffData.buffedNetTime)
                    {
                        RemoveBuffData(buffData);
                    }
                    buffData.buffCurrentTime++;
                }
            }
        }
        public void EOCTick()
        {
            foreach (BuffData buffData in allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveBuffData(buffData);
                }
            }
        }
        public void EOQTick()
        {
            foreach (BuffData buffData in allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfQuest)
                {
                    RemoveBuffData(buffData);
                }
            }
        }

        #region DAY BUFF MGMT
        public int ApplyNInitBuffOnDayNNight(CauseType causeType, int causeName, int causeByCharID
                               , AttribName statName, float value, TimeFrame timeFrame, int netTime
                                , bool isBuff, TimeState timeState)
        {

            AttribModData attribModVal = new AttribModData();

            if (CalendarService.Instance.currtimeState == TimeState.Day) // FOR DAY CORRECTION
            {
                attribModVal = charController.ChangeAttrib(causeType, causeName, causeByCharID
                                                        , statName, value, true);
            }
            if(CalendarService.Instance.currtimeState == TimeState.Night) // FOR NIGHT CORRECTION
            {
                attribModVal = charController.ChangeAttrib(causeType, causeName, causeByCharID
                                                        , statName, -value, true);  
            }
            int currRd = GameSupportService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
                                                                  attribModVal, timeState);

           // allBuffs.Add(buffData);
            allDayNightbuffs.Add(buffData);
            return buffIndex;
        }

        void ToggleBuffsOnTimeStateChg(TimeState timeState) // ON start of the day
        {
            foreach (BuffData buffData in allDayNightbuffs)
            {
                if (buffData.timeState == timeState)
                {  // APPLY temporarily
                    AttribModData attribModData = buffData.attribModData;
                    charController.ChangeAttrib(attribModData.causeType, attribModData.causeName
                    , attribModData.causeByCharID, attribModData.attribModified, attribModData.modCurrVal, true);
                }
                else
                {  // REMOVE temporarily
                    AttribModData attribModData = buffData.attribModData;
                    charController.ChangeAttrib(attribModData.causeType, attribModData.causeName
                    , attribModData.causeByCharID, attribModData.attribModified, -attribModData.modCurrVal, true);
                }
            }
        }
        #endregion


    }
}
