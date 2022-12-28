using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 

namespace Interactables
{
    [System.Serializable]
    public class InvData
    {
        public CharNames charName;      
        public Iitems item;      

        public InvData(CharNames charName, Iitems item)
        {
            this.charName = charName;
            this.item = item;
        }
    }

    [System.Serializable]
    public class ActiveInvData
    {
        public CharNames CharName; 
        public List<Iitems> potionActivInv = new List<Iitems>();
        public List<Iitems> gewgawActivInv = new List<Iitems>();
        public int potionCount; 
        public int gewgawCount;
        public ActiveInvData(CharNames charName)
        {
            CharName = charName;
            potionCount = 0;
            gewgawCount = 0;    
        }
    }

    [System.Serializable]
    public class InvMainModel 
    {
        #region DECLARATIONS

        public List<Iitems> commonInvItems = new List<Iitems>();
        // Abbas 3 X 6,  each added Companion has 2X6 (Locked)(Town, QuestPrepPhase, in camp, in MapInteraction)
        //public List<Iitems> persCommInvItems = new List<Iitems>(); 

        public List<Iitems> stashInvIntems = new List<Iitems>();
        // 6 X 6 behaves like a common Inventory (Open Town/House) 

        public List<Iitems> excessInvItems = new List<Iitems>();
        // 4X6 Behaves like a Excess Inv inv ()       

        public List<ActiveInvData> allActiveInvData = new List<ActiveInvData>();

        #endregion 

        #region COMMON INV
        public bool AddItem2CommInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {          
            if (!InvService.Instance.IsCommInvFull())
            {
                item.invSlotType = SlotType.CommonInv;
                commonInvItems.Add(item);
                InvService.Instance.invViewController.AddItem2InVView(item);
                return true;
            }
            else
            {
                Debug.Log("Inv Size is full");
                return false;
            }         
        }

        public bool RemoveItem2CommInv(Iitems item) // view=> model 
        {
            commonInvItems.Remove(item);
            return true;
        }
        #endregion

        public ActiveInvData GetActiveInvData(CharNames charName)
        {
            ActiveInvData invData = allActiveInvData.Find(t => t.CharName == charName);
            if (invData != null)
                return invData;
            else
                Debug.Log("Potion Active Inv Data Not found" + charName);
            return null;
        }
        #region  ACTIVE INV POTION  

        public void AddItem2PotionActInv(Iitems item, int slotID) // key point of addition
                                                                  // SAVE and LOAD Active slot here
        {
            CharController charController = InvService.Instance.charSelectController;          
            CharNames charName = charController.charModel.charName;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharName == charName); 
            if(activeInvData != null)
            {
                activeInvData.potionActivInv.Add(item);
                activeInvData.potionCount++;
            }
            else
            {
                ActiveInvData activeInvDataNew = new ActiveInvData(charName);
                activeInvDataNew.potionActivInv.Add(item);
                activeInvDataNew.potionCount++; 
                allActiveInvData.Add(activeInvDataNew); 
            }
        }
        public bool RemoveItemFromPotionActInv( Iitems Item)
        {
            CharController charController = InvService.Instance.charSelectController;
            CharNames charName = charController.charModel.charName;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharName == charName);
            if(activeInvData != null)
            {
                activeInvData.potionActivInv.Remove(Item); 
                activeInvData.potionCount--;
            }
            else
            {
                Debug.Log("char active slot data not found"); 
            }
            // remove from char
            return false; 
        }
        #endregion

        #region  ACTIVE INV GEWGAWS  
        public void AddItem2GewgawsActInv(Iitems item, int slotID) // key point of addition
                                                                  // SAVE and LOAD Active slot here
        {
            CharController charController = InvService.Instance.charSelectController;  
            CharNames charName = charController.charModel.charName;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharName == charName);
            if (activeInvData != null)
            {
                activeInvData.gewgawActivInv.Add(item); 
            }
            else
            {
                ActiveInvData activeInvDataNew = new ActiveInvData(charName);
                activeInvDataNew.gewgawActivInv.Add(item);
                allActiveInvData.Add(activeInvData);
            }
        }
        public bool RemoveItemFromGewgawActInv(CharController charController, Iitems Item)
        {
            CharNames charName = charController.charModel.charName;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharName == charName);
            if (activeInvData != null)
            {
                activeInvData.gewgawActivInv.Remove(Item);
            }
            else
            {
                Debug.Log("char active slot data not found" + charController.charModel.charName);
            }
            // remove from char
            return false;
        }
    
        #endregion



        #region STASH
        public void MoveItems2Stash(CharNames charName)
        {
            foreach (InvData invData in commonInvItems)
            {
               
            }
        }

        #endregion




        #region EXCESS INV
        public bool AddItem2Excess(Iitems item)
        {
            item.invSlotType = SlotType.ExcessInv;
            excessInvItems.Add(item);
            // excess inv drag and drop can be opened whereever inv is open
            return false;
        }
        #endregion 
        //
        // TO DO : 
        // update this with every addition and deletion and 
        // update stash/house Inv Items, excess/junk 
        // 


    }
}

