using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;



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
    public class EconoModel
    {
        public Currency moneyInStash = new Currency();
        public Currency moneyInInv = new Currency(); 
        public Currency moneyNet = new Currency();

        public List<NPCMoneyData> allNPCMoneyData = new List<NPCMoneyData>(); 

        public EconoModel(EcoSO ecoSO)
        {
            // PLAYER MONEY 
            this.moneyInStash = ecoSO.moneyInStash.DeepClone();
            this.moneyInInv = ecoSO.moneyInInv.DeepClone();
            this.moneyNet = ecoSO.moneyNet.DeepClone();

            this.allNPCMoneyData = ecoSO.allNPCMoneyData.DeepClone();
        }
    }



}


