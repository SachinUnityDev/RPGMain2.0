using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{



    public class EmeraldRing : GenGewgawBase, IRecipe, Iitems
    {
        public override GenGewgawNames genGewgawNames => GenGewgawNames.EmeraldRing;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.GenGewgaws;
        public int itemName => (int)GenGewgawNames.EmeraldRing; 
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; }

        public void RecipeInit()
        {
            toolData = null;

            ItemData ingred1 = new ItemData(ItemType.Gems, (int)GemNames.Emerald);
            allIngredData.Add(new IngredData(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.TradeGoods, (int)TGNames.SimpleRing);
            allIngredData.Add(new IngredData(ingred2, 1));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {

        }
        public void OnHoverItem()
        {

        }
    }
}