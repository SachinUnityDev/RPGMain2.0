using Common;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;



namespace Quest
{
    //[Serializable]
    //public class LootSortedData
    //{
    //    public ItemType itemType;
    //    public List<ItemDataLoot> lootList = new List<ItemDataLoot>();

    //    public LootSortedData(ItemType itemType)
    //    {
    //        this.itemType = itemType;
    //    }
      
    //}
    
    [Serializable]
    public class ItemDataLoot
    {

        public ItemType itemType;
        public int itemName;
        public int qty;
        public int wt;
        public GenGewgawQ genGewgawQ = GenGewgawQ.None;

        public ItemDataLoot(ItemType itemType, int itemName, int qty, int wt)
        {
            this.itemType = itemType;
            this.itemName = itemName;
            this.qty = qty;
            this.wt = wt;
        }

        public ItemDataLoot(ItemType itemType, GenGewgawQ genGewgawQ,int itemName, int qty, int wt)
        {
            this.itemType = itemType;
            this.genGewgawQ= genGewgawQ;    
            this.itemName = itemName;
            this.qty = qty;
            this.wt = wt;
        }
    }


    public abstract class LootBase
    {        
        public abstract LandscapeNames landscapeName { get;  }
        public abstract List<ItemDataLoot> itemDataLs { get; set; }
        public abstract void InitLootTable();
        List<ItemDataLoot> GetItemsOfType(ItemType itemType)
        {
           List<ItemDataLoot> allItemsOfItemType = new List<ItemDataLoot>();   
            foreach (ItemDataLoot loot in itemDataLs)
            {
                if (loot.itemType == itemType)
                {
                    allItemsOfItemType.Add(loot);
                }
            }
            return allItemsOfItemType;
        }
        ItemDataWithQty GetItemAfterWeightCalc(ItemType itemType)
        {



            return null; 
        }



        public List<ItemDataWithQty> GetLootList(List<ItemType> itemTypes)        
        {
            
            List<ItemDataWithQty> itemsWithQty = new List<ItemDataWithQty>();
            foreach(ItemType itemtype in itemTypes)
            {



            }   



            return null; 
        }


    }
}