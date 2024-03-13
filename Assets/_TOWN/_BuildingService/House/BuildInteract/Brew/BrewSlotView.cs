using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;
namespace Town
{


    public class BrewSlotView : MonoBehaviour
    {
        [Header("to be ref")]
        [SerializeField] Transform recipeSlotTrans;
        public Transform brewSlotContainer;
        public Transform readySlotContainer;
        [Header("Buttons Brew TBR")]
        [SerializeField] BrewBtnPtrEvents brewBtn;

        [Header("Ready and Drink TBR")]
        [SerializeField] BrewReadySlotPtrEvents brewReadySlotPtrEvents; 
        [SerializeField] DrinkBtnPtrEvents drinkBtnPtrEvents;

        public AlcoholNames alcoholName;
        [SerializeField] Iitems item;
        [SerializeField] IRecipe recipe;
        [SerializeField] AlcoholSO alcoholSO;
        [SerializeField] BrewView brewView;

        [SerializeField] ItemDataWithQty ingredData;

        public bool isIngredAvail = false;

   

        public void InitBrewSlot(AlcoholNames alcoholName, BrewView brewView)
        {
            this.alcoholName = alcoholName;
            alcoholSO = ItemService.Instance.allItemSO.GetAlcoholSO(alcoholName);
            ItemData itemData = new ItemData(ItemType.Alcohol, (int)alcoholName);
            item = ItemService.Instance.GetItemBase(itemData);
            this.brewView = brewView;
            FillRecipeSlots();
            brewSlotContainer.GetComponent<BrewWIPContainerView>().InitBrewWIP(this); // container view 
            // btns 
            brewBtn.Init(this);
            drinkBtnPtrEvents.Init(this, brewReadySlotPtrEvents); 


        }

        void FillRecipeSlots()
        {
            recipe = item as IRecipe;

            int j = 0;
            for (int i = 0; i < recipeSlotTrans.childCount; i = i + 2)
            {
                if (j >= recipe.allIngredData.Count)
                {
                    recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>().DisableSlot();
                    break;
                }
                else
                {
                    recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>().EnableSlot();
                }
                ItemDataWithQty ingred = recipe.allIngredData[j];

                recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>().InitBrewRecipe(ingred);

                j++;
            }
        }

        public bool AreIngredSufficient()
        {
            // loop thru ingred and return true only when all ingred are there 
            isIngredAvail = false;
            for (int i = 0; i < recipeSlotTrans.childCount; i = i + 2)
            {
                if (recipeSlotTrans.GetChild(i).gameObject.activeInHierarchy)
                {
                    BrewRecipePtrEvents brewRecipePtrEvents = recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>();
                    isIngredAvail = brewRecipePtrEvents.ChkIngredQtyNupdate();
                    if (!isIngredAvail) break; 
                }
            }
            return isIngredAvail;
        }

        public bool OnBrewBtnPressed()
        {
            for (int i = 0; i < recipeSlotTrans.childCount; i = i + 2)
            {
                if (recipeSlotTrans.GetChild(i).gameObject.activeInHierarchy)
                {
                    BrewRecipePtrEvents brewRecipePtrEvents = recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>();
                    brewRecipePtrEvents.UpdateIngredSlotStatus();
                }
            }
            bool slotFound =  brewSlotContainer.GetComponent<BrewWIPContainerView>().AlotBrewSlot();
            return slotFound;
        }

        public void OnDrinkBtnPressed()
        {
          brewReadySlotPtrEvents.OnConsume(item);
        }
    }
}