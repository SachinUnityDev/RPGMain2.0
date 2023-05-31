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
        public int commonInvCount = 0;
        public List<Iitems> stashInvIntItems = new List<Iitems>();
        // 6 X 6 behaves like a common Inventory (Open Town/House) 
        public int stashInvCount = 0;   
        public List<Iitems> excessInvItems = new List<Iitems>();
        // 4X6 Behaves like a Excess Inv inv ()       
        public int excessInvCount = 0;  
        public List<ActiveInvData> allActiveInvData = new List<ActiveInvData>();


        [Header("Inv max sizes")]
        public int size_Comm = 24;
        public int size_excess = 24;
        public int size_Stash = 18;
        #endregion 




        public bool AddItem2CommORStash(Iitems item)
        {
            if (AddItem2CommInv(item))
            {
                return true; 
            }else if (AddItem2StashInv(item))
            {
                return true; 
            }
            return false; 
        }

        public bool HasItemInQtyCommOrStash(ItemDataWithQty itemDataWithQty)
        {
            int count = 0; 
            List<Iitems> allItems = new List<Iitems>();
            foreach (Iitems item in GetAllItemsInCommOrStash(itemDataWithQty.ItemData.itemType))
            {
                if(item.itemName == itemDataWithQty.ItemData.itemName)
                {
                    count++; 
                }
            }
            if (count >= itemDataWithQty.quantity)
                return true; 
            return false; 
        }

        public List<Iitems> GetAllItemsInCommOrStash(ItemType itemType)
        {
            List<Iitems> allItems = new List<Iitems>();
            allItems.AddRange(commonInvItems);
            allItems.AddRange(stashInvIntItems);
            return allItems;
        }
        #region COMMON INV

        public List<Iitems> GetAllItemsInCommOfType(ItemType itemType)
        {
            List<Iitems> allItems = new List<Iitems>();
            allItems = commonInvItems.Where(t => t.itemType == itemType).ToList();
            if(allItems.Count > 0)
            {
                return allItems;    
            }
            Debug.Log("no Items of type found in comm inv" + itemType); 
            return allItems; 
        }

        public bool AddItem2CommInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {          
            if (!InvService.Instance.IsCommInvFull(item))
            {
                item.invSlotType = SlotType.CommonInv;
                commonInvItems.Add(item); 
                InvService.Instance.commInvViewController.AddItem2InVView(item, false);// this adds to model list
                commonInvCount++; 
                return true;
            }
            else
            {
                Debug.Log("Inv Size is full");
                return false;
            }         
        }

        public bool RemoveItemFrmCommInv(Iitems item) // view=> model 
        {
            commonInvItems.Remove(item);
            commonInvCount--; 
            return true;
        }

        public int GetItemNosInCommInv(ItemData itemData)
        {
            //commonInvItems
            int quantity = commonInvItems.Count(t=>t.itemName == itemData.itemName && t.itemType == itemData.itemType);
            return quantity; 
        }

        public List<Iitems> GetItemsFrmCommonInv(ItemType itemType)
        {
            List<Iitems> allItems = new List<Iitems>();
            allItems = commonInvItems.Where(t => t.itemType == itemType).ToList();
            return allItems;
        }
        #endregion


        #region EXCESS INV 
        public bool AddItem2ExcessInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {
            if (!InvService.Instance.IsExcessInvFull(item))
            {
                item.invSlotType = SlotType.ExcessInv;
                excessInvItems.Add(item);
                InvService.Instance.excessInvViewController.AddItem2InVView(item, false);
                excessInvCount++;
                return true;
            }
            else
            {
                Debug.Log("Inv Size is full");
                return false;
            }
        }

        public bool RemoveItemFrmExcessInv(Iitems item) // view=> model 
        {
            excessInvItems.Remove(item);
            excessInvCount--;
            return true;
        }
        public int GetItemNosInExcessnv(ItemData itemData)
        {
            //commonInvItems
            int quantity = excessInvItems.Count(t => t.itemName == itemData.itemName && t.itemType == itemData.itemType);
            return quantity;
        }

        #endregion


        #region STASH

        public List<Iitems> GetAllItemsInStashofType(ItemType itemType)
        {
            List<Iitems> allItems = new List<Iitems>();
            allItems = stashInvIntItems.Where(t => t.itemType == itemType).ToList();
            if (allItems.Count > 0)
            {
                return allItems;
            }
            Debug.Log("no Items of type found in Stash inv" + itemType);
            return null;
        }
        public List<Iitems> GetItemsFrmStashInv(ItemType itemType)
        {
            List<Iitems> allItems = new List<Iitems>();
            allItems = stashInvIntItems.Where(t => t.itemType == itemType).ToList();
            return allItems;
        }

        public bool AddItem2StashInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {
            if (!InvService.Instance.IsStashInvFull(item))
            {
                item.invSlotType = SlotType.StashInv;
                stashInvIntItems.Add(item);
                InvService.Instance.stashInvViewController.AddItem2InVView(item,false);
                stashInvCount++;
                return true;
            }
            else
            {
                Debug.Log("Inv Size is full");
                return false;
            }
        }

        public bool RemoveItemFrmStashInv(Iitems item) // view=> model 
        {
            stashInvIntItems.Remove(item);
            return true;
        }

        public int GetItemNosInStashInv(ItemData itemData)
        {
           // stash inv Items
            int quantity = stashInvIntItems.Count(t => t.itemName == itemData.itemName && t.itemType == itemData.itemType);
            return quantity;
        }
        #endregion

        #region  ACTIVE INV POTION  
        public ActiveInvData GetActiveInvData(CharNames charName)
        {
            ActiveInvData invData = allActiveInvData.Find(t => t.CharName == charName);
            if (invData != null)
                return invData;
            else
                Debug.Log("Potion Active Inv Data Not found" + charName);
            return null;
        }


        public void AddItem2PotionActInv(Iitems item, int slotID) // key point of addition
                                                                  // SAVE and LOAD Active slot here
        {
            CharController charController = InvService.Instance.charSelectController;
            CharNames charName = charController.charModel.charName;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharName == charName);
            if (activeInvData != null)
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
        public bool RemoveItemFromPotionActInv(Iitems Item)
        {
            CharController charController = InvService.Instance.charSelectController;
            CharNames charName = charController.charModel.charName;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharName == charName);
            if (activeInvData != null)
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
    }
}

