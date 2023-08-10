using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;
using System;

namespace Town
{
  

    [CreateAssetMenu(fileName = "NPCSO", menuName = "Character Service/NPCSO")]

    public class NPCSO : ScriptableObject
    {
        public int npcID;
        public NPCNames npcName;
        public string npcNameStr;
        public NPCClassTypes classTypes;
        public GameObject npcPrefab;  // change to prefab 
        public Sprite npcSprite;
        public Sprite npcHexPortrait;
        public Sprite dialoguePortraitClicked;
        public Sprite dialoguePortraitUnClicked;

        [Header("Items purchaseAble by NPC")]
        public List<ItemType> itemTypesAccepted = new List<ItemType>();

        public BuildingNames buildName;

        public List<WeeklyItemInStock> weeklyItemStock = new List<WeeklyItemInStock>();

        public List<ItemDataWithQtyNPrice> GetItemStock(int weekSeq)
        {
            
            List<ItemDataWithQtyNPrice> result = new List<ItemDataWithQtyNPrice>();
            foreach (WeeklyItemInStock weekItemStock in weeklyItemStock)
            {
                if (weekItemStock.weekSeq == weekSeq)
                {
                    foreach (ItemDataLs itemDataLs in weekItemStock.itemDataLs)
                    {
                        int itemName =
                            itemDataLs.itemName.GetItemName(itemDataLs.itemType);
                        ItemData itemData = new ItemData(itemDataLs.itemType, itemName, itemDataLs.genGawgawQ);
                        ItemDataWithQtyNPrice itemQty = new ItemDataWithQtyNPrice(itemData, itemDataLs.qty);
                        result.Add(itemQty);
                    }
                }
            }
            return result;
        }
    }

   

    [Serializable]
    public class WeeklyItemInStock
    {
        public int weekSeq;
        public List<ItemDataLs> itemDataLs = new List<ItemDataLs>();
    }
    [Serializable]
    public class AllItemNames
    {
        public PotionNames potionName;
        public GenGewgawNames genGewgawNames;
        public HerbNames HerbName;
        public FoodNames foodName;
        public FruitNames fruitNames;
        public IngredNames ingredientName;
        public ScrollNames scrollName;
        public TGNames tgName;
        public ToolNames toolName;
        public TeaNames teaName;
        public SoupNames soupsName;
        public GemNames gemName;
        public AlcoholNames alcoholName;
        public MealNames mealName;
        public SagaicGewgawNames sagaicName;
        public PoeticGewgawNames poeticName;
       

        public int GetItemName(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Potions:
                    return (int)potionName;                     
                case ItemType.GenGewgaws:
                    return (int)genGewgawNames; 
                case ItemType.Herbs:
                    return (int)HerbName;
                case ItemType.Foods:
                    return(int)foodName;
                case ItemType.Fruits:
                    return(int)fruitNames;
                case ItemType.Ingredients:
                    return(int)ingredientName;
                case ItemType.XXX:
                    break;
                case ItemType.Scrolls:
                    return (int)scrollName;                    
                case ItemType.TradeGoods:
                    return (int)tgName; 
                case ItemType.Tools:
                    return (int)toolName;                     
                case ItemType.Teas:
                    return (int)teaName; 
                case ItemType.Soups:
                    return (int)soupsName;                    
                case ItemType.Gems:
                    return (int)gemName; 
                case ItemType.Alcohol:
                    return(int) alcoholName;                    
                case ItemType.Meals:
                    return (int)mealName;                     
                case ItemType.SagaicGewgaws:
                    return (int)sagaicName; 
                case ItemType.PoeticGewgaws:
                    return (int)poeticName;                    
                case ItemType.RelicGewgaws:
                    return (int)0;                                 
                case ItemType.LoreScroll:
                    return (int)0;
                default:
                    break;
            }
            return 0;
        }

    }

    [Serializable]
    public class ItemDataLs
    {
        public ItemType itemType;
        public GenGewgawQ genGawgawQ = GenGewgawQ.None; 
        public int qty; 
        public AllItemNames itemName;
    }
    [Serializable]
    public class NPCWeeklyStockData
    {
        public int WeekInYear;
        public List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();

        public NPCWeeklyStockData(int weekInYear, List<ItemDataWithQty> allItemData)
        {
            WeekInYear = weekInYear;
            this.allItemData = allItemData;
        }
    }


}

