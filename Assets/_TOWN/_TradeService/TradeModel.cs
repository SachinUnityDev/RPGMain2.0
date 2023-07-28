using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;
using System;
using System.Linq;

namespace Town
{
    [Serializable]
    public class TradeModel  // every NPC that trades has a Trade Model 
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
        public List<ItemDataWithQtyNPrice> allTradableItems = new List<ItemDataWithQtyNPrice>();   
        public List<Iitems> allSelectItems = new List<Iitems>();


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
            NPCSO npcSO = TradeService.Instance.allNPCSO.GetNPCSO(npcName); 
            allTradableItems = npcSO.GetItemStock(weekSeq);            
            CreateItems();
            SetPrice();
        }

        void SetPrice()
        {
            foreach (ItemDataWithQtyNPrice itemData in allTradableItems)
            {
                Debug.Log("Item data" + itemData.itemData.itemType+ npcName + weekSeq);
                CostData costData = 
                ItemService.Instance.GetCostData(itemData.itemData.itemType, itemData.itemData.itemName);
                int minCost = (int)(costData.baseCost.BronzifyCurrency() * (100 - costData.fluctuation)/100);
                int maxCost = (int)(costData.baseCost.BronzifyCurrency() * (100 + costData.fluctuation) / 100);
                int bronzifiedResult = UnityEngine.Random.Range(minCost, maxCost);
                itemData.currPrice = (new Currency(0, bronzifiedResult)).RationaliseCurrency(); 
            }
        }

        public void OnSoldFrmStock()
        {
            foreach (Iitems item in allSelectItems)
            {
                allItems.Remove(item);
                foreach (ItemDataWithQtyNPrice itemQty in allTradableItems.ToList())
                {
                    if(item.itemName == itemQty.itemData.itemName 
                        && item.itemType == itemQty.itemData.itemType)
                    {
                        itemQty.quantity--;
                        if(itemQty.quantity <=0)
                            allTradableItems.Remove(itemQty);
                    }
                }
            }
            allSelectItems.Clear(); 
        }

        void CreateItems()
        {
            foreach (ItemDataWithQtyNPrice itemQty in allTradableItems)
            {
                for (int i = 0; i < itemQty.quantity; i++)
                {  
                    Iitems item = ItemService.Instance.GetNewItem(itemQty.itemData);
                    allItems.Add(item);
                }
            }
        }

        public Currency GetCurrPrice(ItemData itemData)
        {
            foreach (ItemDataWithQtyNPrice item in allTradableItems)
            {
                if(item.itemData.itemType == itemData.itemType 
                    && item.itemData.itemName == itemData.itemName)
                {
                    Currency price = item.currPrice; 
                    return price;
                }
            }
            Debug.Log(" current price NULL "); 
            return null;
        }

    }
}