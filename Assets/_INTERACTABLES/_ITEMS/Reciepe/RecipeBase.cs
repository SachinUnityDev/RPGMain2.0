using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Interactables
{
    [Serializable]
    public class ItemDataWithQty
    {
        public ItemData itemData;
        public int quantity;
      
        public ItemDataWithQty() { }
        public ItemDataWithQty(ItemData itemData, int quantity)
        {
            this.itemData = itemData;
            this.quantity = quantity;
        }
    }
    [Serializable]
    public class ItemDataWithQtyNPrice
    {
        public ItemData itemData;
        public int quantity;
        public Currency currPrice;
        
        public ItemDataWithQtyNPrice() { }
        public ItemDataWithQtyNPrice(ItemData itemData, int quantity)
        {
            this.itemData = itemData;
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

