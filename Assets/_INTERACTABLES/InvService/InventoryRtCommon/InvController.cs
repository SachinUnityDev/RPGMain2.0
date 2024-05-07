using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;
using System.Linq;
using Quest;
using Town;
using System;

namespace Interactables
{
    [Serializable]
    public class InvSlotDataBase
    {
        public Iitems item;
        public ItemType itemType;
        public int itemName; 
        public int itemInSlot;
        public int maxItemInSlot;

        public InvSlotDataBase(Iitems item, int itemInSlot)
        {
            this.item = item;
            this.itemInSlot = itemInSlot;

            maxItemInSlot = item.maxInvStackSize;
            itemName = item.itemName; 
            itemType= item.itemType;

        }
    }

    public class InvController : MonoBehaviour
    {
        // contain , transact and update all the inventory controls
        public List<InvSlotDataBase> itemlsComm = new List<InvSlotDataBase>();// replicate slots
        public List<InvSlotDataBase> itemlsExcess = new List<InvSlotDataBase>();
        public List<InvSlotDataBase> itemlsStash = new List<InvSlotDataBase>();
        public int count =0;
        [SerializeField] int size_Comm =0;
        [SerializeField] int size_excess = 0;
        [SerializeField] int size_Stash = 0;


        public void InitSizeList()
        {            
            InvService.Instance.invRightViewController.UpdateCommInvDB();
            InvService.Instance.excessInvViewController.UpdateExcessInvDataBase();
            if(GameService.Instance.currGameModel.gameScene == GameScene.InTown)
            {
                InvService.Instance.stashInvViewController.UpdateStashInvDatabase();
                size_Stash = InvService.Instance.invMainModel.size_Stash;
            }
            size_Comm = InvService.Instance.invMainModel.size_Comm;
            size_excess = InvService.Instance.invMainModel.size_excess;
            
        }
        public bool CheckIfLootCanBeAdded(List<ItemBaseWithQty> items)
        {            
            InitSizeList();
            foreach (ItemBaseWithQty item in items) 
            {
                if (!CanAddItem2CommInv(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanAddItem2CommInv(ItemBaseWithQty itemBaseWithQty)
        {
            bool slotFound = false;   
            foreach (InvSlotDataBase itemDB in itemlsComm.ToList())
            {
                if(itemDB.itemName == itemBaseWithQty.item.itemName &&
                    itemDB.itemType == itemBaseWithQty.item.itemType)// genQ to be added
                {
                    int diff = itemDB.maxItemInSlot - itemDB.itemInSlot; 
                    if (diff > 0)
                    {
                        int qty = itemBaseWithQty.qty; 
                        if (diff<= qty)
                        {
                            itemDB.itemInSlot += qty;
                            slotFound = true;
                            break;
                        }
                        else if(qty > diff)
                        {
                            int nextSlotQty = qty - diff;
                            itemDB.itemInSlot += diff;

                            InvSlotDataBase itemBase = new InvSlotDataBase(itemBaseWithQty.item, nextSlotQty);
                            itemlsComm.Add(itemBase);
                            slotFound = true;
                            break;
                        }
                    }
                }
            }
            if(!slotFound)
            {
                InvSlotDataBase itemBase = new InvSlotDataBase(itemBaseWithQty.item, itemBaseWithQty.qty);
                itemlsComm.Add(itemBase);
            }

            if(itemlsComm.Count<= size_Comm)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    




    }


}



