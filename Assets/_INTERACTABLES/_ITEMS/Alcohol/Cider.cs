using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class Cider : IRecipe, Iitems
    {
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Alcohol;
        public int itemName => (int)AlcoholNames.Cider;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; }
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.Fermentor);

            ItemData ingred1 = new ItemData(ItemType.Fruits, (int)FruitNames.Apple);
            allIngredData.Add(new IngredData(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.Yeast);
            allIngredData.Add(new IngredData(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Ingredients, (int)IngredNames.Cardamom);
            allIngredData.Add(new IngredData(ingred3, 1));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {

        }
        public void OnHoverItem()
        {

        }
    }
}