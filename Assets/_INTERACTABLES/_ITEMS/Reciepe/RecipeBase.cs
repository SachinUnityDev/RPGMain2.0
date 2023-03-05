using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Interactables
{
    [System.Serializable]
    public class ItemDataWithQty
    {
        public ItemData ItemData;
        public int quantity;

        public ItemDataWithQty(ItemData itemData, int quantity)
        {
            ItemData = itemData;
            this.quantity = quantity;
        }
    }

    public interface IRecipe
    {  
         ItemData toolData { get; set; }
         List<ItemDataWithQty> allIngredData { get; set; }
         //RecipeType type { get; set; }   
         void RecipeInit(); 
        
    }
}

