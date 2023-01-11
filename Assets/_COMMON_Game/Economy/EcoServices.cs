using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.Linq; 

namespace Common
{
    public class EcoServices : MonoSingletonGeneric<EcoServices>, ISaveableService
    {
        public EconoModel econoModel;
        public EcoSO ecoSO; 

        void Start()
        {
            econoModel = new EconoModel(ecoSO);
        }

        public Currency GetMoneyInAct(NPCNames npcName)
        {
           Currency npcMoney=  econoModel.allNPCMoneyData.Find(t => t.npcName == npcName).money;
            return npcMoney;
        }

        public Currency GetMoneyValueNetPlayer()
        {
            return econoModel.moneyNet; 
        }

        public void DebitPlayerAct(Currency amt)
        {
            econoModel.moneyNet.SubMoney(amt);
          
        }
        public void PayMoney2Player (Currency amt)
        {
            econoModel.moneyNet.AddMoney(amt);
        }
        public void DebitNPCAct(Currency amt, NPCNames npcName)
        {
            econoModel.allNPCMoneyData.Find(t => t.npcName == npcName).money.SubMoney(amt);
        }
        public void PayMoney2NPC(Currency amt, NPCNames npcName)
        {
            econoModel.allNPCMoneyData.Find(t => t.npcName == npcName).money.AddMoney(amt) ; 
        }

        public void ChgMoneyInStash() // stash is like a chest in the town
        {
            // use curr stash value 
        }

        public void ChgMoneyInInv()// Inv is mobile 
        {

        }



#region SAVE_LOAD SERVICES
        public void RestoreState()
        {
            Debug.Log("ECO SERVICES SAVE .. restored"); 
        }

        public void ClearState()
        {
            Debug.Log("ECO SERVICES SAVE .. cleared");
        }

        public void SaveState()
        {
            Debug.Log("ECO SERVICES SAVE .. saveState");
        }

#endregion
    }
}

