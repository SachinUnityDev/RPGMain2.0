using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class BuffModel
    {
        public int charID = -1; // the charID of the char this buffModel is attached to
        public List<BuffData> allBuffs;
        public List<BuffData> allDayNightbuffs;
        public List<PosBuffData> allPosBuffs; 

        public List<OnSOCBuffData> allBuffOnSOC;


        public List<string> buffStrs;
        public List<string> deDuffStrs;

        public int buffIndex = 0;
        public int SOCBuffIndex = 0;

        public BuffModel()
        {
            allBuffs = new List<BuffData>();
            allDayNightbuffs = new List<BuffData>();
            allPosBuffs = new List<PosBuffData>();
            allBuffOnSOC = new List<OnSOCBuffData>();
            buffStrs = new List<string>();
            deDuffStrs = new List<string>();
        }
        public BuffModel(int charID)
        {
            this.charID = charID;
            allBuffs = new List<BuffData>();
            allDayNightbuffs = new List<BuffData>();
            allPosBuffs = new List<PosBuffData>();
            allBuffOnSOC = new List<OnSOCBuffData>();
            buffStrs = new List<string>();
            deDuffStrs = new List<string>();
        }
    }
}