using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class Minami : NPCBase
    {
        public override NPCNames nPCNames => NPCNames.MinamiTheSoothsayer;

        public override BuildingNames buildingNames => BuildingNames.Temple;

        public override void NPCInit()
        {
            List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();

            ItemData item = new ItemData(ItemType.Gems, (int)GemNames.Ruri);
            ItemDataWithQty itemQty = new ItemDataWithQty(item, 3); 
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Gems, (int)GemNames.Malachite);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Gems, (int)GemNames.Meranite);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            int ran = ItemService.Instance.GetRandomItemExcpt
                (new List<int>() { (int)ScrollNames.ScrollOfWater }, Enum.GetNames(typeof(ScrollNames)).Length);

            item = new ItemData(ItemType.Scrolls, ran);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            ran = ItemService.Instance.GetRandomItemExcpt
                (new List<int>() { (int)ScrollNames.ScrollOfWater, ran }, Enum.GetNames(typeof(ScrollNames)).Length);

            item = new ItemData(ItemType.Scrolls, ran);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);


            NPCWeeklyStockData stockData = new NPCWeeklyStockData(1, allItemData); 
            allWeeklyStock.Add(stockData);
            allItemData.Clear(); 
            /// WEEK 2
            /// 
            item = new ItemData(ItemType.Gems, (int)GemNames.Ruri);
            itemQty = new ItemDataWithQty(item, 3);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Gems, (int)GemNames.Carnelian);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Gems, (int)GemNames.Meranite);
            itemQty = new ItemDataWithQty(item, 1);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            ran = ItemService.Instance.GetRandomItemExcpt
                (new List<int>() { (int)ScrollNames.ScrollOfWater }, Enum.GetNames(typeof(ScrollNames)).Length);

            item = new ItemData(ItemType.Scrolls, ran);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            ran = ItemService.Instance.GetRandomItemExcpt
                (new List<int>() { (int)ScrollNames.ScrollOfWater, ran }, Enum.GetNames(typeof(ScrollNames)).Length);

            item = new ItemData(ItemType.Scrolls, ran);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            stockData = new NPCWeeklyStockData(2, allItemData);
            allWeeklyStock.Add(stockData);
            allItemData.Clear();

            /// WEEKLY 3 
       

            item = new ItemData(ItemType.Gems, (int)GemNames.Ruri);
            itemQty = new ItemDataWithQty(item, 3);
            allItemData.Add(itemQty);


            item = new ItemData(ItemType.Gems, (int)GemNames.Carnelian);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Gems, (int)GemNames.Malachite);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Gems, (int)GemNames.Meranite);
            itemQty = new ItemDataWithQty(item, 3);
            allItemData.Add(itemQty);

            item = new ItemData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            ran = ItemService.Instance.GetRandomItemExcpt
                (new List<int>() { (int)ScrollNames.ScrollOfWater }, Enum.GetNames(typeof(ScrollNames)).Length);

            item = new ItemData(ItemType.Scrolls, ran);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            ran = ItemService.Instance.GetRandomItemExcpt
                (new List<int>() { (int)ScrollNames.ScrollOfWater, ran }, Enum.GetNames(typeof(ScrollNames)).Length);

            item = new ItemData(ItemType.Scrolls, ran);
            itemQty = new ItemDataWithQty(item, 2);
            allItemData.Add(itemQty);

            stockData = new NPCWeeklyStockData(3, allItemData);
            allWeeklyStock.Add(stockData);
            allItemData.Clear();
        }
    }
}