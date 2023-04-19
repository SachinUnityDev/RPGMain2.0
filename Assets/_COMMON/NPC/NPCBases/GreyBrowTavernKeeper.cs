using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class GreyBrowTavernKeeper : NPCBase
    {
        public override NPCNames nPCNames => NPCNames.GreybrowTheTavernkeeper; 


        public override void NPCInit()
        {
            List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();

            ItemData item = new ItemData(ItemType.Foods, (int)FoodNames.FlaskOfWater);
            ItemDataWithQty itemQty = new ItemDataWithQty(item, 20);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Bread);
            itemQty = new ItemDataWithQty(item, 24);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Fish);
            itemQty = new ItemDataWithQty(item, 8);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Beef);
            itemQty = new ItemDataWithQty(item, 12);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Key);
            itemQty = new ItemDataWithQty(item, 9);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Shovel);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Chalice);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            NPCWeeklyStockData stockData = new NPCWeeklyStockData(1, allItemData);
            allWeeklyStock.Add(stockData);
            allItemData.Clear();
           
            /// WEEK 2 
            item = new ItemData(ItemType.Foods, (int)FoodNames.FlaskOfWater);
            itemQty = new ItemDataWithQty(item, 20);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Bread);
            itemQty = new ItemDataWithQty(item, 24);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Venison);
            itemQty = new ItemDataWithQty(item, 12);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Poultry);
            itemQty = new ItemDataWithQty(item, 12);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Beef);
            itemQty = new ItemDataWithQty(item, 8);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Key);
            itemQty = new ItemDataWithQty(item, 9);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Shovel);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.PlagueMask);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            stockData = new NPCWeeklyStockData(2, allItemData);
            allWeeklyStock.Add(stockData);
            allItemData.Clear();
            // Week3 
            item = new ItemData(ItemType.Foods, (int)FoodNames.FlaskOfWater);
            itemQty = new ItemDataWithQty(item, 24);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Bread);
            itemQty = new ItemDataWithQty(item, 16);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Poultry);
            itemQty = new ItemDataWithQty(item, 8);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Foods, (int)FoodNames.Beef);
            itemQty = new ItemDataWithQty(item, 12);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Key);
            itemQty = new ItemDataWithQty(item, 12);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Rope);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Tools, (int)ToolNames.Chalice);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            stockData = new NPCWeeklyStockData(2, allItemData);
            allWeeklyStock.Add(stockData);
            allItemData.Clear();
        }
    }
}