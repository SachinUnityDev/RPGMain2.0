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
        Dictionary<GemName, Type> allGems = new Dictionary<GemName, Type>();

        [Header("GENERIC GEWGAWS")]
        Dictionary<GenGewgawNames, Type> allGenGewgaws = new Dictionary<GenGewgawNames, Type>();
        public Dictionary<PrefixNames, Type> allPrefixes = new Dictionary<PrefixNames, Type>();
        public Dictionary<SuffixNames, Type> allSuffixes = new Dictionary<SuffixNames, Type>();

        [Header("SAGAIC GEWGAWS")]
        Dictionary<SagaicGewgawNames,  Type> allSagaicGewgaws = new Dictionary<SagaicGewgawNames, Type>();

        [Header("POTIONS")]
        Dictionary<PotionName, Type> allPotions= new Dictionary<PotionName, Type>();

        [Header("HERBS")]
        Dictionary<HerbNames, Type> allHerbs = new Dictionary<HerbNames, Type>();

        [Header("TOOLS")]
        Dictionary<ToolNames, Type> allTools = new Dictionary<ToolNames, Type>();

        [Header("Ingredient")]
        Dictionary<IngredNames, Type> allIngreds = new Dictionary<IngredNames, Type>();

        [SerializeField] int itemId = -1; 

        [SerializeField] int gemsCount = 0;
        [SerializeField] int genGewgawCount = 0;
        [SerializeField] int sagaicGewgawCount = 0;
        [SerializeField] int potionCount = 0;
        [SerializeField] int herbCount = 0;
        [SerializeField] int toolCount = 0;
        [SerializeField] int ingredCount = 0;
        void Start()
        {
            itemId =-1;
            ItemInit();
        }

        void ItemInit()
        {
            PotionInit();           
            GenGewGawInit();
            HerbsInit();
            GemsInit();
            SagaicGewgawInit();
            IngredInit();
        }
        public Iitems GetNewItem(ItemType itemType, int itemName)
        {
            switch (itemType)
            {
                case ItemType.Potions:
                    return GetNewPotionItem((PotionName)itemName);
                case ItemType.GenGewgaws:
                    return GetGenGewgaws((GenGewgawNames)itemName);
                case ItemType.Herbs:
                    return GetNewHerbItem((HerbNames)itemName);
                case ItemType.Foods:
                    break;
                case ItemType.Fruits:
                    break;
                case ItemType.Ingredients:
                    return GetNewIngredItem((IngredNames)itemName);
                 
                case ItemType.Recipes:
                    break;
                case ItemType.Scrolls:
                    break;
                case ItemType.TradeGoods:
                    break;
                case ItemType.Tools:
                    break;
                case ItemType.Teas: // not in demo 
                    break;
                case ItemType.Soups:// not in demo 
                    break;
                case ItemType.Gems:
                    return GetNewGemItem((GemName)itemName);
                case ItemType.Alcohol:// not in demo 
                    break;
                case ItemType.Meals:// not in demo 
                    break;
                case ItemType.SagaicGewgaws:
                    return GetNewSagaicGewgaw((SagaicGewgawNames)itemName);
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

        public Iitems GetNewGemItem(GemName _gemName)
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
            InitPrefixes();
            InitSuffixes();
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
        public Iitems GetGenGewgaws(GenGewgawNames genGewgawName)
        {
            GenGewgawSO genGewgawSO = ItemService.Instance.GetGewgawSO(genGewgawName);

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

        public Iitems GetNewPotionItem(PotionName _PotionName)
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


    }


}


