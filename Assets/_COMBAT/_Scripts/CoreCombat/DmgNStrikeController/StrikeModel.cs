using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 



namespace Combat
{
    public class ThornBuffData
    {
        public int thornID = -1;      
        public DamageType damageType;  // damage attackers will be reverted by
        public float thornsMin;
        public float thornsMax;
        public int rdCount;
        public TimeFrame timeFrame = TimeFrame.None;
        public int currentTime =-1;
        public int castTime = 0; 

        public ThornBuffData(int thornID, DamageType damageType, float thornsMin, float thornsMax
                                    , TimeFrame timeframe, int castTime)
        {
            this.thornID = thornID;            
            this.damageType = damageType;
            this.thornsMin = thornsMin;
            this.thornsMax = thornsMax;
            this.timeFrame = timeframe; 
            this.castTime = castTime;            
        }
    }
    public class RetaliateBuffData
    {
        public int retaliateID = -1;
        CauseType causeType;
        int causeName; 
        public TimeFrame timeFrame = TimeFrame.None;
        public int currentTime = -1;
        public int castTime = 0;

        public RetaliateBuffData(int retaliateID, CauseType causeType, int causeName, TimeFrame timeFrame, int castTime)
        {
            this.retaliateID = retaliateID;
            this.causeType = causeType; 
            this.causeName = causeName;
            this.timeFrame = timeFrame;
            this.castTime = castTime;
        }
    }


    //Thorns Damage
    // if you are struck you revert back with thorns damage
    public class StrikerModel
    {
        public List<ThornBuffData> allThornsData = new List<ThornBuffData>();
        public List<RetaliateBuffData> allRetaliateData = new List<RetaliateBuffData>();
        

        public void RemoveThornDamage(int thornID)
        {
            int index = allThornsData.FindIndex(t => t.thornID == thornID);
            if (index != -1)
            {
                allThornsData.RemoveAt(index);
            }
        }
    }
}
