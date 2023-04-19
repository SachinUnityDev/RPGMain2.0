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
        public GameObject npcPrefab;  // change to prefab 
        public Sprite npcSprite;
        public Sprite npcHexPortrait;
        public Sprite dialoguePortraitClicked;
        public Sprite dialoguePortraitUnClicked;

        [Header("Items purchaseAble by NPC")]
        public List<ItemType> itemTypesAccepted = new List<ItemType>();

        public BuildingNames buildName;

        public List<WeeklyItemInStock> weeklyItemStock = new List<WeeklyItemInStock>();


        private void Awake()
        {
            if(npcName == NPCNames.AmishTheMerchant)
            {
                //for (int i = 0; i < 4; i++)
                //{
                //    ItemDataLs itemDataLs = new ItemDataLs()
                //}


            }
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
        public Pouches pouchesName; 
    }

    [Serializable]
    public class ItemDataLs
    {

        public ItemType itemType;
        public AllItemNames itemName;
        public GenGewgawQ genGawgawQ = GenGewgawQ.None; 
        public int qty; 
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

