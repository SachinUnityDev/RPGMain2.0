using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DragonflyWings : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.DragonflyWings;

        public ItemType itemType => ItemType.Ingredients;

        public int itemName => (int)IngredNames.DragonflyWings;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }

        public void OnHoverItem()
        {

        }
    }
}

