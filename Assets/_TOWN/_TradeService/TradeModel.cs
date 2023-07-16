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
        public string npcNameStr;

        [Header("Items purchaseAble by NPC")]
        public List<ItemType> itemTypesAccepted = new List<ItemType>();

        public BuildingNames buildName;

        public List<WeeklyItemInStock> weeklyItemStock = new List<WeeklyItemInStock>();

        [Header("current Data")]
        public int weekSeq; 
        public List<Iitems> allItems = new List<Iitems>();
        public List<ItemDataWithQty> allTradebaleItems = new List<ItemDataWithQty>();   

        public TradeModel(NPCSO npcSO, int weekSeq) 
        {
            npcName = npcSO.npcName;

            itemTypesAccepted = npcSO.itemTypesAccepted.DeepClone();
            buildName = npcSO.buildName;
            weeklyItemStock = npcSO.weeklyItemStock.DeepClone();

           SetWeeklyData(weekSeq);
        }

        void SetWeeklyData(int weekSeq)
        {
            this.weekSeq = weekSeq;
            allTradebaleItems = TradeService.Instance.allNPCSO.GetNPCStock(npcName, weekSeq);            
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