using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Cardamom : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.Cardamom;

        public ItemType itemType => ItemType.Ingredients;

        public int itemName => (int)IngredNames.Cardamom;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }

        public void OnHoverItem()
        {

        }
    }
}

