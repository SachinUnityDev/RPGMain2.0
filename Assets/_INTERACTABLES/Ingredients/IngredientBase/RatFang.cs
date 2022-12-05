using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class RatFang : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.RatFang;
        public ItemType itemType => ItemType.Ingredients;
        public int itemName => (int)IngredNames.RatFang;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }

        public void OnHoverItem()
        {

        }
    }
}

