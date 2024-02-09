using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class MushroomSoup : IRecipe, Iitems
    {
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Soups;
        public int itemName => (int)SoupNames.MushroomSoup;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; }
        public Currency currency { get; set; }
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.CookingPot);

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.WhiteMushroom);
            allIngredData.Add(new ItemDataWithQty(ingred1, 5));

            ItemData ingred2 = new ItemData(ItemType.Herbs, (int)HerbNames.Thyme);
            allIngredData.Add(new ItemDataWithQty(ingred2, 3));

            ItemData ingred3 = new ItemData(ItemType.Ingredients, (int)IngredNames.Onion);
            allIngredData.Add(new ItemDataWithQty(ingred3, 1));

            ItemData ingred4 = new ItemData(ItemType.Foods, (int)FoodNames.FlaskOfWater);
            allIngredData.Add(new ItemDataWithQty(ingred4, 3));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {

        }
        public void OnHoverItem()
        {

        }
    }
}