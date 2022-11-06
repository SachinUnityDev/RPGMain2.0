using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
namespace Interactables
{

    [Serializable]
    public class ItemActionStrData
    {
        public ItemActions itemActions;
        public string itemStr; 

    }

    [CreateAssetMenu(fileName = "InvSO", menuName = "Inventory Service/InvSO")]

    public class InvSO : ScriptableObject
    {
        [Header("Slot sprites")]
        public Sprite emptySlot;
        public Sprite filledSlot;
        public Sprite epicSlot;
        public Sprite folkloricSlot;
        public Sprite sagaicSlot;
        public Sprite poeticSlot;
        public Sprite relicSlot;
        public Sprite hendiadicSlot;

        [Header("ITEM SOs")]
        public List<PotionSO> allPotions = new List<PotionSO>();
        public List<GemSO> allGems = new List<GemSO>();
        public List<ItemActionStrData> allItemStr = new List<ItemActionStrData>();
        
        public GameObject itemImgPrefab; 
        private void Awake()
        {
 

        }
        public string GetItemActionStrings(ItemActions itemActions)
        {
            string str = allItemStr.Find(t => t.itemActions == itemActions).itemStr;
            if (str != null)
                return str;
            else
                Debug.Log("String not found in SO");
            return null; 
        }

        public Sprite GetSprite(int itemName, ItemType itemType)
        {
            Sprite sprite = null; 
            switch (itemType)
            {
                case ItemType.Potions:
                    sprite = allPotions.Find(t => t.potionName == (PotionName)itemName).iconSprite;                    
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
                    sprite = allGems.Find(t => t.gemName == (GemName)itemName).iconSprite;
                    break;
                case ItemType.Alcohol:
                    break;
                case ItemType.Meals:
                    break;
              
                default:
                    break;

            }
            return sprite;

            
        }


    }



}

