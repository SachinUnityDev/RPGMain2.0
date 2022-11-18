using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq; 


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
        public CharModData charModData;
        public string directString;    


        //public BuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame, 
        //          int buffedNetTime, CharModData charModData)  // deprecated 
        //{
        //    this.buffID = buffID;
        //    this.isBuff = isBuff;   
        //    this.startRoundNo = startRoundNo;
        //    this.timeFrame = timeFrame;
        //    this.buffCurrentTime = 0; 
        //    this.buffedNetTime = buffedNetTime;
        //    this.charModData = charModData.DeepClone();
        //}

        public BuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame,
            int buffedNetTime, CharModData charModData, string directString = "")
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.charModData = charModData;
            this.directString = directString; 
        }
    }

    public class BuffController : MonoBehaviour
    {
         List<BuffData> allBuffs = new List<BuffData>();  
         List<BuffData> allDaybuffs = new List<BuffData>(); 
         List <BuffData> allNightbuffs = new List<BuffData>();  

        // use array here for the index to work 

        CharController charController; // ref to char Controller 
        [SerializeField]List<string> buffStrs = new List<string>();
        [SerializeField]List<string> deDuffStrs = new List<string>();

        public int buffIndex = 0; 
        void Start()
        {
            // should have feature of printing some data from skills directly
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR +=RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick; 

          //  QuestEventService.Instance.OnDayChange

        }
#region  APPLY_BUFFS 
        public int ApplyBuff(CauseType causeType, int causeName, int causeByCharID
                                , StatsName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            // Actual buff application 
            CharModData charModVal =  charController.ChangeStat( causeType,  causeName, causeByCharID
                                            ,  statName,  value, true);
            int currRd = CombatService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex,isBuff, currRd, timeFrame, netTime,
                                                                  charModVal, directStr);                
                allBuffs.Add(buffData);               
                return buffIndex;         
        }

        public int ApplyBuffOnRange(CauseType causeType, int causeName, int causeByCharID, StatsName statName
            , float minChgR, float maxChgR, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            CharModData charModData = charController.ChangeStatRange(causeType, causeName, causeByCharID
                                           , statName, minChgR, maxChgR,  true);

            int currRd = CombatService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
                                                            charModData, directStr);            
            allBuffs.Add(buffData);           
            return buffIndex; 
        }

        bool IsRangeChange(BuffData buffData)
        {
            if (buffData.charModData.modChgMinR == 0 &&
                buffData.charModData.modChgMaxR == 0)
                return false;
            else
                return true; 
        }
#endregion

#region REMOVE BUFFS 
        public bool RemoveBuff(int buffID)
        {
            int index = allBuffs.FindIndex(t => t.buffID == buffID);
            if (index == -1) return false; 
            BuffData buffData = allBuffs[index];
            RemoveBuffData(buffData);
            return true;
        }
        public void RemoveBuffData(BuffData buffData)
        {
            if (IsRangeChange(buffData))
            { 
                charController.ChangeStatRange(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified
                                     , -buffData.charModData.modChgMinR, -buffData.charModData.modChgMinR,true);

            }else
            {
                charController.ChangeStat(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified, -buffData.charModData.modCurrVal, true);
            }
            allBuffs.Remove(buffData);
        }


#endregion
        public List<string> GetBuffList()
        {  
            foreach (BuffData buffData in allBuffs)
            {  
                if(buffData.isBuff)
                    buffStrs.Add(buffData.directString);  
            }
            return buffStrs;            
        }
        public List<string> GetDeBuffList()
        {
            foreach (BuffData buffData in allBuffs)
            {
                if (!buffData.isBuff)
                    deDuffStrs.Add(buffData.directString); 
            }
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

#region DAY BUFF MGMT
        public int ApplyNInitBuffOnDay(CauseType causeType, int causeName, int causeByCharID
                               , StatsName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            
            
           CharModData charModVal = charController.ChangeStat(causeType, causeName, causeByCharID
                                                        , statName, value, true);
           
            if(CalendarService.Instance.timeState == TimeState.Night) // FOR NIGHT CORRECTION
            {
                charController.ChangeStat(causeType, causeName, causeByCharID
                                                        , statName, -value, true);  
            }

            int currRd = CombatService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
                                                                  charModVal, directStr);

            allBuffs.Add(buffData);
            allDaybuffs.Add(buffData);
            return buffIndex;

        }

        public int ApplyNInitBuffOnDayRange(CauseType causeType, int causeName, int causeByCharID, StatsName statName
            , float minChgR, float maxChgR, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {

            CharModData charModData = charController.ChangeStatRange(causeType, causeName, causeByCharID
                                           , statName, minChgR, maxChgR, true);

            if (CalendarService.Instance.timeState == TimeState.Night) // FOR NIGHT CORRECTION
            {
                charController.ChangeStatRange(causeType, causeName, causeByCharID
                                            , statName, -minChgR, -maxChgR, true);
            }
            int currRd = CombatService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex,isBuff, currRd, timeFrame, netTime,
                                                            charModData, directStr);
            allBuffs.Add(buffData);
            allDaybuffs.Add(buffData);  
            return buffIndex;
        }

        void ToggleBuffsOnStartOfTheDay() // ON start of the day
        {
            foreach (BuffData buffData in allDaybuffs)
            {
                CharModData charModData = buffData.charModData; 
                if(charModData.modChgMinR == 0 || charModData.modChgMaxR == 0)
                {
                    charController.ChangeStat(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, charModData.modCurrVal
                   , true);
                }
                else
                {
                    charController.ChangeStatRange(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, charModData.modChgMinR
                   , charModData.modChgMaxR, true);
                }
            }

            foreach (BuffData buffData in allNightbuffs)
            {
                CharModData charModData = buffData.charModData;
                if (charModData.modChgMinR == 0 || charModData.modChgMaxR == 0)
                {
                    charController.ChangeStat(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, -charModData.modCurrVal
                   , true);
                }
                else
                {
                    charController.ChangeStatRange(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, -charModData.modChgMinR
                   , -charModData.modChgMaxR, true);
                }
            }
        }
        void ToggleBuffsOnStartOfTheNight() // ON start of the Night
        {
            foreach (BuffData buffData in allNightbuffs)
            {
                CharModData charModData = buffData.charModData;
                if (charModData.modChgMinR == 0 || charModData.modChgMaxR == 0)
                {
                    charController.ChangeStat(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, charModData.modCurrVal
                   , true);
                }
                else
                {
                    charController.ChangeStatRange(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, charModData.modChgMinR
                   , charModData.modChgMaxR, true);
                }
            }

            foreach (BuffData buffData in allDaybuffs)
            {
                CharModData charModData = buffData.charModData;
                if (charModData.modChgMinR == 0 || charModData.modChgMaxR == 0)
                {
                    charController.ChangeStat(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, -charModData.modCurrVal
                   ,  true);
                }
                else
                {
                    charController.ChangeStatRange(charModData.causeType, charModData.causeName
                   , charModData.causeByCharID, charModData.statModified, -charModData.modChgMinR
                   , -charModData.modChgMaxR, true);
                }
            }
        }

        public void RemoveBuffOnDay(BuffData buffData)
        {
            if (IsRangeChange(buffData))
            {
                charController.ChangeStatRange(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified
                                     , -buffData.charModData.modChgMinR, -buffData.charModData.modChgMinR, true);

            }
            else
            {
                charController.ChangeStat(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified, -buffData.charModData.modCurrVal, true);
            }
            allBuffs.Remove(buffData);
            allDaybuffs.Remove(buffData);
        }
 
        void RemoveBuffOnEndofDay()
        {
            foreach (BuffData buffData in allDaybuffs)
            {
                RemoveBuffDay(buffData);
            }
        }

        void ApplyBuffDay(BuffData buffData)
        {
            CharModData charModData = buffData.charModData;

            if (IsRangeChange(buffData))
            {
                charController.ChangeStatRange(charModData.causeType, charModData.causeName, charModData.causeByCharID
                                        , charModData.statModified, charModData.modChgMinR, charModData.modChgMaxR, true);
            }
            else
            {
                charController.ChangeStat(charModData.causeType, charModData.causeName, charModData.causeByCharID
                                        , charModData.statModified, charModData.modCurrVal, true);
            }
        }

        void RemoveBuffDay(BuffData buffData)
        {
            CharModData charModData = buffData.charModData;

            if (IsRangeChange(buffData))
            {
                charController.ChangeStatRange(charModData.causeType, charModData.causeName, charModData.causeByCharID
                                        , charModData.statModified, -charModData.modChgMinR, -charModData.modChgMaxR, true);
            }
            else
            {
                charController.ChangeStat(charModData.causeType, charModData.causeName, charModData.causeByCharID
                                        , charModData.statModified, -charModData.modCurrVal, true);
            }
        }

        #endregion

#region NIGHT BUFF MGMT
        public int ApplyBuffOnNight(CauseType causeType, int causeName, int causeByCharID
                               , StatsName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {

            // Actual buff application 
            CharModData charModVal = charController.ChangeStat(causeType, causeName, causeByCharID
                                            , statName, value, true);

            int currRd = CombatService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex,isBuff, currRd, timeFrame, netTime,
                                                                  charModVal, directStr);


            allBuffs.Add(buffData);
            allNightbuffs.Add(buffData);
            return buffIndex;

        }

        public int ApplyBuffOnNightRange(CauseType causeType, int causeName, int causeByCharID, StatsName statName
            , float minChgR, float maxChgR, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            CharModData charModData = charController.ChangeStatRange(causeType, causeName, causeByCharID
                                           , statName, minChgR, maxChgR, true);

            int currRd = CombatService.Instance.currentRound;
            buffIndex++;
            BuffData buffData = new BuffData(buffIndex,isBuff, currRd, timeFrame, netTime,
                                                            charModData, directStr);
            allBuffs.Add(buffData);
            allNightbuffs.Add(buffData);
            return buffIndex;
        }

        public void RemoveBuffOnNight(BuffData buffData)
        {
            if (IsRangeChange(buffData))
            {
                charController.ChangeStatRange(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified
                                     , -buffData.charModData.modChgMinR, -buffData.charModData.modChgMinR, true);

            }
            else
            {
                charController.ChangeStat(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified, -buffData.charModData.modCurrVal, true);
            }
            allBuffs.Remove(buffData);
            allNightbuffs.Remove(buffData);
        }

#endregion

        public int ApplyBuffExpExtra(CauseType causeType, int causeName, int causeByCharID
                                , float valPercent, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {



            return 0; 
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


    }
}
//string str = $"{buffData.charModData.modifiedVal} {buffData.charModData.statModfified}, {buffData.buffedNetTime}  rds";
//buffStrs.Add(str);


//string str = $"{buffData.charModData.modifiedVal} {buffData.charModData.statModfified}, {buffData.buffedNetTime}  rds";
//deDuffStrs.Add(str);


//if(directStr == "")
//{
//    BuffData buffData = new BuffData(isBuff, currRd, timeFrame, netTime,
//                                                      charModVal);
//    allBuffs.Add(buffData);
//}
//else
//{
//}