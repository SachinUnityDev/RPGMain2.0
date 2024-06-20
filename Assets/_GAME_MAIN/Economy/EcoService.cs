using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.Linq;
using System;
using Town;
using Quest;
using Intro;
using System.IO;

namespace Common
{
    public enum PocketType
    {     
        Stash, 
        Inv,         
    }

    public class EcoService : MonoSingletonGeneric<EcoService>, ISaveable
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

        public ServicePath servicePath => ServicePath.EcoService; 
        void OnEnable()
        {
            ecoController = transform.GetComponent<EcoController>();    
        }
     
        
        public void InitEcoServices()
        {
            ecoController = transform.GetComponent<EcoController>();
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);            
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    econoModel = new EconoModel(ecoSO);
                    ecoController.InitEcoController(econoModel);
                }
                else
                {
                    LoadState();
                }
            }
            else
            {
                Debug.LogError("Service Directory missing"+ path);
            }
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
        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/EconoModel.txt";
            if (File.Exists(path))
            {
                string contents = File.ReadAllText(path);
                econoModel = JsonUtility.FromJson<EconoModel>(contents);
            }
            else
            {
                Debug.LogError("ECO MODEL NOT FOUND");
            }            
        }

        public void ClearState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);           
            DeleteAllFilesInDirectory(path);
        }

        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);           
            ClearState();           
            string ecoModelJson = JsonUtility.ToJson(econoModel);                
            string fileName = path + "EconoModel"+".txt";
            File.WriteAllText(fileName, ecoModelJson);
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveState();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadState();
            }
        }

        #endregion
    }
}

