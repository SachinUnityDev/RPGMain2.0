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
    [SerializeField] Transform brewSlotContainer;    

    [Header("Buttons Brew and Drink")]
    [SerializeField] Button brewBtn;
    [SerializeField] Button drinkBtn;


    public AlcoholNames alcoholName;
    [SerializeField] Iitems item;
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
      //  brewSlotContainer.GetComponent<BrewWIPContainerView>().InitBrewWIP(this); // container view 
    }

    void FillRecipeSlots()
    {
        IRecipe recipe = item as IRecipe;
           
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
                    InvService.Instance.invMainModel.GetItemNosInStashInv(ingred.ItemData);
                quantity +=
                    InvService.Instance.invMainModel.GetItemNosInStashInv(ingred.ItemData);

                recipeSlotTrans.GetChild(i).GetComponent<BrewRecipePtrEvents>().InitBrewRecipe(ingred, quantity);                               
                
                j++;
            }
    }

    void AreIngredSufficient()
    {

    }

    void OnBrewBtnPressed()
    {
       // recipe 

        // if there are sufficient ingred.. slot status and fill it up
        // loop thru slots if the staus returned as true we are good 

        


    }

    void StartTheBrewProcess()
    {
        // subtract the ingred from the slots and inventories
        // 



    }
    
    void OnDrinkBtnPressed()
    {

    }




}
