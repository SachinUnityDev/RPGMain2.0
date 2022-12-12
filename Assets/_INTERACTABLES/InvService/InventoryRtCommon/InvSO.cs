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

        public Sprite GetBGSprite(Iitems item)
        {
            if(item.itemType == ItemType.GenGewgaws)
            {
                GenGewgawQ suffixQuality = GenGewgawQ.None;
                GenGewgawQ prefixQuality = GenGewgawQ.None; 
                GenGewgawBase genGewgawbase = (GenGewgawBase)item;
                SuffixBase suffixBase = genGewgawbase.suffixBase;
                if(suffixBase != null)
                 suffixQuality= suffixBase.genGewgawQ;

                PrefixBase prefixBase = genGewgawbase.prefixBase;
                if (prefixBase != null)
                    prefixQuality = prefixBase.genGewgawQ;

                if(suffixQuality == GenGewgawQ.Lyric|| prefixQuality == GenGewgawQ.Lyric)
                {
                    return filledSlot; 
                }
                if (suffixQuality == GenGewgawQ.Folkloric || prefixQuality == GenGewgawQ.Folkloric)
                {
                    return folkloricSlot; 
                }
                if (suffixQuality == GenGewgawQ.Epic || prefixQuality == GenGewgawQ.Epic)
                {
                    return epicSlot;
                }

            }else if (item.itemType == ItemType.SagaicGewgaws)
            {
                return sagaicSlot;
            }else if (item.itemType == ItemType.PoeticGewgaws)
            {
                return poeticSlot;
            }
            else
            {
                return filledSlot;
            }
            return emptySlot;// if empty pass item no


        }

        public Sprite GetSprite(int itemName, ItemType itemType)
        {
            Sprite sprite = null; 
            switch (itemType)
            {
                case ItemType.Potions:
                    sprite = ItemService.Instance.GetPotionSO((PotionNames)itemName).iconSprite;                         
                    break;
                case ItemType.GenGewgaws:
                    sprite = ItemService.Instance.GetGenGewgawSO((GenGewgawNames)itemName).iconSprite;
                    break;
                case ItemType.Herbs:
                    sprite = ItemService.Instance.GetHerbSO((HerbNames)itemName).iconSprite;
                    break;
                case ItemType.Foods:
                    sprite = ItemService.Instance.GetFoodSO((FoodNames)itemName).iconSprite;
                    break;
                case ItemType.Fruits:
                    sprite = ItemService.Instance.GetFruitSO((FruitNames)itemName).iconSprite;
                    break;
                case ItemType.Ingredients:
                    sprite = ItemService.Instance.GetIngredSO((IngredNames)itemName).iconSprite;
                    break;
                case ItemType.Recipes:
                   // sprite = ItemService.Instance.GetCookingRecipeSO(itemName).iconSprite;
                    break;
                case ItemType.Scrolls:
                    sprite = ItemService.Instance.GetScrollSO((ScrollNames)itemName).iconSprite;
                    break;
                case ItemType.TradeGoods:
                    sprite = ItemService.Instance.GetTradeGoodsSO((TGNames)itemName).iconSprite;
                    break;
                case ItemType.Tools:
                    sprite = ItemService.Instance.GetToolSO((ToolNames)itemName).iconSprite;
                    break;
                case ItemType.Teas:// cannot be carried in inv
                                   // sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
                case ItemType.Soups: // cannot be carried in inv
                                     // sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
                case ItemType.Gems:
                    sprite = ItemService.Instance.GetGemSO((GemNames)itemName).iconSprite;
                    break;
                case ItemType.Alcohol: // cannot be carried in inv
                                       // sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
                case ItemType.Meals:  // cannot be carried in inv
                    //sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
              
                default:
                    break;

            }
            return sprite;

            
        }


    }



}

