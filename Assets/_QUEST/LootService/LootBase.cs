using Common;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;
using Combat;


namespace Quest
{

    [Serializable]
    public class ItemLootData
    {

        public ItemType itemType;
        public int itemName;
        public int qty;
        public int wt;
        public GenGewgawQ genGewgawQ = GenGewgawQ.None;

        public ItemLootData(ItemType itemType, int itemName, int qty, int wt)
        {
            this.itemType = itemType;
            this.itemName = itemName;
            this.qty = qty;
            this.wt = wt;
        }

        public ItemLootData(ItemType itemType, GenGewgawQ genGewgawQ,int itemName, int qty, int wt)
        {
            this.itemType = itemType;
            this.genGewgawQ= genGewgawQ;    
            this.itemName = itemName;
            this.qty = qty;
            this.wt = wt;
        }

        public ItemDataWithQty GetItemDataWithQty()
        {
            ItemData itemData = new ItemData(itemType, itemName, genGewgawQ);
            ItemDataWithQty item = new ItemDataWithQty(itemData,qty);
            return item;
        }


    }


    public abstract class LootBase
    {        
        public abstract LandscapeNames landscapeName { get;  }
        public  List<ItemLootData> itemDataLs = new List<ItemLootData>();
        List<ItemDataWithQty> itemsWithQty = new List<ItemDataWithQty>();
        public abstract void InitLootTable();
        List<ItemLootData> GetItemsOfType(ItemType itemType)
        {
           List<ItemLootData> allItemsOfItemType = new List<ItemLootData>();   
            foreach (ItemLootData loot in itemDataLs)
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
            List<ItemLootData> allItemLootData = new List<ItemLootData>();
            allItemLootData = GetItemsOfType(itemType);
            int netWt = 0; float cumWt = 0; 
            allItemLootData.ForEach(t => netWt += t.wt);
            // do chance calc here 
            List<float> chances = new List<float>();

            foreach (ItemLootData lootData in allItemLootData)
            {
                if(netWt != 0)
                {
                    float ch = ((float)lootData.wt / netWt) * 100f;
                    cumWt += ch; 
                    chances.Add(cumWt);    
                }                
            }
            int index = chances.GetChanceFrmList();
            
            ItemDataWithQty itemDataWithQqty = allItemLootData[index].GetItemDataWithQty();
            return itemDataWithQqty;



            //for (int i = allItemLootData.Count-1; i >=0; i--)
            //{
              
            //    int j = i - 1;
            //    if (i > 0)
            //    {
            //        if ((netWt- allItemLootData[i].wt) < chance && chance < allItemLootData[i].wt)
            //        {
            //            itemDataWithQqty = allItemLootData[i].GetItemDataWithQty();
            //            break;
            //        }
            //    }
            //    else 
            //    {
            //        itemDataWithQqty = allItemLootData[i].GetItemDataWithQty();
            //        break;
            //    }
            //}


            //foreach (ItemLootData lootData in allItemLootData)
            //{
            //    if (lootData.wt < chance)
            //    {
            //        itemDataWithQqty = lootData.GetItemDataWithQty();
            //        break;
            //    }
            //}
          //  return itemDataWithQqty; 
        }



        public List<ItemDataWithQty> GetLootList(List<ItemType> itemTypes)        
        {
            itemsWithQty.Clear();
            foreach (ItemType itemtype in itemTypes)
            {
                ItemDataWithQty itemWithQty = GetItemAfterWeightCalc(itemtype); 
                itemsWithQty.Add(itemWithQty);
            }   
            return itemsWithQty; 
        }


    }
}