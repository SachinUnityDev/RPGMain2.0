using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Interactables
{
    public class RecipeService : MonoSingletonGeneric<RecipeService>
    {

        /// <summary>
        /// Meet NPC .. buy from them 
        /// when u know the recipe you can make them at specific places 
        /// if you have the item in the inv 
        /// </summary>

       
        public RecipeModel recipeModel; 
        public List<IRecipe> knownRecipes = new List<IRecipe>();  

        void Start()
        {

        }

        public IRecipe CreateRecipe(ItemData pdtName)
        {


            return null; 
        }


        public bool AddRecipe2Known(ItemData itemData)
        {
            if(!recipeModel.allRecipeKnown.Any(t=>t.itemType==itemData.itemType && t.ItemName == itemData.ItemName))
            {
                recipeModel.allRecipeKnown.Add(itemData);
                return true;
            }
            else
            {
                return false; 
            }
            
        }
        public bool RemoveRecipeFrmKnown(ItemData itemData)
        {
            if (recipeModel.allRecipeKnown.Any(t => t.itemType == itemData.itemType && t.ItemName == itemData.ItemName))
            {
                recipeModel.allRecipeKnown.Remove(itemData);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}