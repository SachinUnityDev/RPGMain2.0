using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;

public class BrewSlotView : MonoBehaviour
{
    [Header("to be ref")]
    [SerializeField] Transform recipeSlotTrans;    
    public Transform brewSlotContainer;
    public Transform readySlotContainer; 
    [Header("Buttons Brew and Drink")]
    [SerializeField] Button brewBtn;
    [SerializeField] Button drinkBtn;


    public AlcoholNames alcoholName;
    [SerializeField] Iitems item;
    [SerializeField] IRecipe recipe;
    [SerializeField] AlcoholSO alcoholSO;
    [SerializeField] BrewView brewView;

    [SerializeField] IngredData ingredData;

    void Awake()
    {
        brewBtn.onClick.AddListener(OnBrewBtnPressed);
        drinkBtn.onClick.AddListener(OnDrinkBtnPressed);
    }

    public void InitBrewSlot(AlcoholNames alcoholName, BrewView brewView)
    {
        this.alcoholName= alcoholName;
        alcoholSO = ItemService.Instance.GetAlcoholSO(alcoholName);
        ItemData itemData = new ItemData(ItemType.Alcohol, (int)alcoholName);
        item = ItemService.Instance.GetItemBase(itemData); 
        this.brewView = brewView;
        FillRecipeSlots();
        brewSlotContainer.GetComponent<BrewWIPContainerView>().InitBrewWIP(this); // container view 
    }

    void FillRecipeSlots()
    {
        recipe = item as IRecipe;
           
            int j = 0; 
            for (int i = 0; i < recipeSlotTrans.childCount; i= i+2)
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
                IngredData ingred = recipe.allIngredData[j];       
                int quantity =
                    InvService.Instance.invMainModel.GetItemNosInCommInv(ingred.ItemData);
                quantity +=
                    InvService.Instance.invMainModel.GetItemNosInStashInv(ingred.ItemData);

                recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>().InitBrewRecipe(ingred, quantity);                               
                
                j++;
            }
    }

    void AreIngredSufficient()
    {
        // loop thru ingred and return true only when all ingred are there 
    }

    void OnBrewBtnPressed()
    {
        brewSlotContainer.GetComponent<BrewWIPContainerView>().AllotBrewSlot();
    }

    void OnDrinkBtnPressed()
    {
        readySlotContainer.GetChild(0).GetComponent<BrewReadySlotPtrEvents>().OnConsume(item);
    }




}
