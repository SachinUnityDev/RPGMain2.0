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
        public Currency moneyInStash = new Currency();
        public Currency moneyInInv = new Currency(); 

        public Currency moneyGainedInQ= new Currency();


        public List<NPCMoneyData> allNPCMoneyData = new List<NPCMoneyData>(); 
        public List<EventCostMultData> allWeekEventCostData = new List<EventCostMultData>(); 
        public EconoModel(EcoSO ecoSO)
        {
            // PLAYER MONEY 
            this.moneyInStash = ecoSO.moneyInStash.DeepClone();
            this.moneyInInv = ecoSO.moneyInInv.DeepClone();          

          //  this.allNPCMoneyData = ecoSO.allNPCMoneyData.DeepClone();
        }
        
    }

 

}


