using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.Linq;
using System;
using Town;
using Quest;

namespace Common
{
    public enum PocketType
    {     
        Stash, 
        Inv, 
    }

    public class EcoServices : MonoSingletonGeneric<EcoServices>, ISaveableService
    {
        public event Action<PocketType> OnPocketSelected;
        public EconoModel econoModel;
        public EcoSO ecoSO;
        public event Action<Currency> OnInvMoneyChg;
        public event Action<Currency> OnStashMoneyChg;

        public PocketType currPocket; 
        public EcoController ecoController;

        [Header("Game Init")]
        public bool isNewGInitDone = false;


        void Start()
        {
            ecoController = transform.GetComponent<EcoController>();    
        }
     
        public void InitEcoServices()
        {
            ecoController = transform.GetComponent<EcoController>();
            econoModel = new EconoModel(ecoSO);
            ecoController.InitEcoController(econoModel);
            isNewGInitDone = true;
        }

        public void On_PocketSelected(PocketType pocketType)
        {
            currPocket= pocketType;
            OnPocketSelected?.Invoke(pocketType); 
        }
        public bool HasMoney(PocketType pocketType, Currency reqCurr)
        {
            bool hasMoney = false;           
            switch (pocketType)
            {              
                case PocketType.Stash:
                    hasMoney = GetMoneyAmtInPlayerStash().IsMoneySufficent(reqCurr);                    
                    break;
                case PocketType.Inv:
                   hasMoney = GetMoneyAmtInPlayerInv().IsMoneySufficent(reqCurr);
                    break;
                default:
                    break;
            }
            return hasMoney;
        }

        //public Currency GetMoneyInAct(NPCNames npcName)
        //{
        //   Currency npcMoney=  econoModel.allNPCMoneyData.Find(t => t.npcName == npcName).money;
        //    return npcMoney;
        //}
        public Currency GetMoneyFrmCurrentPocket()
        {
            if(currPocket== PocketType.Stash)
            {
                return GetMoneyAmtInPlayerStash();
            }
            else
            {
                return GetMoneyAmtInPlayerInv();                
            }
        }
        public bool DebitMoneyFrmCurrentPocket(Currency amt)
        {
            if (currPocket == PocketType.Stash)
            {
                return DebitPlayerStash(amt);
            }
            else
            {
                return DebitPlayerInv(amt);
            }
        }

        public Currency GetMoneyAmtInPlayerStash()
        {
            return econoModel.moneyInStash; 
        }
        public Currency GetMoneyAmtInPlayerInv()
        {
            return econoModel.moneyInInv;
        }
        public bool DebitPlayerStash(Currency amt)
        {
            bool isSuccess = econoModel.moneyInStash.SubMoney(amt);
            if (isSuccess)
                OnStashMoneyChg?.Invoke(econoModel.moneyInStash);
            return isSuccess;
        }
        public bool DebitPlayerInv(Currency amt)
        {
            bool isSuccess = econoModel.moneyInInv.SubMoney(amt);
            if(isSuccess)
            OnInvMoneyChg?.Invoke(econoModel.moneyInInv); 
            return isSuccess;
        }
        public bool DebitPlayerInvThenStash(Currency amt)
        {
            if (!DebitPlayerInv(amt))
                if (!DebitPlayerStash(amt))
                    return false;
            return true; 
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
        
        public void AddMoney2PlayerQuest(Currency amt)
        {
            econoModel.moneyGainedInQ.AddMoney(amt);
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

