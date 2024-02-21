using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Interactables
{
    [System.Serializable]
    public class GenGewgawData
    {
        public GenGewgawNames genGewgawName;
        public GenGewgawQ genGewgawQ;
        public Type type;

        public GenGewgawData(GenGewgawNames genGewgawName, GenGewgawQ genGewgawQ, Type type)
        {
            this.genGewgawName = genGewgawName;
            this.genGewgawQ = genGewgawQ;
            this.type = type;
        }
    }

    public class ItemFactory : MonoBehaviour
    {
    
        Dictionary<Iitems, Type> allItems = new Dictionary<Iitems, Type>();
       
        [Header("GEMS")]
        Dictionary<GemNames, Type> allGems = new Dictionary<GemNames, Type>();

        [Header("GENERIC GEWGAWS")]
        public List<GenGewgawData> allGenGewgawData = new List<GenGewgawData>();
        public Dictionary<PrefixNames, Type> allPrefixes = new Dictionary<PrefixNames, Type>();
        public Dictionary<SuffixNames, Type> allSuffixes = new Dictionary<SuffixNames, Type>();

        [Header("SAGAIC GEWGAWS")]
        Dictionary<SagaicGewgawNames,  Type> allSagaicGewgaws = new Dictionary<SagaicGewgawNames, Type>();

        [Header("Poetic GEWGAWS")]
        Dictionary<PoeticGewgawNames, Type> allPoeticGewgaws = new Dictionary<PoeticGewgawNames, Type>();


        [Header("POTIONS")]
        Dictionary<PotionNames, Type> allPotions= new Dictionary<PotionNames, Type>();

        [Header("HERBS")]
        Dictionary<HerbNames, Type> allHerbs = new Dictionary<HerbNames, Type>();

        [Header("FOODS")]
        Dictionary<FoodNames, Type> allFoods = new Dictionary<FoodNames, Type>();

        [Header("FRUITS")]
        Dictionary<FruitNames, Type> allFruits = new Dictionary<FruitNames, Type>();

        [Header("TOOLS")]
        Dictionary<ToolNames, Type> allTools = new Dictionary<ToolNames, Type>();

        [Header("Ingredient")]
        Dictionary<IngredNames, Type> allIngreds = new Dictionary<IngredNames, Type>();

        [Header("Scrolls")]
        Dictionary<ScrollNames, Type> allScrolls = new Dictionary<ScrollNames, Type>();

        [Header("Trade goods")]
        Dictionary<TGNames, Type> allTradeGoods = new Dictionary<TGNames, Type>();

        [Header("Recipe Factory")]
        Dictionary<ItemData, Type> allRecipes = new Dictionary<ItemData, Type>();

        [Header("Meals")]
        Dictionary<MealNames, Type> allMeals = new Dictionary<MealNames, Type>();

        [Header("Alcohol")]
        Dictionary<AlcoholNames, Type> allAcohols  = new Dictionary<AlcoholNames, Type>();

        [Header("Soups")]
        Dictionary<SoupNames, Type> allSoups = new Dictionary<SoupNames, Type>();
        
        [Header("Tea")]
        Dictionary<TeaNames, Type> allTea = new Dictionary<TeaNames, Type>();
        [Header("Lore Books")]
        Dictionary<LoreBookNames, Type> allLoreBooks = new Dictionary<LoreBookNames, Type>();

        [SerializeField] int itemId = -1; 

        [SerializeField] int gemsCount = 0;
        [SerializeField] int genGewgawCount = 0;
        [SerializeField] int sagaicGewgawCount = 0;
        [SerializeField] int poeticGewgawCount = 0;
        [SerializeField] int potionCount = 0;
        [SerializeField] int herbCount = 0;
        [SerializeField] int fruitCount = 0;
        [SerializeField] int foodCount = 0; 
        [SerializeField] int toolCount = 0;
        [SerializeField] int ingredCount = 0;
        [SerializeField] int scrollCount = 0;
        [SerializeField] int TgCount = 0;
        [SerializeField] int recipeCount = 0;
        [SerializeField] int mealCount = 0;
        [SerializeField] int alcoholCount = 0;
        [SerializeField] int soupCount = 0;
        [SerializeField] int teaCount = 0;
        [SerializeField] int loreBookCount = 0; 

        void Start()
        {
            
        }

        public void ItemInit()
        {
            itemId = -1;
            
            PotionInit();           
            GenGewGawInit();              
            HerbsInit();            
            FoodInit();
            FruitInit();
            GemsInit();
            SagaicGewgawInit();
            PoeticGewgawInit();
            IngredInit();
            EnchantmentScrollsInit();
            TradeGoodsInit();
            ToolsInit();
            MealsInit();
            AlcoholInit();
            LoreBooksInit(); 
           // RecipeInit(); 
        }
        public Iitems GetNewGenGewgaw(GenGewgawNames genGewgawNames, GenGewgawQ genGewgawQ)
        {
            Iitems itemGengewgaw = GetGenGewgaws(genGewgawNames, genGewgawQ);
            GenGewgawSO genGewgawSO = ItemService.Instance.allItemSO.GetGenGewgawSO(genGewgawNames);
            GenGewgawBase genGewgawBase = itemGengewgaw as GenGewgawBase;

            genGewgawBase.prefixBase = GetPrefixBase(genGewgawSO.prefixName); 
            genGewgawBase.suffixBase = GetSuffixBase(genGewgawSO.suffixName);
            genGewgawBase.genGewgawQ = genGewgawQ;
            itemGengewgaw.InitItem(itemId, genGewgawSO.maxInvStackSize);
            return itemGengewgaw;
        }


        public Iitems GetNewItem(ItemType itemType, int itemName)
        {
            itemId++;
            switch (itemType)
            {
                case ItemType.Potions:
                    Iitems itemPotion = GetNewPotionItem((PotionNames)itemName);
                    PotionSO potionSO = ItemService.Instance.allItemSO.GetPotionSO((PotionNames)itemName);                    
                    itemPotion.InitItem(itemId, potionSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemPotion);    
                    return itemPotion;
                //case ItemType.GenGewgaws:
                //    //Iitems itemGengewgaw = GetGenGewgaws((GenGewgawNames)itemName);                     
                //    //GenGewgawSO genGewgawSO = ItemService.Instance.GetGenGewgawSO((GenGewgawNames)itemName);
                //    //itemGengewgaw.InitItem(itemId, genGewgawSO.maxInvStackSize);
                //    //return itemGengewgaw;
                case ItemType.Herbs:
                    Iitems itemHerbs = GetNewHerbItem((HerbNames)itemName);
                    HerbSO herbSO = ItemService.Instance.allItemSO.GetHerbSO((HerbNames)itemName);              
                    itemHerbs.InitItem(itemId, herbSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemHerbs);
                    return itemHerbs;
                case ItemType.Foods:
                    Iitems itemFoods = GetNewFoodItem((FoodNames)itemName);
                    FoodSO foodSO = ItemService.Instance.allItemSO.GetFoodSO((FoodNames)itemName);
                    itemFoods.InitItem(itemId, foodSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemFoods);
                    return itemFoods;
                case ItemType.Fruits:
                    Iitems itemFruits = GetNewFruitItem((FruitNames)itemName);
                    FruitSO fruitSO = ItemService.Instance.allItemSO.GetFruitSO((FruitNames)itemName);
                    itemFruits.InitItem(itemId, fruitSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemFruits);
                    return itemFruits;
                case ItemType.Ingredients:
                    Iitems itemIngred = GetNewIngredItem((IngredNames)itemName);
                    IngredSO ingredSO = ItemService.Instance.allItemSO.GetIngredSO((IngredNames)itemName);
                    itemIngred.InitItem(itemId, ingredSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemIngred);
                    return itemIngred;
                case ItemType.XXX:     
                    break;
                case ItemType.Scrolls:
                    Iitems itemScrolls = GetNewScrollItem((ScrollNames)itemName);
                    ScrollSO scrollSO = ItemService.Instance.allItemSO.GetScrollSO((ScrollNames)itemName);
                    itemScrolls.InitItem(itemId, scrollSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemScrolls);
                    return itemScrolls;
                    
                case ItemType.TradeGoods:
                    Iitems itemTg = GetNewTgItem((TGNames)itemName);
                    TGSO tgSO = ItemService.Instance.allItemSO.GetTradeGoodsSO((TGNames)itemName);
                    itemTg.InitItem(itemId, tgSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemTg);
                    return itemTg; 
                  
                case ItemType.Tools:
                    Iitems toolItem = GetNewToolItem((ToolNames)itemName);
                    ToolsSO toolSO = ItemService.Instance.allItemSO.GetToolSO((ToolNames)itemName);
                    toolItem.InitItem(itemId, toolSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(toolItem);
                    return toolItem;                    
                case ItemType.Teas: // not in demo 
                    break;
                case ItemType.Soups:// not in demo 
                    break;
                case ItemType.Gems:
                    Iitems itemGems = GetNewGemItem((GemNames)itemName);
                    GemSO gemSO = ItemService.Instance.allItemSO.GetGemSO((GemNames)itemName);
                    itemGems.InitItem(itemId, gemSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(itemGems);
                    return itemGems;
                case ItemType.Alcohol:// not in demo 
                    Iitems alcoholItem = GetNewAlcoholItem((AlcoholNames)itemName);
                    AlcoholSO alcoholSO = ItemService.Instance.allItemSO
                                                    .GetAlcoholSO((AlcoholNames)itemName);
                    alcoholItem.InitItem(itemId, alcoholSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(alcoholItem);
                    return alcoholItem;
                case ItemType.Meals:
                    Iitems mealItem = GetNewMealItem((MealNames)itemName);
                    MealsSO mealSO = ItemService.Instance.allItemSO
                                                    .GetMealSO((MealNames)itemName);
                    mealItem.InitItem(itemId, mealSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(mealItem);
                    return mealItem;
                case ItemType.SagaicGewgaws:
                    Iitems sagaicGewgaw = GetNewSagaicGewgaw((SagaicGewgawNames)itemName);
                    SagaicGewgawSO sagaicGewgawSO = ItemService.Instance.allItemSO
                                                    .GetSagaicGewgawSO((SagaicGewgawNames)itemName);
                    sagaicGewgaw.InitItem(itemId, sagaicGewgawSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(sagaicGewgaw);
                    return sagaicGewgaw;                    
                case ItemType.PoeticGewgaws:
                    Iitems poeticGewgaw = GetNewPoeticGewgaw((PoeticGewgawNames)itemName);
                    PoeticGewgawSO poeticGewgawSO = ItemService.Instance.allItemSO
                                                    .GetPoeticGewgawSO((PoeticGewgawNames)itemName);
                    poeticGewgaw.InitItem(itemId, poeticGewgawSO.maxInvStackSize);
                    ItemService.Instance.allItemsInGame.Add(poeticGewgaw);
                    return poeticGewgaw;                    
                case ItemType.RelicGewgaws: // not in demo 
                    break;
                case ItemType.LoreBooks:
                    Iitems loreBook = GetNewLoreBookItem((LoreBookNames)itemName);
                    LoreBookSO LoreBookSO = ItemService.Instance.allItemSO
                                                    .GetLoreBookSO((LoreBookNames)itemName);
                    loreBook.InitItem(itemId, LoreBookSO.inventoryStack);
                    ItemService.Instance.allItemsInGame.Add(loreBook);
                    return loreBook;
                default:
                    break;
            }

            return null;
        }

        #region GEMS 
         void GemsInit()
        {
            if (allGems.Count > 0) return;
            var getGems = Assembly.GetAssembly(typeof(GemBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(GemBase)));

            foreach (var getGem in getGems)
            {
                var t = Activator.CreateInstance(getGem) as GemBase;
                
                allGems.Add(t.gemName, getGem);
            }
            gemsCount = allGems.Count;
        }

         Iitems GetNewGemItem(GemNames _gemName)
        {
            foreach (var gem in allGems)
            {
                if (gem.Key == _gemName)
                {
                    var t = Activator.CreateInstance(gem.Value) as Iitems;                    
                    return t;
                }
            }
            return null;
        }

        #endregion

        #region GENERIC GEWGAWS 
         void GenGewGawInit()
        {
            //if (allGenGewgaws.Count > 0) return;
            InitPrefixes();
            InitSuffixes();

            var getallGenGewgaws = Assembly.GetAssembly(typeof(GenGewgawBase)).GetTypes()
                                     .Where(myType => myType.IsClass
                                     && !myType.IsAbstract && myType.IsSubclassOf(typeof(GenGewgawBase)));           
   
            foreach (var genGewgaws in getallGenGewgaws)
            {
                var t = Activator.CreateInstance(genGewgaws) as GenGewgawBase;
                
                GenGewgawSO genGewgawSO = ItemService.Instance.allItemSO.GetGenGewgawSO(t.genGewgawNames);                
                SuffixNames suffixName = genGewgawSO.suffixName;
                PrefixNames prefixName = genGewgawSO.prefixName;

                SuffixBase suffixBase = GetSuffixBase(suffixName);
                PrefixBase prefixBase = GetPrefixBase(prefixName);

                IEpic epic1 = suffixBase as IEpic;
                IEpic epic2 = prefixBase as IEpic;
                if(epic1 != null || epic2 != null)
                {
                    var x = Activator.CreateInstance(genGewgaws) as GenGewgawBase;
                    x.GenGewgawInit(GenGewgawQ.Epic);
                    GenGewgawData genGewgawData 
                                    = new GenGewgawData(x.genGewgawNames, GenGewgawQ.Epic, genGewgaws);
                    allGenGewgawData.Add(genGewgawData);
                }
                
                IFolkloric folkLoric1 = suffixBase as IFolkloric;
                IFolkloric folkLoric2 = prefixBase as IFolkloric;
                if (folkLoric1 != null || folkLoric2 != null)
                {
                    var y = Activator.CreateInstance(genGewgaws) as GenGewgawBase;  
                    y.GenGewgawInit(GenGewgawQ.Folkloric);
                    GenGewgawData genGewgawData 
                                    = new GenGewgawData(y.genGewgawNames, GenGewgawQ.Folkloric, genGewgaws);
                    allGenGewgawData.Add(genGewgawData);
                }
                ILyric lyric1 = suffixBase as ILyric;
                ILyric lyric2 = prefixBase as ILyric;
                if (lyric1 != null || lyric2 != null)
                {
                    var z = Activator.CreateInstance(genGewgaws) as GenGewgawBase;
                    z.GenGewgawInit(GenGewgawQ.Lyric);
                    GenGewgawData genGewgawData 
                                = new GenGewgawData(z.genGewgawNames, GenGewgawQ.Lyric, genGewgaws);
                    allGenGewgawData.Add(genGewgawData);
                }
            }
            genGewgawCount = allGenGewgawData.Count; 
        }
         void InitSuffixes()
        {
            if (allSuffixes.Count > 0) return;

            var getallsuffixes = Assembly.GetAssembly(typeof(SuffixBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(SuffixBase)));

            foreach (var suffix in getallsuffixes)
            {
                var t = Activator.CreateInstance(suffix) as SuffixBase;
                Debug.Log("KEY NAME " + t.suffixName);
                allSuffixes.Add(t.suffixName, suffix);
            }
        }
         void InitPrefixes()
        {
            if (allPrefixes.Count > 0) return;

            var getallPrefixes = Assembly.GetAssembly(typeof(PrefixBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(PrefixBase)));

            foreach (var prefix in getallPrefixes)
            {
                var t = Activator.CreateInstance(prefix) as PrefixBase;
                allPrefixes.Add(t.prefixName, prefix);
            }
        }
         Iitems GetGenGewgaws(GenGewgawNames genGewgawName, GenGewgawQ genGewgawQ)
        {         
            foreach (var gewgaw in allGenGewgawData)
            {
                if (gewgaw.genGewgawName == genGewgawName && gewgaw.genGewgawQ == genGewgawQ)
                {
                    var t = Activator.CreateInstance(gewgaw.type) as GenGewgawBase;
                    return t as Iitems;
                }
            }
            Debug.Log("gewgawbase class Not found" + genGewgawName);
            return null;

        }

         SuffixBase GetSuffixBase(SuffixNames _suffixName)
        {
            foreach (var suffix in allSuffixes)
            {
                if (suffix.Key == _suffixName)
                {
                    var t = Activator.CreateInstance(suffix.Value) as SuffixBase;
                    return t;
                }
            }
            Debug.Log("suffix class Not found" + _suffixName);
            return null;
        }
   

         PrefixBase GetPrefixBase(PrefixNames _prefixName)
        {
            foreach (var prefix in allPrefixes)
            {
                if (prefix.Key == _prefixName)
                {
                    var t = Activator.CreateInstance(prefix.Value) as PrefixBase;
                    return t;
                }
            }
            Debug.Log("Prefix class Not found" + _prefixName);
            return null;
        }
        #endregion

        #region  SAGAIC GEWGAWS 
         void SagaicGewgawInit()
        {
            if (allSagaicGewgaws.Count > 0) return;
            var getSagaicGewgaws = Assembly.GetAssembly(typeof(SagaicGewgawBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(SagaicGewgawBase)));

            foreach (var getSagaic in getSagaicGewgaws)
            {
                var t = Activator.CreateInstance(getSagaic) as SagaicGewgawBase;
                allSagaicGewgaws.Add(t.sagaicGewgawName, getSagaic);               
            }
            sagaicGewgawCount = allSagaicGewgaws.Count;
        }

         Iitems GetNewSagaicGewgaw(SagaicGewgawNames sagaicName)
        {
            foreach (var sagaic in allSagaicGewgaws)
            {
                if (sagaic.Key == sagaicName)
                {
                    var t = Activator.CreateInstance(sagaic.Value) as Iitems;
                    return t;
                }
            }
            return null;
        }
        #endregion

        #region POETIC GEWGAWS
         void PoeticGewgawInit()
        {
            if (allPoeticGewgaws.Count > 0) return;
            var getPoeticGewgaws = Assembly.GetAssembly(typeof(PoeticGewgawBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(PoeticGewgawBase)));

            foreach (var getPoetic in getPoeticGewgaws)
            {
                var t = Activator.CreateInstance(getPoetic) as PoeticGewgawBase;
                allPoeticGewgaws.Add(t.poeticGewgawName, getPoetic);
            }
            poeticGewgawCount = allPoeticGewgaws.Count;
        }

         Iitems GetNewPoeticGewgaw(PoeticGewgawNames poeticName)
        {
            foreach (var poetic in allPoeticGewgaws)
            {
                if (poetic.Key == poeticName)
                {
                    var t = Activator.CreateInstance(poetic.Value) as Iitems;
                    return t;
                }
            }
            return null;
        }


        #endregion 



        #region POTIONS 

         void PotionInit()
        {
            if (allPotions.Count > 0) return;

            var getAllPotions = Assembly.GetAssembly(typeof(PotionsBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(PotionsBase)));

            foreach (var potion in getAllPotions)
            {
                var t = Activator.CreateInstance(potion) as PotionsBase;
                allPotions.Add(t.potionName, potion);
            }
            potionCount = allPotions.Count;
        }

         Iitems GetNewPotionItem(PotionNames _PotionName)
        {
            foreach (var potion in allPotions)
            {
                if (potion.Key == _PotionName)
                {
                    var t = Activator.CreateInstance(potion.Value) as Iitems;
                    // itemID 
                    // slotypeto be updated on call pos
                    // max stack size to be updated there itself 

                    return t;
                }
            }
            Debug.Log("Potion class Not found" + _PotionName);
            return null;
        }
        #endregion

        #region HERBS
         void HerbsInit()
        {
            if (allHerbs.Count > 0) return;
            var getHerbs = Assembly.GetAssembly(typeof(HerbBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(HerbBase)));

            foreach (var getHerb in getHerbs)
            {
                var t = Activator.CreateInstance(getHerb) as HerbBase;

                allHerbs.Add(t.herbName, getHerb);
            }
            herbCount = allHerbs.Count;
        }

         Iitems GetNewHerbItem(HerbNames _herbName)
        {
            foreach (var herb in allHerbs)
            {
                if (herb.Key == _herbName)
                {
                    var t = Activator.CreateInstance(herb.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Herb base class Not found" + _herbName);
            return null;
        }

        #endregion

        #region FOODS
         void FoodInit()
        {
            if (allFoods.Count > 0) return;
            var getFoods = Assembly.GetAssembly(typeof(Foodbase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(Foodbase)));

            foreach (var getFood in getFoods)
            {
                var t = Activator.CreateInstance(getFood) as Foodbase;

                allFoods.Add(t.foodName, getFood);
            }
            foodCount = allFoods.Count;
        }

         Iitems GetNewFoodItem(FoodNames _foodName)
        {
            foreach (var food in allFoods)
            {
                if (food.Key == _foodName)
                {
                    var t = Activator.CreateInstance(food.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Food base class Not found" + _foodName);
            return null;
        }
        #endregion

        #region FRUITS
        void FruitInit()
        {
            if (allFruits.Count > 0) return;
            var getFruits = Assembly.GetAssembly(typeof(FruitBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(FruitBase)));

            foreach (var getFruit in getFruits)
            {
                var t = Activator.CreateInstance(getFruit) as FruitBase;

                allFruits.Add(t.fruitName, getFruit);
            }
            fruitCount = allFruits.Count;
        }

        Iitems GetNewFruitItem(FruitNames _fruitName)
        {
            foreach (var fruit in allFruits)
            {
                if (fruit.Key == _fruitName)
                {
                    var t = Activator.CreateInstance(fruit.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Fruit base class Not found" + _fruitName);
            return null;
        }

        # endregion

        #region INGREDIENTS
        void IngredInit()
        {
            if (allIngreds.Count > 0) return;
            var getIngreds = Assembly.GetAssembly(typeof(IngredBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(IngredBase)));

            foreach (var getIngred in getIngreds)
            {
                var t = Activator.CreateInstance(getIngred) as IngredBase;

                allIngreds.Add(t.ingredName, getIngred);
            }
            ingredCount = allIngreds.Count;
        }

        Iitems GetNewIngredItem(IngredNames ingredName)
        {
            foreach (var ingred in allIngreds)
            {
                if (ingred.Key == ingredName)
                {
                    var t = Activator.CreateInstance(ingred.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Herb base class Not found" + ingredName);
            return null;
        }

        #endregion

        #region MEALS

        void MealsInit()
        {
            if (allMeals.Count > 0) return;
            var getMeals = Assembly.GetAssembly(typeof(MealBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(MealBase)));

            foreach (var getMeal in getMeals)
            {
                var t = Activator.CreateInstance(getMeal) as MealBase;

                allMeals.Add(t.mealName, getMeal);
            }
            mealCount = allMeals.Count;
        }

        Iitems GetNewMealItem(MealNames _mealName)
        {
            foreach (var meal in allMeals)
            {
                if (meal.Key == _mealName)
                {
                    var t = Activator.CreateInstance(meal.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("meal base class Not found" + _mealName);
            return null;
        }
        #endregion

        #region ALCOHOL
        void AlcoholInit()
        {
            if (allAcohols.Count > 0) return;
            var getAlcohols = Assembly.GetAssembly(typeof(AlcoholBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(AlcoholBase)));

            foreach (var getAlcohol in getAlcohols)
            {
                var t = Activator.CreateInstance(getAlcohol) as AlcoholBase;

                allAcohols.Add(t.alcoholName, getAlcohol);
            }
            alcoholCount = allAcohols.Count;
        }

        Iitems GetNewAlcoholItem(AlcoholNames _alcoholNames)
        {
            foreach (var alcohol in allAcohols)
            {
                if (alcohol.Key == _alcoholNames)
                {
                    var t = Activator.CreateInstance(alcohol.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("alcohol base class Not found" + _alcoholNames);
            return null;
        }

        #endregion

        #region RECIPES
        //public void RecipeInit()
        //{
        //    if (allRecipes.Count > 0) return;
        //    var getRecipes = Assembly.GetAssembly(typeof(IRecipe)).GetTypes()
        //                           .Where(myType => myType.IsClass
        //                           && !myType.IsAbstract && myType.IsSubclassOf(typeof(RecipeBase)));

        //    foreach (var getRecipe in getRecipes)
        //    {
        //        var t = Activator.CreateInstance(getRecipe) as RecipeBase;

        //       allRecipes.Add(new ItemData(t.pdtType, t.pdtName), getRecipe);
        //    }
        //    recipeCount = allRecipes.Count;
        //}

        public Iitems GetNewRecipe(ItemData pdtData)
        {
            foreach (var recipe in allRecipes)
            {
                if (recipe.Key.itemName == pdtData.itemName 
                    && recipe.Key.itemType == pdtData.itemType)
                {
                    var t = Activator.CreateInstance(recipe.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Herb base class Not found" + pdtData.itemType);
            return null;
        }



        # endregion

        #region SCROLLS 
        void EnchantmentScrollsInit()
        {
            if (allScrolls.Count > 0) return;
            var getScrolls = Assembly.GetAssembly(typeof(EnchantScrollBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(EnchantScrollBase)));

            foreach (var getScroll in getScrolls)
            {
                var t = Activator.CreateInstance(getScroll) as EnchantScrollBase;

                allScrolls.Add(t.scrollName, getScroll);
            }
            scrollCount = allScrolls.Count;
        }

        Iitems GetNewScrollItem(ScrollNames scrollName)
        {
            foreach (var scroll in allScrolls)
            {
                if (scroll.Key == scrollName)
                {
                    var t = Activator.CreateInstance(scroll.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("   Scroll Base class Not found" + scrollName);
            return null;
        }



        #endregion

        #region LORE BOOKS

        void LoreBooksInit()
        {
            if (allLoreBooks.Count > 0) return;
            var getBooks = Assembly.GetAssembly(typeof(LoreBookBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(LoreBookBase)));

            foreach (var getBook in getBooks)
            {
                var t = Activator.CreateInstance(getBook) as LoreBookBase;

               allLoreBooks.Add(t.loreName, getBook);
            }
            loreBookCount = allLoreBooks.Count;
        }

        Iitems GetNewLoreBookItem(LoreBookNames loreName)
        {
            foreach (var lb in allLoreBooks)
            {
                if (lb.Key == loreName)
                {
                    var t = Activator.CreateInstance(lb.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Lore book Base class Not found" + loreName);
            return null;
        }
        #endregion

        #region TRADE GOODS

        void TradeGoodsInit()
        {
            if (allTradeGoods.Count > 0) return;
            var getTgs = Assembly.GetAssembly(typeof(TGBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(TGBase)));

            foreach (var getTg in getTgs)
            {
                var t = Activator.CreateInstance(getTg) as TGBase;

                allTradeGoods.Add(t.tgName, getTg);
            }
            TgCount = allTradeGoods.Count;  
        }

        Iitems GetNewTgItem(TGNames tgName)
        {
            foreach (var tg in allTradeGoods)
            {
                if (tg.Key == tgName)
                {
                    var t = Activator.CreateInstance(tg.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("   Trade goods Base class Not found" + tgName);
            return null;
        }

        #endregion

        #region TOOLS 
        void ToolsInit()
        {
            if (allTools.Count > 0) return;
            var getTools = Assembly.GetAssembly(typeof(ToolBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(ToolBase)));

            foreach (var getTool in getTools)
            {
                var t = Activator.CreateInstance(getTool) as ToolBase;
                allTools.Add(t.toolName, getTool);
            }
            toolCount = allTools.Count;
        }

        Iitems GetNewToolItem(ToolNames toolName)
        {
            foreach (var tool in allTools)
            {
                if (tool.Key == toolName)
                {
                    var t = Activator.CreateInstance(tool.Value) as Iitems;
                    return t;
                }
            }
            Debug.LogError("tool Base class Not found" + toolName);
            return null;
        }



        #endregion
    }


}


