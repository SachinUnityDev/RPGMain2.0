using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class AmetystRing : GenGewgawBase, IRecipe, Iitems, IEquipAble
    {
        public override GenGewgawNames genGewgawNames => GenGewgawNames.AmetystRing;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.GenGewgaws;
        public int itemName => (int)GenGewgawNames.AmetystRing;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; } = new List<IngredData>();

        public void RecipeInit()
        {
            toolData = null;

            ItemData ingred1 = new ItemData(ItemType.Gems, (int)GemNames.Ametyst);
            allIngredData.Add(new IngredData(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.TradeGoods, (int)TGNames.SimpleRing);
            allIngredData.Add(new IngredData(ingred2, 1));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;

            GenGewgawInit(genGewgawQ);
          
        }
        public void OnHoverItem()
        {

        }

        public void ApplyEquipableFX()
        {
            EquipGenGewgawFX(); 
        }

        public void RemoveEquipableFX()
        {
            UnEquipGenGewgawFX();
        }
    }
}
