using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class MuttonStew : MealBase, IRecipe, Iitems
    {
        public override MealNames mealName => MealNames.MuttonStew;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Meals;
        public int itemName => (int)MealNames.MuttonStew;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; } = new List<ItemDataWithQty>();
        public Currency currency { get; set; }
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.CookingPot);

            ItemData ingred1 = new ItemData(ItemType.Foods, (int)FoodNames.Mutton);
            allIngredData.Add(new ItemDataWithQty(ingred1, 3));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.Onion);
            allIngredData.Add(new ItemDataWithQty(ingred2, 3));

            ItemData ingred3 = new ItemData(ItemType.Ingredients, (int)IngredNames.Garlic);
            allIngredData.Add(new ItemDataWithQty(ingred3, 4));

            ItemData ingred4 = new ItemData(ItemType.Foods, (int)FoodNames.FlaskOfWater);
            allIngredData.Add(new ItemDataWithQty(ingred4, 1));
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