using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class SeasonedVenison :MealBase, IRecipe , Iitems
    {
        public override MealNames mealName => MealNames.SeasonedVenison;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Meals;
        public int itemName => (int)MealNames.SeasonedVenison;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; } = new List<IngredData>(); 
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.CookingPot);

            ItemData ingred1 = new ItemData(ItemType.Foods, (int)FoodNames.Venison);
            allIngredData.Add(new IngredData(ingred1, 3));

            ItemData ingred2 = new ItemData(ItemType.Herbs, (int)HerbNames.Thyme);
            allIngredData.Add(new IngredData(ingred2, 3));

            ItemData ingred3 = new ItemData(ItemType.Ingredients, (int)IngredNames.Ginger);
            allIngredData.Add(new IngredData(ingred3, 3));

            ItemData ingred4 = new ItemData(ItemType.Ingredients, (int)IngredNames.RedPepper);
            allIngredData.Add(new IngredData(ingred4, 3));

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