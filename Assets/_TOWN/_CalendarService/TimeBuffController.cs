using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;
using System.Linq;

namespace Common
{
    [Serializable]
    public class DayBuffData
    {
        public CauseData causeData;
        public int buffID;
        public DayName dayName;
        public AttribName attribName;
        public int valChg;
        public int minValChg;
        public int maxValChg;
        public TimeFrame timeFrame;
        public int timeVal;
        public int currTime = 0; 
        public bool isBuff;

        public DayBuffData(CauseData causeData, int buffID, DayName dayName, AttribName attribName
            , int valChg, TimeFrame timeFrame, int timeVal, bool isBuff)
        {
            this.causeData = causeData;
            this.buffID = buffID;
            this.dayName = dayName;
            this.attribName = attribName;
            this.valChg = valChg;
            this.timeFrame = timeFrame;
            this.timeVal = timeVal;
            this.isBuff = isBuff;
        }

        public DayBuffData(CauseData causeData, int buffID, DayName dayName, AttribName attribName
                            , int minValChg, int maxValChg, TimeFrame timeFrame, int timeVal, bool isBuff)
        {
            this.causeData = causeData;
            this.buffID = buffID;
            this.dayName = dayName;
            this.attribName = attribName;
            this.minValChg = minValChg;
            this.maxValChg = maxValChg;
            this.timeFrame = timeFrame;
            this.timeVal = timeVal;
            this.isBuff = isBuff;
        }
    }
    public class TimeBuffController : MonoBehaviour
    {
        public List<DayBuffData> allBuffData = new List<DayBuffData>();
        CharController charController;

        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDay += (int day) => DayTick();
            charController = GetComponent<CharController>();
        }
        public int ApplyDayBuff(CauseType causeType, int causeName, int causebyCharID, DayName dayName
                               , AttribName attribName, int valChg, int timeVal, bool isBuff)
        {

            CauseData causeData = new CauseData(causeType, causeName, causebyCharID, charController.charModel.charID);

            int buffID =
                    charController.buffController.ApplyBuff(causeType, (int)causeName, causebyCharID,
                                                     attribName, valChg, TimeFrame.EndOfDay, timeVal, isBuff);

            DayBuffData dayBuffData = new DayBuffData(causeData, buffID, dayName, attribName
                                                        , valChg, TimeFrame.EndOfDay, timeVal, isBuff);
            dayBuffData.currTime++; 
            allBuffData.Add(dayBuffData);
            return buffID;
        }
        public void DayTick()
        {
            foreach (DayBuffData buff in allBuffData.ToList())
            {
                if (buff.timeFrame == TimeFrame.EndOfDay && buff.currTime >= buff.timeVal)
                {                
                    RemoveBuffPerma(buff.buffID);
                }
                else
                {
                    buff.currTime++; 
                }
            }
        }
        // create a day tick... 
        void RemoveBuffFX(int buffID)
        {
            charController.buffController.RemoveBuff(buffID);
        }
        public void RemoveBuffPerma(int buffID)
        {
            RemoveBuffFX(buffID);
            int index = allBuffData.FindIndex(t => t.buffID == buffID);
            if (index != -1)
                allBuffData.RemoveAt(index);
            else
                Debug.Log("buff not found" + buffID);
        }
    }
}