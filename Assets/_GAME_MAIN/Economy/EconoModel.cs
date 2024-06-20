using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Town;

namespace Common
{
    [System.Serializable]
    public class NPCMoneyData
    {
        public NPCNames npcName;
        public Currency money;

        public NPCMoneyData(NPCNames npcName, Currency money)
        {
            this.npcName = npcName;
            this.money = money;
        }
    }
    [System.Serializable]
    public class BronzifiedRange
    {
        public float bronzeValMin;
        public float bronzeValMax;

        public BronzifiedRange(float bronzeValMin, float bronzeValMax)
        {
            this.bronzeValMin = bronzeValMin;
            this.bronzeValMax = bronzeValMax;
        }
    }


    [System.Serializable]
    public class EconoModel
    {
        public Currency moneyInStash;
        public Currency moneyInInv; 

        public Currency moneyGainedInQ;


        public List<NPCMoneyData> allNPCMoneyData; 
        public List<EventCostMultData> allWeekEventCostData; 
        public EconoModel(EcoSO ecoSO)
        {
            moneyInStash = new Currency();
            moneyInInv = new Currency();
            moneyGainedInQ = new Currency();
            allNPCMoneyData = new List<NPCMoneyData>();
            allWeekEventCostData = new List<EventCostMultData>();
            // PLAYER MONEY 
            this.moneyInStash = ecoSO.moneyInStash.DeepClone();
            this.moneyInInv = ecoSO.moneyInInv.DeepClone();          

          //  this.allNPCMoneyData = ecoSO.allNPCMoneyData.DeepClone();
        }
        public EconoModel()
        {
            moneyInStash = new Currency();
            moneyInInv = new Currency();
            moneyGainedInQ = new Currency();
            allNPCMoneyData = new List<NPCMoneyData>();
            allWeekEventCostData = new List<EventCostMultData>();
        }
    }
}


