using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Interactables
{
    public class RecipeItemPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        [SerializeField] TextMeshProUGUI itemName;
        private void Start()
        {
            itemName = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>(); 
        }

        public void Init(Iitems item, IngredData ingredData = null)
        {
        
                switch (item.itemType)
                {
                    case ItemType.None:
                        break;
                    case ItemType.Potions:
                        PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)item.itemName);
                        transform.GetComponent<Image>().sprite = potionSO.iconSprite;
                        itemName.text = potionSO.potionName.ToString().CreateSpace();
                        break;
                    case ItemType.GenGewgaws:
                        GenGewgawSO genSO = ItemService.Instance.GetGenGewgawSO((GenGewgawNames)item.itemName);
                        transform.GetComponent<Image>().sprite = genSO.iconSprite;
                        itemName.text = genSO.genGewgawName.ToString().CreateSpace();
                        break;
                    case ItemType.Herbs:
                        HerbSO herbSO = ItemService.Instance.GetHerbSO((HerbNames)item.itemName);
                        transform.GetComponent<Image>().sprite = herbSO.iconSprite;
                        itemName.text = herbSO.herbName.ToString().CreateSpace();
                        break;
                    case ItemType.Foods:
                        FoodSO foodSO = ItemService.Instance.GetFoodSO((FoodNames)item.itemName);
                        transform.GetComponent<Image>().sprite = foodSO.iconSprite;
                        itemName.text = foodSO.foodName.ToString().CreateSpace();
                        break;
                    case ItemType.Fruits:
                        FruitSO fruitSO = ItemService.Instance.GetFruitSO((FruitNames)item.itemName);
                        transform.GetComponent<Image>().sprite = fruitSO.iconSprite;
                        itemName.text = fruitSO.fruitName.ToString().CreateSpace();
                        break;
                    case ItemType.Ingredients:

                        IngredSO ingredSO = ItemService.Instance.GetIngredSO((IngredNames)item.itemName);
                        transform.GetComponent<Image>().sprite = ingredSO.iconSprite;
                        itemName.text = ingredSO.ingredName.ToString().CreateSpace();
                        break;
                    case ItemType.XXX:
                        break;
                    case ItemType.Scrolls:
                        break;
                    case ItemType.TradeGoods:
                        TGSO tgSO = ItemService.Instance.GetTradeGoodsSO((TGNames)item.itemName);
                        transform.GetComponent<Image>().sprite = tgSO.iconSprite;
                        itemName.text = tgSO.tgName.ToString().CreateSpace();
                        break;
                    case ItemType.Tools:
                        ToolsSO toolSO = ItemService.Instance.GetToolSO((ToolNames)item.itemName);
                        transform.GetComponent<Image>().sprite = toolSO.iconSprite;
                        itemName.text = toolSO.toolName.ToString().CreateSpace();
                        break;
                    case ItemType.Teas:
                        break;
                    case ItemType.Soups:
                        break;
                    case ItemType.Gems:
                        GemSO gemSO = ItemService.Instance.GetGemSO((GemNames)item.itemName);
                        transform.GetComponent<Image>().sprite = gemSO.iconSprite;
                        itemName.text = gemSO.gemName.ToString().CreateSpace();
                        break;
                    case ItemType.Alcohol:
                        AlcoholSO alcoholSO = ItemService.Instance.GetAlcoholSO((AlcoholNames)item.itemName);
                        transform.GetComponent<Image>().sprite = alcoholSO.iconSprite;
                        itemName.text = alcoholSO.alcoholName.ToString().CreateSpace();
                        break;
                    case ItemType.Meals:
                        MealsSO mealSO = ItemService.Instance.GetMealSO((MealNames)item.itemName);
                        transform.GetComponent<Image>().sprite = mealSO.iconSprite;
                        itemName.text = mealSO.mealName.ToString().CreateSpace();
                        break;
                    case ItemType.SagaicGewgaws:
                        break;
                    case ItemType.PoeticGewgaws:
                        break;
                    case ItemType.RelicGewgaws:
                        break;
                    case ItemType.Pouches:
                        break;
                    default:
                        break;
                }

            
            // change image 
            // get 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetChild(0).gameObject.SetActive(true);  
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}