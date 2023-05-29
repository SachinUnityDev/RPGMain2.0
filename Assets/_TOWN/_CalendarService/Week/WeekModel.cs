using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class WeekModel
    {
        public List<int> orderInSeq = new List<int>();
        public WeekEventsName weekName;
        public int weekCount;
        public string weekNameStr;
        public string weekDesc;
        public List<string> WeekSpecs = new List<string>();

        public WeekModel(WeekSO weekSO)
        {
            this.orderInSeq = weekSO.orderInSeq.DeepClone();
            this.weekName = weekSO.weekName;
            this.weekCount = weekSO.weekCount;
            this.weekNameStr = weekSO.weekNameStr;
            this.weekDesc = weekSO.weekDesc;
            this.WeekSpecs = weekSO.WeekSpecs.DeepClone();
        }
    }
}