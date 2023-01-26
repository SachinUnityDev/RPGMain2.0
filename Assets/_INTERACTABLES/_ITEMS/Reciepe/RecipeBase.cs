using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Interactables
{
    [System.Serializable]
    public class IngredData
    {
        public ItemData ItemData;
        public int quantity;

        public IngredData(ItemData itemData, int quantity)
        {
            ItemData = itemData;
            this.quantity = quantity;
        }
    }

    public interface IRecipe
    {  
         ItemData toolData { get; set; }
         List<IngredData> allIngredData { get; set; }
         //RecipeType type { get; set; }   
         void RecipeInit(); 
        
    }
}

