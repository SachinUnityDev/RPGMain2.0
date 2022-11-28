using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class FelineHeart : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.FelineHeart;

        public ItemType itemType => ItemType.Ingredients;

        public int itemName => (int)IngredNames.FelineHeart;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }

        public void OnHoverItem()
        {

        }
    }
}
