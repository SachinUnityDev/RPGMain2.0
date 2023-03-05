using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class CookedHam : MealBase, IRecipe, Iitems
    {
        public override MealNames mealName => MealNames.CookedHam;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Meals;
        public int itemName => (int)MealNames.CookedHam;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; } = new List<ItemDataWithQty>();
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.CookingPot);
            ItemData ingred1 = new ItemData(ItemType.Foods, (int)FoodNames.Ham);
            ItemDataWithQty ingredData = new ItemDataWithQty(ingred1, 1);
            allIngredData.Add(ingredData);
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            RecipeInit();
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }

        public void OnHoverItem()
        {

        }
    }
}