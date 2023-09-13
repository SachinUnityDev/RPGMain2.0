using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    public class RecipePanelView : MonoBehaviour
    {
        [SerializeField] ItemData itemData;

        [SerializeField] Transform finalPdtTrans;
        [SerializeField] Transform ingred1Trans; 
        [SerializeField] Transform ingred2Trans;
        [SerializeField] Transform ingred3Trans;
        [SerializeField] Transform ingred4Trans;
        [SerializeField] Transform toolTrans;
        [SerializeField] TextMeshProUGUI slotTitleTxt; 

        [Header("Ingred Data")]
        List<ItemDataWithQty> allIngredData = new List<ItemDataWithQty>();
        [Header("Tools Data")]
        [SerializeField] ItemData toolData; 

        IRecipe iRecipe; 
        private void OnEnable()
        {
            finalPdtTrans = transform.GetChild(0).GetChild(0);
            ingred1Trans = transform.GetChild(0).GetChild(2);
            ingred2Trans = transform.GetChild(0).GetChild(4);
            ingred3Trans = transform.GetChild(0).GetChild(6);
            ingred4Trans = transform.GetChild(0).GetChild(8);
            toolTrans = transform.GetChild(0).GetChild(9);
           
        }
        public void Init(ItemData itemData)
        {
            this.itemData = itemData;
          

            GetItemRecipeNIngredData(); 
        }

        void GetItemRecipeNIngredData()
        {
            iRecipe = null;
            Iitems iitem = ItemService.Instance.GetItemBase(itemData);

            if(iitem != null)
            {
                iRecipe = iitem as IRecipe;
                if (iRecipe != null)
                {
                    allIngredData = iRecipe.allIngredData;
                    toolData = iRecipe.toolData; 
                }
                finalPdtTrans = transform.GetChild(0).GetChild(0);
                finalPdtTrans.transform.GetComponent<RecipeItemPtrEvents>().Init(iitem);
                int j = 0;
                for (int i = 0; i < allIngredData.Count; i++)
                {
                    Iitems ingredItem = ItemService.Instance.GetItemBase(allIngredData[i].itemData);
                    j = j + 2;
                    transform.GetChild(0).GetChild(j).gameObject.SetActive(true);
                    transform.GetChild(0).GetChild(j).GetComponent<RecipeItemPtrEvents>().Init(ingredItem, allIngredData[i]);
                }
                for (int k = j+1; k <= 8; k++)
                {
                    transform.GetChild(0).GetChild(k).gameObject.SetActive(false);                    
                }
                if (toolData != null)
                {
                    Iitems toolItem = ItemService.Instance.GetItemBase(toolData);
                    transform.GetChild(0).GetChild(9).gameObject.SetActive(true);
                    transform.GetChild(0).GetChild(9).GetComponent<RecipeItemPtrEvents>().Init(toolItem);
                }
                else
                {
                    transform.GetChild(0).GetChild(9).gameObject.SetActive(false);
                }                    
            }
            slotTitleTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            switch (itemData.itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Potions:
                    PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)itemData.itemName);                   
                    finalPdtTrans.GetComponent<Image>().sprite = potionSO.iconSprite;
                    slotTitleTxt.text = potionSO.potionName.ToString().CreateSpace();
                    break;
                case ItemType.GenGewgaws:
                    GenGewgawSO genSO = ItemService.Instance.GetGenGewgawSO((GenGewgawNames)itemData.itemName);
                    finalPdtTrans.GetComponent<Image>().sprite = genSO.iconSprite;
                    slotTitleTxt.text = genSO.genGewgawName.ToString().CreateSpace();
                    break;
                case ItemType.Herbs:
                    break;
                case ItemType.Foods:
                    break;
                case ItemType.Fruits:
                    break;
                case ItemType.Ingredients:
                    break;
                case ItemType.XXX:
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
                    AlcoholSO alcoholSO = ItemService.Instance.GetAlcoholSO((AlcoholNames)itemData.itemName);
                    finalPdtTrans.GetComponent<Image>().sprite = alcoholSO.iconSprite;
                    slotTitleTxt.text = alcoholSO.alcoholName.ToString().CreateSpace();
                    break;
                case ItemType.Meals:
                    MealsSO mealSO = ItemService.Instance.GetMealSO((MealNames)itemData.itemName);
                    finalPdtTrans.GetComponent<Image>().sprite = mealSO.iconSprite;
                    slotTitleTxt.text = mealSO.mealName.ToString().CreateSpace();
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
        }  
    }
}