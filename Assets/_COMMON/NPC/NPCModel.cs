using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    [Serializable]
    public class NPCModel
    {

        [Header("NPC item in stock inv ")]
        public List<NPCWeeklyStockData> allWeeklyStock = new List<NPCWeeklyStockData>();


        //public void LoadWeek_1Items()
        //{
        //    List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();

        //    ItemData item = new ItemData(ItemType.Gems, (int)GemNames.Ruri);
        //    ItemDataWithQty itemQty = new ItemDataWithQty(item, 3);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Gems, (int)GemNames.Malachite);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Gems, (int)GemNames.Meranite);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);

        //    int ran = ItemService.Instance.GetRandomItemExcpt
        //        (new List<int>() { (int)ScrollNames.ScrollOfWater }, Enum.GetNames(typeof(ScrollNames)).Length);

        //    item = new ItemData(ItemType.Scrolls, ran);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);

        //    ran = ItemService.Instance.GetRandomItemExcpt
        //        (new List<int>() { (int)ScrollNames.ScrollOfWater, ran }, Enum.GetNames(typeof(ScrollNames)).Length);

        //    item = new ItemData(ItemType.Scrolls, ran);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);


        //    NPCWeeklyStockData stockData = new NPCWeeklyStockData(1, allItemData);
        //    allWeeklyStock.Add(stockData);
        //    allItemData.Clear();


        //}
        //public void LoadWeek_2Items() 
        //{
        //    List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();
        //   ItemData item = new ItemData(ItemType.Gems, (int)GemNames.Ruri);
        //   ItemDataWithQty  itemQty = new ItemDataWithQty(item, 3);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Gems, (int)GemNames.Carnelian);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Gems, (int)GemNames.Meranite);
        //    itemQty = new ItemDataWithQty(item, 1);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    int ran = ItemService.Instance.GetRandomItemExcpt
        //        (new List<int>() { (int)ScrollNames.ScrollOfWater }, Enum.GetNames(typeof(ScrollNames)).Length);

        //    item = new ItemData(ItemType.Scrolls, ran);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    ran = ItemService.Instance.GetRandomItemExcpt
        //        (new List<int>() { (int)ScrollNames.ScrollOfWater, ran }, Enum.GetNames(typeof(ScrollNames)).Length);

        //    item = new ItemData(ItemType.Scrolls, ran);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    NPCWeeklyStockData 
        //    stockData = new NPCWeeklyStockData(2, allItemData);
        //    allWeeklyStock.Add(stockData);
        //    allItemData.Clear();
        //}
        //public void LoadWeek_3Items()
        //{
        //    List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();

        //    ItemData item = new ItemData(ItemType.Gems, (int)GemNames.Ruri);
        //    ItemDataWithQty itemQty = new ItemDataWithQty(item, 3);
        //    allItemData.Add(itemQty);


        //    item = new ItemData(ItemType.Gems, (int)GemNames.Carnelian);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Gems, (int)GemNames.Malachite);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Gems, (int)GemNames.Meranite);
        //    itemQty = new ItemDataWithQty(item, 3);
        //    allItemData.Add(itemQty);

        //    item = new ItemData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    int ran = ItemService.Instance.GetRandomItemExcpt
        //        (new List<int>() { (int)ScrollNames.ScrollOfWater }, Enum.GetNames(typeof(ScrollNames)).Length);

        //    item = new ItemData(ItemType.Scrolls, ran);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    ran = ItemService.Instance.GetRandomItemExcpt
        //        (new List<int>() { (int)ScrollNames.ScrollOfWater, ran }, Enum.GetNames(typeof(ScrollNames)).Length);

        //    item = new ItemData(ItemType.Scrolls, ran);
        //    itemQty = new ItemDataWithQty(item, 2);
        //    allItemData.Add(itemQty);

        //    NPCWeeklyStockData
        //    stockData = new NPCWeeklyStockData(3, allItemData);
        //    allWeeklyStock.Add(stockData);
        //    allItemData.Clear();
        //}

    }
}