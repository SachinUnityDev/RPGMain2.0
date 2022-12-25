using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class CookedMutton : IRecipe, Iitems
    {

        public int itemId { get; set; }
        public ItemType itemType => ItemType.Meals;
        public int itemName => (int)MealsNames.CookedMutton;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; }
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.CookingPot);

            ItemData ingred1 = new ItemData(ItemType.Foods, (int)FoodNames.Mutton);
            allIngredData.Add(new IngredData(ingred1, 1));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {

        }

        public void OnHoverItem()
        {

        }
    }
}