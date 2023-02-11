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
    }

    void FillRecipeSlots()
    {
        IRecipe recipe = item as IRecipe;
        if (recipe != null) 
        {
            int i = 0; int j = 0; 
            foreach (IngredData ingred in recipe.allIngredData)
            {             
                int quantity =
                    InvService.Instance.invMainModel.GetItemNosInStashInv(ingred.ItemData);
                quantity +=
                    InvService.Instance.invMainModel.GetItemNosInStashInv(ingred.ItemData);                

                    recipeSlotTrans.GetChild(j).GetComponent<BrewRecipePtrEvents>().InitBrewRecipe(ingred, quantity);
                
                i++; j = j + 2;
            }
        }
    }



    void OnBrewBtnPressed()
    {
        // if there are sufficient ingred.. slot status and fill it up
        // 
        // 



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
