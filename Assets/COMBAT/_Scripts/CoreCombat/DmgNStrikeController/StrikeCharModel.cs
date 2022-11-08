using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 



namespace Combat
{
    public class ThornsDmgData
    {
        public int thornID = -1;
        public AttackType attackType;
        public DamageType damageType;  // damage attackers will be reverted by
        public float thornsMin;
        public float thornsMax;
        public int rdCount;
        public TimeFrame timeFrame = TimeFrame.None;
        public int currentTime =-1;

        public ThornsDmgData(int thornID,  AttackType attackType, DamageType damageType, float thornsMin, float thornsMax)
        {
            this.thornID = thornID;
            this.attackType = attackType;
            this.damageType = damageType;
            this.thornsMin = thornsMin;
            this.thornsMax = thornsMax;
        }

        public ThornsDmgData(TimeFrame timeFrame, int currentTime, int thornID
            , AttackType attackType, DamageType damageType, float thornsMin, float thornsMax)
        {
            this.timeFrame = timeFrame; 
            this.currentTime = currentTime;
            this.thornID = thornID;
            this.attackType = attackType;
            this.damageType = damageType;
            this.thornsMin = thornsMin;
            this.thornsMax = thornsMax;
        }

    }


    //Thorns Damage
    // if you are struck you revert back with thorns damage
    public class StrikeCharModel
    {
        public List<ThornsDmgData> allThornsData = new List<ThornsDmgData>();

        public void AddThornsDamage(ThornsDmgData thornDmgData)
        {
            allThornsData.Add(thornDmgData);            
        }

        public void RemoveThornDamage(int thornID)
        {
            ThornsDmgData thornDmgData = allThornsData.Find(t => t.thornID == thornID);
            allThornsData.Remove(thornDmgData);
        }
    




    }
}
