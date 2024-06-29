using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class WeekModel
    {
        public WeekEventsName weekName;
        public int weekCount;
        public string weekNameStr;
        public string weekDesc;
        public List<string> WeekSpecs;
        public bool isDayBonusReceived = false; 
        public WeekModel(WeekSO weekSO)
        {
            this.weekName = weekSO.weekName;
            this.weekCount = weekSO.weekCount;
            this.weekNameStr = weekSO.weekNameStr;
            this.weekDesc = weekSO.weekDesc;
            WeekSpecs = new List<string>();
            if(weekSO.WeekSpecs.Count>0)
            this.WeekSpecs = weekSO.WeekSpecs.DeepClone();
        }
    }
}