using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;
using System;

namespace Town
{
    [Serializable]
    public class TradeModel
    {
        public NPCNames npcName;
        public int weekSeq; 
        public List<Iitems> allItems = new List<Iitems>();
        public List<ItemDataWithQty> allTradebaleItems = new List<ItemDataWithQty>();   

        public TradeModel(NPCSO npcSO, int weekSeq) 
        {
            npcName = npcSO.npcName;
            this.weekSeq = weekSeq; 
            allTradebaleItems = TradeService.Instance.allNPCSO.GetNPCStock(npcName,weekSeq);
            // create items from factory
            CreateItems();
        }


        void CreateItems()
        {

            foreach (ItemDataWithQty itemQty in allTradebaleItems)
            {
                for (int i = 0; i < itemQty.quantity; i++)
                {  
                    Iitems item = ItemService.Instance.GetNewItem(itemQty.ItemData);
                    allItems.Add(item);
                }
            }
        }
    }
}