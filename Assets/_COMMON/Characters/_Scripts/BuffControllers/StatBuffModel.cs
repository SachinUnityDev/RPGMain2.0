using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class StatBuffModel
    {
        public int charID = -1 ;
        [Header("Stat Buff")]
        public List<StatAltBuffData> allStatAltBuffData;

        [Header("all Day and Night Buff")]
        public List<StatBuffData> allDayNightbuffs;
        // use array here for the index to work 

        public List<string> buffStrs;
        public List<string> deDuffStrs;

        public StatBuffModel(int charID)
        {
            this.charID = charID;
            allStatAltBuffData = new List<StatAltBuffData>();
            allDayNightbuffs = new List<StatBuffData>();
            buffStrs = new List<string>();
            deDuffStrs = new List<string>();

        }   

    }
}
