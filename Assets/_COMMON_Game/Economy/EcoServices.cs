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

        public Currency GetMoneyAmtInPlayerStash()
        {
            return econoModel.moneyInStash; 
        }
        public Currency GetMoneyAmtInPlayerInv()
        {
            return econoModel.moneyInInv;
        }
        public void DebitPlayerStash(Currency amt)
        {
            econoModel.moneyInStash.SubMoney(amt);          
        }
        public void DebitPlayerInv(Currency amt)
        {
            econoModel.moneyInInv.SubMoney(amt);
        }
        public void AddMoney2PlayerStash (Currency amt)
        {
            econoModel.moneyInStash.AddMoney(amt);
        }
        public void AddMoney2PlayerInv(Currency amt)
        {
            econoModel.moneyInInv.AddMoney(amt);
        }
        //public void DebitNPCAct(Currency amt, NPCNames npcName)
        //{
        //    econoModel.allNPCMoneyData.Find(t => t.npcName == npcName).money.SubMoney(amt);
        //}
        //public void PayMoney2NPC(Currency amt, NPCNames npcName)
        //{
        //    econoModel.allNPCMoneyData.Find(t => t.npcName == npcName).money.AddMoney(amt) ; 
        //}



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

