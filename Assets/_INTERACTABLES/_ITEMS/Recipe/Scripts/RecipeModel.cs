using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Interactables
{

    [Serializable]
    public class RecipeModel 
    {
        public List<ItemData> allRecipeKnown = new List<ItemData>();

        // default known 
        public List<ItemType> GetItemTypesForRecipe(RecipeType recipeType)
        {
            List<ItemType> allItemtype = new List<ItemType>();
            switch (recipeType)
            {
                case RecipeType.None:
                    break;
                case RecipeType.Cooking:
                    allItemtype.Add(ItemType.Meals); 
                    allItemtype.Add(ItemType.Soups);
                    allItemtype.Add(ItemType.Teas);
                    break;
                case RecipeType.Crafting:
                    allItemtype.Add(ItemType.Potions);
                    break;
                case RecipeType.Merging:
                    allItemtype.Add(ItemType.GenGewgaws);
                    break;
                case RecipeType.Brewing:
                    allItemtype.Add(ItemType.Alcohol);
                    break;
                default:
                    break;
            }
            return allItemtype; 
        }

        public List<ItemData> GetRecipeTypeKnown(RecipeType recipeType)
        {
            List<ItemData> allKnown4RecipeType = new List<ItemData>();

            foreach (ItemType _itemtype in GetItemTypesForRecipe(recipeType))
            {
                allKnown4RecipeType.AddRange
                    (allRecipeKnown.Where(t => t.itemType == _itemtype).ToList());
            }
            return allKnown4RecipeType;
        }



    }
}