using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.Linq;
using System;

namespace Common
{
    public class EcoServices : MonoSingletonGeneric<EcoServices>, ISaveableService
    {
        public EconoModel econoModel;
        public EcoSO ecoSO;
        public event Action<Currency> OnInvMoneyChg;
        public event Action<Currency> OnStashMoneyChg;
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
            OnStashMoneyChg?.Invoke(econoModel.moneyInStash); 
        }
        public void DebitPlayerInv(Currency amt)
        {
            econoModel.moneyInInv.SubMoney(amt);
            OnInvMoneyChg?.Invoke(econoModel.moneyInInv); 
        }
        public void AddMoney2PlayerStash (Currency amt)
        {
            econoModel.moneyInStash.AddMoney(amt);
            OnStashMoneyChg?.Invoke(econoModel.moneyInStash);
        }
        public void AddMoney2PlayerInv(Currency amt)
        {
            econoModel.moneyInInv.AddMoney(amt);
            OnInvMoneyChg?.Invoke(econoModel.moneyInInv); 
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

