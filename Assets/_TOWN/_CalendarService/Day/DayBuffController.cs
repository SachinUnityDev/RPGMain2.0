using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{

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
    

   

    public class DayBuffController : MonoBehaviour
    {
        public List<DayBuffData> allBuffData = new List<DayBuffData>();
        CharController charController;

        private void Start()
        {
            charController = GetComponent<CharController>();
            CalendarService.Instance.OnStartOfCalDay += (int day)=>ToggleDayBuff();
        }

        public int ApplyDayBuff(CauseType causeType, int causeName, int causebyCharID, DayName dayName
                                            , AttribName attribName, int valChg, int timeVal, bool isBuff)
        {
           
            CauseData causeData = new CauseData(causeType, causeName, causebyCharID, charController.charModel.charID);

            int buffID =
                    charController.buffController.ApplyBuff(causeType, (int)causeName, causebyCharID,
                                            attribName, valChg, TimeFrame.Infinity, -1, isBuff);

            DayBuffData dayBuffData = new DayBuffData(causeData, buffID, dayName, attribName
                                                        , valChg, TimeFrame.EndOfDay, timeVal, isBuff);

            allBuffData.Add(dayBuffData);
            return buffID;
        }
        public void ToggleDayBuff()
        {
            DayName dayName = CalendarService.Instance.currDayName; 

            foreach (DayBuffData buff in allBuffData)
            {
                if (buff.dayName == dayName)
                {
                    ApplyDayBuff(buff.causeData.causeType, buff.causeData.causeName
                        , buff.causeData.causeByCharID, dayName, buff.attribName,buff.valChg
                        , buff.timeVal, buff.isBuff);
                }
                else
                {
                    RemoveBuffToggle(buff.buffID);
                }
            }
        }

        void RemoveBuffToggle(int buffID)
        {
            charController.buffController.RemoveBuff(buffID);   
        }
        public void RemoveBuffPerma(int buffID)
        {
            charController.buffController.RemoveBuff(buffID);
            int index = allBuffData.FindIndex(t=>t.buffID == buffID);

            if(index != -1)
                allBuffData.RemoveAt(index);    
            else
                Debug.Log("buff not found" + buffID);

        }




    }
}