using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Interactables
{
    public class ItemFactory : MonoBehaviour
    {
    
        Dictionary<Iitems, Type> allItems = new Dictionary<Iitems, Type>();
       
        [Header("GEMS")]
        Dictionary<GemNames, Type> allGems = new Dictionary<GemNames, Type>();

        [Header("GENERIC GEWGAWS")]
        Dictionary<GenGewgawNames, Type> allGenGewgaws = new Dictionary<GenGewgawNames, Type>();
        public Dictionary<PrefixNames, Type> allPrefixes = new Dictionary<PrefixNames, Type>();
        public Dictionary<SuffixNames, Type> allSuffixes = new Dictionary<SuffixNames, Type>();

        [Header("SAGAIC GEWGAWS")]
        Dictionary<SagaicGewgawNames,  Type> allSagaicGewgaws = new Dictionary<SagaicGewgawNames, Type>();

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



        LoreScroll loreScroll; 


        [SerializeField] int itemId = -1; 

        [SerializeField] int gemsCount = 0;
        [SerializeField] int genGewgawCount = 0;
        [SerializeField] int sagaicGewgawCount = 0;
        [SerializeField] int potionCount = 0;
        [SerializeField] int herbCount = 0;
        [SerializeField] int fruitCount = 0;
        [SerializeField] int foodCount = 0; 
        [SerializeField] int toolCount = 0;
        [SerializeField] int ingredCount = 0;
        [SerializeField] int scrollCount = 0;
        [SerializeField] int TgCount = 0; 
        void Start()
        {
            itemId =-1;
            ItemInit();
        }

        void ItemInit()
        {
            PotionInit();           
            GenGewGawInit();
            InitPrefixes();
            InitSuffixes();     
            HerbsInit();            
            FoodInit();
            FruitInit();
            GemsInit();
            SagaicGewgawInit();
            IngredInit();
            EnchantmentScrollsInit();
            TradeGoodsInit();
            ToolsInit();

        }
        public Iitems GetNewGenGewgaw(GenGewgawNames genGewgawNames, GenGewgawQ genGewgawQ)
        {
            Iitems itemGengewgaw = GetGenGewgaws(genGewgawNames, genGewgawQ);
            GenGewgawSO genGewgawSO = ItemService.Instance.GetGenGewgawSO(genGewgawNames);
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
                    PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)itemName);
                    itemPotion.InitItem(itemId, potionSO.maxInvStackSize);
                    return itemPotion;
                //case ItemType.GenGewgaws:
                //    //Iitems itemGengewgaw = GetGenGewgaws((GenGewgawNames)itemName);                     
                //    //GenGewgawSO genGewgawSO = ItemService.Instance.GetGenGewgawSO((GenGewgawNames)itemName);
                //    //itemGengewgaw.InitItem(itemId, genGewgawSO.maxInvStackSize);
                //    //return itemGengewgaw;
                case ItemType.Herbs:
                    Iitems itemHerbs = GetNewHerbItem((HerbNames)itemName);
                    HerbSO herbSO = ItemService.Instance.GetHerbSO((HerbNames)itemName);
                    itemHerbs.InitItem(itemId, herbSO.maxInvStackSize);
                    return itemHerbs;
                case ItemType.Foods:
                    Iitems itemFoods = GetNewFoodItem((FoodNames)itemName);
                    FoodSO foodSO = ItemService.Instance.GetFoodSO((FoodNames)itemName);
                    itemFoods.InitItem(itemId, foodSO.maxInvStackSize);
                    return itemFoods;
                case ItemType.Fruits:
                    break;
                case ItemType.Ingredients:
                    Iitems itemIngred = GetNewIngredItem((IngredNames)itemName);
                    IngredSO ingredSO = ItemService.Instance.GetIngredSO((IngredNames)itemName);
                    itemIngred.InitItem(itemId, ingredSO.maxInvStackSize);
                    return itemIngred;
                case ItemType.Recipes:
                    break;
                case ItemType.Scrolls:
                    Iitems itemScrolls = GetNewScrollItem((ScrollNames)itemName);
                    ScrollSO scrollSO = ItemService.Instance.GetScrollSO((ScrollNames)itemName);
                    itemScrolls.InitItem(itemId, scrollSO.maxInvStackSize);
                    return itemScrolls;
                    
                case ItemType.TradeGoods:
                    Iitems itemTg = GetNewTgItem((TGNames)itemName);
                    TGSO tgSO = ItemService.Instance.GetTradeGoodsSO((TGNames)itemName);
                    itemTg.InitItem(itemId, tgSO.maxInvStackSize);
                    return itemTg; 
                  
                case ItemType.Tools:
                    Iitems tool = GetNewToolItem((ToolNames)itemName);
                    ToolsSO toolSO = ItemService.Instance.GetToolSO((ToolNames)itemName);
                    tool.InitItem(itemId, toolSO.maxInvStackSize);
                    return tool;                    
                case ItemType.Teas: // not in demo 
                    break;
                case ItemType.Soups:// not in demo 
                    break;
                case ItemType.Gems:
                    Iitems itemGems = GetNewGemItem((GemNames)itemName);
                    GemSO gemSO = ItemService.Instance.GetGemSO((GemNames)itemName);
                    itemGems.InitItem(itemId, gemSO.maxInvStackSize);
                    return itemGems;
                case ItemType.Alcohol:// not in demo 
                    break;
                case ItemType.Meals:// not in demo 
                    break;
                case ItemType.SagaicGewgaws:
                    Iitems sagaicGewgaw = GetNewSagaicGewgaw((SagaicGewgawNames)itemName);
                    SagaicGewgawSO sagaicGewgawSO = ItemService.Instance
                                                    .GetSagaicGewgawSO((SagaicGewgawNames)itemName);
                    sagaicGewgaw.InitItem(itemId, sagaicGewgawSO.maxInvStackSize);
                    return sagaicGewgaw;                    
                case ItemType.PoeticGewgaws:
                    break;
                case ItemType.RelicGewgaws: // not in demo 
                    break;
                default:
                    break;
            }

            return null;
        }

        #region GEMS 
        public void GemsInit()
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

        public Iitems GetNewGemItem(GemNames _gemName)
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
        public void GenGewGawInit()
        {
            if (allGenGewgaws.Count > 0) return;

            var getallGenGewgaws = Assembly.GetAssembly(typeof(GenGewgawBase)).GetTypes()
                                     .Where(myType => myType.IsClass
                                     && !myType.IsAbstract && myType.IsSubclassOf(typeof(GenGewgawBase)));           
   
            foreach (var genGewgaws in getallGenGewgaws)
            {
                var t = Activator.CreateInstance(genGewgaws) as GenGewgawBase;
                allGenGewgaws.Add(t.genGewgawNames, genGewgaws);
            }
            genGewgawCount = allGenGewgaws.Count; 
        }
        public void InitSuffixes()
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
        public void InitPrefixes()
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
        public Iitems GetGenGewgaws(GenGewgawNames genGewgawName, GenGewgawQ genGewgawQ)
        {
            GenGewgawSO genGewgawSO = ItemService.Instance.GetGenGewgawSO(genGewgawName);

            SuffixNames suffixName = genGewgawSO.suffixName;
            PrefixNames prefixName = genGewgawSO.prefixName;

            foreach (var gewgaw in allGenGewgaws)
            {
                if (gewgaw.Key == genGewgawName)
                {
                    var t = Activator.CreateInstance(gewgaw.Value) as GenGewgawBase;
                    // is mutated as gengewgawBase as prefix and Suffix are to be allocated

                    t.suffixBase = GetSuffixBase(suffixName);
                    t.prefixBase = GetPrefixBase(prefixName);   
                    if(t.suffixBase != null)
                    t.suffixBase.SuffixInit(genGewgawQ);
                    if (t.prefixBase != null)
                        t.prefixBase.PrefixInit(genGewgawQ);

                    return t as Iitems;
                }
            }
            Debug.Log("gewgawbase class Not found" + genGewgawName);
            return null;

        }

        public SuffixBase GetSuffixBase(SuffixNames _suffixName)
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
   

        public PrefixBase GetPrefixBase(PrefixNames _prefixName)
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
        public void SagaicGewgawInit()
        {
            if (allSagaicGewgaws.Count > 0) return;
            var getSagaicGewgaws = Assembly.GetAssembly(typeof(SagaicGewgawBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(SagaicGewgawBase)));

            foreach (var getSagaic in getSagaicGewgaws)
            {
                var t = Activator.CreateInstance(getSagaic) as SagaicGewgawBase;
                allSagaicGewgaws.Add(t.sagaicgewgawName, getSagaic);               
            }
            sagaicGewgawCount = allSagaicGewgaws.Count;
        }

        public Iitems GetNewSagaicGewgaw(SagaicGewgawNames sagaicName)
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

        #region POTIONS 

        public void PotionInit()
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

        public Iitems GetNewPotionItem(PotionNames _PotionName)
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
        public void HerbsInit()
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

        public Iitems GetNewHerbItem(HerbNames _herbName)
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
        public void FoodInit()
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

        public Iitems GetNewFoodItem(FoodNames _foodName)
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
        public void FruitInit()
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

        public Iitems GetNewFruitItem(FruitNames _fruitName)
        {
            foreach (var fruit in allFruits)
            {
                if (fruit.Key == _fruitName)
                {
                    var t = Activator.CreateInstance(fruit.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("Food base class Not found" + _fruitName);
            return null;
        }

        # endregion

        #region INGREDIENTS
        public void IngredInit()
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

        public Iitems GetNewIngredItem(IngredNames ingredName)
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

        #region SCROLLS 
        public void EnchantmentScrollsInit()
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

        public Iitems GetNewScrollItem(ScrollNames scrollName)
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

        #region LORE SCROLLS

        public LoreScroll GetLoreScroll()
        {
            return loreScroll; 
        }

        #endregion

        #region TRADE GOODS

        public void TradeGoodsInit()
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

        public Iitems GetNewTgItem(TGNames tgName)
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
        public void ToolsInit()
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

        public Iitems GetNewToolItem(ToolNames toolName)
        {
            foreach (var tool in allTools)
            {
                if (tool.Key == toolName)
                {
                    var t = Activator.CreateInstance(tool.Value) as Iitems;
                    return t;
                }
            }
            Debug.Log("   tool Base class Not found" + toolName);
            return null;
        }



        #endregion
    }


}


