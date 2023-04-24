using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Town
{
    [Serializable]
    public class DayModel
    {
        public int orderInSeq;
        public DayName dayName;
        public DayName2 dayName2;
        public string dayNameStr;
        public string dayDescription;
        [TextArea(10, 15)]
        public List<string> tipOfTheDayList = new List<string>();
        public string daySpecs = "";
        public DayModel(DaySO daySO )
        {
            this.orderInSeq = daySO.orderInSeq;
            this.dayName = daySO.dayName;
            this.dayName2 = daySO.dayName2;
            this.dayNameStr = daySO.dayNameStr;
            this.dayDescription = daySO.dayDescription;            
        }
    }
}