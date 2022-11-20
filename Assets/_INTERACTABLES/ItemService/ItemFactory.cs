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
       
        [Header("individual items")]
        Dictionary<GemName, Type> allGems = new Dictionary<GemName, Type>();
        Dictionary<GenGewgawNames, Type> allGenGewgaws = new Dictionary<GenGewgawNames, Type>(); 
        Dictionary<SagaicGewgawNames,  Type> allSagaicGewgaws = new Dictionary<SagaicGewgawNames, Type>();  
        Dictionary<PotionName, Type> allPotion = new Dictionary<PotionName, Type>();
        Dictionary<HerbNames, Type> allHerbs = new Dictionary<HerbNames, Type>();

        [SerializeField] int gemsCount = 0;
        [SerializeField] int genGewgawCount = 0;
        [SerializeField] int sagaicGewgawCount = 0;
        [SerializeField] int potionCount = 0;
        [SerializeField] int herbCount = 0; 
        void Start()
        {
            GemsCreate();
        }

        public Iitems GetItemList(ItemType itemType, int itemName)
        {
            switch (itemType)
            {
                case ItemType.Potions:


                    break;
                case ItemType.GenGewgaws:
                    break;
                case ItemType.Herbs:
                    break;
                case ItemType.Foods:
                    break;
                case ItemType.Fruits:
                    break;
                case ItemType.Ingredients:
                    break;
                case ItemType.Recipes:
                    break;
                case ItemType.Scrolls:
                    break;
                case ItemType.TradeGoods:
                    break;
                case ItemType.Tools:
                    break;
                case ItemType.Teas:
                    break;
                case ItemType.Soups:
                    break;
                case ItemType.Gems:
                    break;
                case ItemType.Alcohol:
                    break;
                case ItemType.Meals:
                    break;
                default:
                    break;
            }

            return null;
        }

        public void GemsCreate()
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

        public Iitems GetNewGemBase(GemName _gemName)
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
            count = allHerbs.Count;
        }

        public HerbBase GetHerbBase(HerbNames _herbName)
        {
            foreach (var herb in allHerbs)
            {
                if (herb.Key == _herbName)
                {
                    var t = Activator.CreateInstance(herb.Value) as HerbBase;
                    return t;
                }
            }
            Debug.Log("Herb base class Not found" + _herbName);
            return null;
        }





    }


}


