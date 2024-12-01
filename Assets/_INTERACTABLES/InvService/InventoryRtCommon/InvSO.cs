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
            if(item == null) return emptySlot;   
            if(item.itemType == ItemType.GenGewgaws)
            {
                GenGewgawBase genGewgawbase = (GenGewgawBase)item;
                GenGewgawQ genGewgawQ = genGewgawbase.genGewgawQ;  

                if(genGewgawQ == GenGewgawQ.Lyric)
                {
                    return filledSlot; 
                }
                if (genGewgawQ == GenGewgawQ.Folkloric)
                {
                    return folkloricSlot; 
                }
                if (genGewgawQ == GenGewgawQ.Epic)
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
            return emptySlot;
        }
        public Sprite GetSprite(int itemName, ItemType itemType)
        {
            Sprite sprite = null; 
            switch (itemType)
            {
                case ItemType.Potions:
                    sprite = ItemService.Instance.allItemSO.GetPotionSO((PotionNames)itemName).iconSprite;                         
                    break;
                case ItemType.GenGewgaws:
                    sprite = ItemService.Instance.allItemSO.GetGenGewgawSO((GenGewgawNames)itemName).iconSprite;
                    break;
                case ItemType.Herbs:
                    sprite = ItemService.Instance.allItemSO.GetHerbSO((HerbNames)itemName).iconSprite;
                    break;
                case ItemType.Foods:
                    sprite = ItemService.Instance.allItemSO.GetFoodSO((FoodNames)itemName).iconSprite;
                    break;
                case ItemType.Fruits:
                    sprite = ItemService.Instance.allItemSO.GetFruitSO((FruitNames)itemName).iconSprite;
                    break;
                case ItemType.Ingredients:
                    sprite = ItemService.Instance.allItemSO.GetIngredSO((IngredNames)itemName).iconSprite;
                    break;
                case ItemType.XXX:
                   // sprite = ItemService.Instance.GetCookingRecipeSO(itemName).iconSprite;
                    break;
                case ItemType.Scrolls:
                    sprite = ItemService.Instance.allItemSO.GetScrollSO((ScrollNames)itemName).iconSprite;
                    break;
                case ItemType.TradeGoods:
                    sprite = ItemService.Instance.allItemSO.GetTradeGoodsSO((TGNames)itemName).iconSprite;
                    break;
                case ItemType.Tools:
                    try
                    {
                        sprite = ItemService.Instance.allItemSO.GetToolSO((ToolNames)itemName).iconSprite;
                    }catch(Exception e)
                    {
                        Debug.Log("Tool not found in SO" +((ToolNames)itemName).ToString());
                    }
                    
                    break;
                case ItemType.Teas:// cannot be carried in inv
                                   // sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
                case ItemType.Soups: // cannot be carried in inv
                                     // sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
                case ItemType.Gems:
                    sprite = ItemService.Instance.allItemSO.GetGemSO((GemNames)itemName).iconSprite;
                    break;
                case ItemType.Alcohol: // cannot be carried in inv
                                       // sprite = ItemService.Instance.GetPotionSO((PotionName)itemName).iconSprite;
                    break;
                case ItemType.Meals:  // cannot be carried in inv
                    sprite = ItemService.Instance.allItemSO.GetMealSO((MealNames)itemName).iconSprite;
                    break;
                case ItemType.SagaicGewgaws:
                    sprite = ItemService.Instance.allItemSO.GetSagaicGewgawSO((SagaicGewgawNames)itemName).iconSprite;
                    break;
                case ItemType.PoeticGewgaws:
                    sprite = ItemService.Instance.allItemSO.GetPoeticGewgawSO((PoeticGewgawNames)itemName).iconSprite;
                    break;
                case ItemType.LoreBooks:
                    sprite = ItemService.Instance.allItemSO.GetLoreBookSO((LoreBookNames)itemName).iconSprite;
                    break;

                default:
                    break;

            }
            return sprite;
        }

        public string GetItemName(ItemData itemData)
        {
            switch (itemData.itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Potions:
                    PotionSO potionSO = ItemService.Instance.allItemSO.GetPotionSO((PotionNames)itemData.itemName);
                    return potionSO.desc;

                case ItemType.GenGewgaws:
                    GenGewgawSO genGewgawSO = ItemService.Instance.allItemSO.GetGenGewgawSO((GenGewgawNames)itemData.itemName);
                    return genGewgawSO.desc;

                case ItemType.Herbs:
                    HerbSO herbSO = ItemService.Instance.allItemSO.GetHerbSO((HerbNames)itemData.itemName);
                    return herbSO.desc;
                case ItemType.Foods:
                    FoodSO foodSO = ItemService.Instance.allItemSO.GetFoodSO((FoodNames)itemData.itemName);
                    return foodSO.desc;
                case ItemType.Fruits:
                    FruitSO fruitSO = ItemService.Instance.allItemSO.GetFruitSO((FruitNames)itemData.itemName);
                    return fruitSO.desc;
                case ItemType.Ingredients:
                    IngredSO ingredSO = ItemService.Instance.allItemSO.GetIngredSO((IngredNames)itemData.itemName);
                    return ingredSO.desc;
                case ItemType.XXX:
                    break;
                case ItemType.Scrolls:
                    ScrollSO scrollSO = ItemService.Instance.allItemSO.GetScrollSO((ScrollNames)itemData.itemName);
                    return scrollSO.desc;
                case ItemType.TradeGoods:  // start from here
                    TGSO tgSO = ItemService.Instance.allItemSO.GetTradeGoodsSO((TGNames)itemData.itemName);
                    return tgSO.desc;
                case ItemType.Tools:
                    ToolsSO toolSO = ItemService.Instance.allItemSO.GetToolSO((ToolNames)itemData.itemName);
                    return toolSO.desc;
                case ItemType.Teas:
                    break;
                case ItemType.Soups:
                    break;
                case ItemType.Gems:
                    GemSO gemSO = ItemService.Instance.allItemSO.GetGemSO((GemNames)itemData.itemName);
                    return gemSO.desc;
                case ItemType.Alcohol:
                    AlcoholSO alcoholSO = ItemService.Instance.allItemSO.GetAlcoholSO((AlcoholNames)itemData.itemName);
                    return alcoholSO.desc;                    
                case ItemType.Meals:
                    break;
                case ItemType.SagaicGewgaws:
                    SagaicGewgawSO sagaicGewgawSO = ItemService.Instance.allItemSO.GetSagaicGewgawSO((SagaicGewgawNames)itemData.itemName);
                    return sagaicGewgawSO.desc;
                case ItemType.PoeticGewgaws:
                    PoeticGewgawSO poeticGewgawSO = ItemService.Instance.allItemSO.GetPoeticGewgawSO((PoeticGewgawNames)itemData.itemName);
                    return poeticGewgawSO.desc;
                case ItemType.RelicGewgaws:
                    // write Relic 
                    break;

                default:
                    break;
            }
            return ""; 
        }
    }



}

