using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 



namespace Combat
{
    public class ThornsDmgData
    {
        int thornID = -1;
        public AttackType AttackType;
        public DamageType DamageType;  // damage attackers will be reverted by
        public float thornsMin;
        public float thornsMax;
        public int rdCount;

        public ThornsDmgData(int thornID,  AttackType attackType, DamageType damageType, float thornsMin, float thornsMax)
        {
            this.thornID = thornID;
            AttackType = attackType;
            DamageType = damageType;
            this.thornsMin = thornsMin;
            this.thornsMax = thornsMax;
        }
    }


    //Thorns Damage
    // if you are struck you revert back with thorns damage
    public class StrikeCharModel
    {
        public List<ThornsDmgData> allThornsData = new List<ThornsDmgData>();
        public int index = -1; 
        public int AddThornsDamage(AttackType attackType, DamageType damageType
            , float minVal, float maxVal)
        {
            index++;
            ThornsDmgData thornData = new ThornsDmgData(index, attackType, damageType, minVal, maxVal);
            allThornsData.Add(thornData);
            return index;
        }

        public void RemoveThornDamage(int thornID)
        {


        }
    




    }
}
