using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using Unity;
using System;

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
    public class ActiveInvData: ISerializationCallbackReceiver
    {
        public int CharID;
        public Iitems[] potionActiveInv = new Iitems[2];
        public Iitems[] gewgawActiveInv = new Iitems[3];

        public Iitems provisionSlot; 


        [Header("Save")]
        public ItemData[] potionActiveInvData = new ItemData[2];
        public ItemData provisionSlotData; 
        public ItemData[] gewgawActiveInvData = new ItemData[3];
        public int potionCount; 
        public int gewgawCount;
        public ActiveInvData(int charID)
        {
            this.CharID = charID;
            potionCount = 0;
            gewgawCount = 0;
            potionActiveInv = new Iitems[2];
            gewgawActiveInv = new Iitems[3];
        }

        // Before saving convert all Iitems list to ItemData list
        public void OnAfterDeserialize()
        {
             potionCount = 0;gewgawCount = 0;
             potionActiveInv = new Iitems[2];
             gewgawActiveInv = new Iitems[3];
            foreach (ItemData item in potionActiveInvData)
            {
                if (item.itemType == ItemType.None) continue; 
                potionActiveInv[potionCount] = ItemService.Instance.GetNewItem(item);
                potionCount++;
            }
            if(provisionSlotData != null)
                provisionSlot = ItemService.Instance.GetNewItem(provisionSlotData); 
            foreach (ItemData item in gewgawActiveInvData)
            {
                if (item.itemType == ItemType.None) continue;
                gewgawActiveInv[gewgawCount] = ItemService.Instance.GetNewItem(item);
                gewgawCount++;
            }
        }

        public void OnBeforeSerialize()
        {
            int i= -1;
            
            foreach (Iitems item in potionActiveInv)
            {
                i++;
                if (item == null) continue; 
                ItemData itemData = new ItemData(item.itemType, item.itemName);                                  
                potionActiveInvData[i] = itemData;                
            }
            if(provisionSlot != null)
                provisionSlotData = new ItemData(provisionSlot.itemType, provisionSlot.itemName);
            i = -1; 
            foreach (Iitems item in gewgawActiveInv)
            {
                i++;
                if (item == null) continue;
                gewgawActiveInvData[i] = new ItemData(item.itemType, item.itemName);
                i++;
            }
        }
    }

    [Serializable]
    public class InvMainModel: ISerializationCallbackReceiver
    { 
        #region DECLARATIONS


        public List<Iitems> commonInvItems = new List<Iitems>();
        // Abbas 3 X 6,  each added Companion has 2X6 (Locked)(Town, QuestPrepPhase, in camp, in MapInteraction)
        //public List<Iitems> persCommInvItems = new List<Iitems>(); 
        public int commonInvCount = 0;
        public List<Iitems> stashInvItems = new List<Iitems>();
        // 6 X 6 behaves like a common Inventory (Open Town/House) 
        public int stashInvCount = 0;   
        public List<Iitems> excessInvItems = new List<Iitems>();
        // 4X6 Behaves like a Excess Inv inv ()       
        public int excessInvCount = 0;

        [Header("Savable data")]
        public List<ActiveInvData> allActiveInvData = new List<ActiveInvData>();
        public List<ItemData> commonInvItemData = new List<ItemData>();
        public List<ItemData> stashInvItemsData = new List<ItemData>();
        public List<ItemData> excessInvItemsData = new List<ItemData>();

        [Header("Inv max sizes")]
        public int size_Comm = 18;
        public int size_excess = 24;
        public int size_Stash = 18;
        #endregion 

        public void SetCommInvSize(int size)
        {
            size_Comm= size;
            // set slot controller of Islotable


        }



        public int GetCommInvSize()
        {
            // chk is party locked 
            // if locked chk the chars in party
            if(CharService.Instance.isPartyLocked)
            {
                int count = CharService.Instance.allCharsInPartyLocked.Count - 1;
                return 18 + 6 *2* count;
            }
            return 18; 
        }
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

        public bool AddItem2CommORExcess(Iitems item)
        {    
            if (AddItem2CommInv(item))
            {
                return true;
            }
            if (AddItem2ExcessInv(item))
            {
                return true;
            }
            return false;
        }

        public bool HasItemInQtyComm(ItemDataWithQty itemDataWithQty)
        {
            int count = 0;
            List<Iitems> allItems = new List<Iitems>();
            foreach (Iitems item in GetAllItemsInCommOfType(itemDataWithQty.itemData.itemType))
            {
                if (item.itemName == itemDataWithQty.itemData.itemName)
                {
                    count++;
                }
            }
            if (count >= itemDataWithQty.quantity)
                return true;
            return false;
        }

        public bool HasItemInQtyCommOrStash(ItemDataWithQty itemDataWithQty)
        {
            int count = 0; 
            List<Iitems> allItems = new List<Iitems>();
            foreach (Iitems item in GetAllItemsInCommOrStash(itemDataWithQty.itemData.itemType))
            {
                if(item.itemName == itemDataWithQty.itemData.itemName)
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
            List<Iitems> allItemsINComm = new List<Iitems>();
            List<Iitems> allItemsINStash = new List<Iitems>();
            allItemsINComm = GetAllItemsInCommOfType(itemType);
            allItemsINStash = GetAllItemsInStashofType(itemType);
            if(allItemsINComm.Count > 0)
                allItems.AddRange(allItemsINComm);
            if (allItemsINStash.Count > 0)
                allItems.AddRange(allItemsINStash);
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
            if (InvService.Instance.invRightViewController.AddItem2InVView(item, false))// this adds to Inv view list
            {
                item.invSlotType = SlotType.CommonInv;
                commonInvItems.Add(item);                 // item added here in list 
                commonInvCount++;
                InvService.Instance.On_ItemAdded2Comm(item); 
                return true;
            }
            else
            {
                Debug.Log("Comm Inv is full");
                return false;
            }         
        }
        public bool RemoveItemFrmCommInv(Iitems item) // view=> model 
        {
            commonInvItems.Remove(item);
            InvService.Instance.On_ItemRemovedFrmComm(item);
            commonInvCount--; 
            return true;
        }
        public bool RemoveItemFrmCommInv(ItemData itemData)
        {
            foreach (Iitems item in commonInvItems.ToList())
            {
                GenGewgawBase gbase = item as GenGewgawBase;
                if (gbase != null && item.itemName == itemData.itemName && item.itemType == itemData.itemType)
                {
                   return RemoveItemFrmCommInv(item);

                }
                else if (item.itemName== itemData.itemName && item.itemType == itemData.itemType)
                {
                   return RemoveItemFrmCommInv(item);
                }
            }
            return false; 
        }
        public int GetItemNosInCommInv(ItemData itemData)
        {
            //commonInvItems
            int quantity = commonInvItems.Count(t=>t.itemName == itemData.itemName && t.itemType == itemData.itemType);
            return quantity; 
        }
        public Iitems GetItemRefFrmCommInv(ItemType itemType, int itemName)
        {
            Iitems item =  commonInvItems.Find(t => t.itemType == itemType
                                        && t.itemName == itemName);
            if(item == null)
            {
                Debug.Log("Item Not FOUND in comm Inv"); return null;
            }

            return item; 
        }        
        public ItemDataWithQty GetItemDataWithQtyFrmCommInv(ItemType itemType, int itemName)
        {
            List<Iitems> allItems = commonInvItems.Where(t => t.itemType == itemType && t.itemName == itemName).ToList();
            ItemData itemData = new ItemData(itemType, itemName); 
            ItemDataWithQty itemDataWithQty = new ItemDataWithQty(itemData, allItems.Count);
            return itemDataWithQty;
        }

        #endregion

        #region EXCESS INV 
        public bool AddItem2ExcessInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {
            if (InvService.Instance.excessInvViewController.AddItem2InVView(item, false))
            {
                item.invSlotType = SlotType.ExcessInv;
                excessInvItems.Add(item);               
                excessInvCount++;
                return true;
            }
            else
            {
                Debug.Log("Excess Inv Size is full");
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
            allItems = stashInvItems.Where(t => t.itemType == itemType).ToList();
            if (allItems.Count > 0)
            {
                return allItems;
            }
            Debug.Log("no Items of type found in Stash inv" + itemType);
            return allItems;
        }
        public List<Iitems> GetItemsFrmStashInv(ItemType itemType)
        {
            List<Iitems> allItems = new List<Iitems>();
            allItems = stashInvItems.Where(t => t.itemType == itemType).ToList();
            return allItems;
        }

        public bool AddItem2StashInv(Iitems item)   // KEY POINT OF ADDITION OF ITEM // Add to model => view
        {
            if (InvService.Instance.stashInvViewController.AddItem2InVView(item, false)) // adds to model 
            {
                item.invSlotType = SlotType.StashInv;
                stashInvItems.Add(item);                
                stashInvCount++;
                return true;
            }
            else
            {
                Debug.Log("Stash Inv Size is full");
                return false;
            }
        }

        public bool RemoveItemFrmStashInv(Iitems item) // view=> model 
        {
            stashInvItems.Remove(item);
            return true;
        }
        public bool RemoveItemFrmStashInv(ItemData itemData)
        {
            foreach (Iitems item in stashInvItems.ToList())
            {
                GenGewgawBase gbase = item as GenGewgawBase;
                if (gbase != null && item.itemName == itemData.itemName && item.itemType == itemData.itemType)
                {
                    return RemoveItemFrmStashInv(item);

                }
                else if (item.itemName == itemData.itemName && item.itemType == itemData.itemType)
                {
                    return RemoveItemFrmStashInv(item);
                }
            }
            return false;
        }
        public int GetItemNosInStashInv(ItemData itemData)
        {
           // stash inv Items
            int quantity = stashInvItems.Count(t => t.itemName == itemData.itemName && t.itemType == itemData.itemType);
            return quantity;
        }
        #endregion

        #region  ACTIVE INV POTION  
        public ActiveInvData GetActiveInvData(int charID)
        {
            ActiveInvData invData = allActiveInvData.Find(t => t.CharID == charID);
            if (invData != null)
                return invData;
            else
                Debug.Log("Active Inv Data Not found" + charID);
            return null;
        }
        public void EquipItem2PotionActInv(Iitems item, int slotId) // no more than one call to be created
        {
            CharController charController = InvService.Instance.charSelectController;
            int charID = charController.charModel.charID;

            ActiveInvData activeInvData = GetActiveInvData(charID);
            if (activeInvData != null)
            {
                activeInvData.potionActiveInv[slotId] = item;
                activeInvData.potionCount++;
            }
            else
            {
                ActiveInvData activeInvDataNew = new ActiveInvData(charID);
                activeInvDataNew.potionActiveInv[slotId] = item; 
                activeInvDataNew.potionCount++;
                allActiveInvData.Add(activeInvDataNew);
            }
            EquipItem(item, charController);
        }

        public void EquipItem2PotionProvSlot(Iitems item, CharController charController  ) // key point of addition
                                                            // only for provision slot
        {

            int charID = charController.charModel.charID;

            ActiveInvData activeInvData = GetActiveInvData(charID);
            if (activeInvData != null)
            {
                activeInvData.provisionSlot = item; 
            }
            else
            {
                ActiveInvData activeInvDataNew = new ActiveInvData(charID);   
                activeInvDataNew.provisionSlot = item;
                allActiveInvData.Add(activeInvDataNew);
            }
            EquipItem(item, charController);
            if (InvService.Instance.invRightViewController.Add2ProvisionSlot(item))
            {
                // broascast provision added 
                Debug.Log("Potion added to provision" + (PotionNames)item.itemName);

            }
            else
            {
                Debug.LogError("Potion cannot be added to provision" + (PotionNames)item.itemName + charController.charModel.charName);
            } 
        }
        public void EquipItem(Iitems item, CharController charController)
        {
            IEquipAble equip = item as IEquipAble;
            if (equip != null)
            {
                equip.ApplyEquipableFX(charController);
            }
        }
        void UnEquipItem(Iitems item)
        {
            IEquipAble equip = item as IEquipAble;
            if (equip != null)
            {
                equip.RemoveEquipableFX(); 
            }
        }
        public bool RemoveItemFromPotionActInv(Iitems Item, int slotID)
        {
            CharController charController = InvService.Instance.charSelectController;
            int charID = charController.charModel.charID;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharID == charID);
            if (activeInvData != null)
            {
                if (slotID < 2) // provision slot is
                {
                    activeInvData.potionActiveInv[slotID] = null;
                    activeInvData.potionCount--;
                }
                else
                {
                    activeInvData.provisionSlot = null;
                }                
                UnEquipItem(Item);
                return true;
            }
            else
            {
                Debug.Log("char active slot data not found");
                return false;
            }
            // remove from char
            
        }
        #endregion

        #region  ACTIVE INV GEWGAWS  
        public void EquipItem2GewgawsActInv(Iitems item, int slotID) // key point of addition
                                                                   // SAVE and LOAD Active slot here
        {
            CharController charController = InvService.Instance.charSelectController;
            int charID = charController.charModel.charID;

            int index = allActiveInvData.FindIndex(t => t.CharID == charID);
            if (index != -1)
            {
                allActiveInvData[index].gewgawActiveInv[slotID] = item;
                allActiveInvData[index].gewgawCount++;
            }
            else
            {
                ActiveInvData activeInvDataNew = new ActiveInvData(charID);
                activeInvDataNew.gewgawActiveInv[slotID] = item;
                activeInvDataNew.gewgawCount++;

                allActiveInvData.Add(activeInvDataNew);
            }
        }
        public bool RemoveItemFromGewgawActInv(Iitems Item, int slotID)
        {
            CharController charController = InvService.Instance.charSelectController;

            int charID = charController.charModel.charID;

            ActiveInvData activeInvData = allActiveInvData.Find(t => t.CharID == charID);
            if (activeInvData != null)
            {
                activeInvData.gewgawActiveInv[slotID] = null;
                activeInvData.gewgawCount--;
                UnEquipItem(Item); 
                return true;
            }
            else
            {
                Debug.Log("char active slot data not found" + charController.charModel.charName);
                return false;
            }
            // remove from char
          
        }


        // Before saving convert all Iitems list to ItemData list 
        public void OnBeforeSerialize()
        {
            commonInvItemData.Clear();
            foreach (Iitems item in commonInvItems)
            {
                if(item.itemType != ItemType.GenGewgaws)
                {
                    commonInvItemData.Add(new ItemData(item.itemType, item.itemName));
                }                
                else
                {
                    GenGewgawBase gbase = item as GenGewgawBase;
                    commonInvItemData.Add(new ItemData(item.itemType, item.itemName, gbase.genGewgawQ));
                }
            }
            stashInvItemsData.Clear();
            foreach (Iitems item in stashInvItems)
            {
                if(item.itemType != ItemType.GenGewgaws)
                {
                    stashInvItemsData.Add(new ItemData(item.itemType, item.itemName));
                }
                else
                {
                    GenGewgawBase gbase = item as GenGewgawBase;
                    stashInvItemsData.Add(new ItemData(item.itemType, item.itemName, gbase.genGewgawQ));
                }               
            }
            excessInvItemsData.Clear();
            foreach (Iitems item in excessInvItems)
            {
                if(item.itemType != ItemType.GenGewgaws)
                {
                    excessInvItemsData.Add(new ItemData(item.itemType, item.itemName));
                }
                else
                {
                    GenGewgawBase gbase = item as GenGewgawBase;
                    excessInvItemsData.Add(new ItemData(item.itemType, item.itemName, gbase.genGewgawQ));
                }
            }   
        }

        // After loading convert all ItemData list to Iitems list
        public void OnAfterDeserialize()
        {
            commonInvItems.Clear();
            foreach (ItemData itemData in commonInvItemData)
            {
                Iitems item = ItemService.Instance.GetNewItem(itemData);
                commonInvItems.Add(item);
            }
            stashInvItems.Clear();
            foreach (ItemData itemData in stashInvItemsData)
            {
                Iitems item = ItemService.Instance.GetNewItem(itemData);
                stashInvItems.Add(item);
            }
            excessInvItems.Clear(); 
            foreach (ItemData itemData in excessInvItemsData)
            {
                Iitems item = ItemService.Instance.GetNewItem(itemData);
                excessInvItems.Add(item);
            }
        }

        #endregion



    }
}

