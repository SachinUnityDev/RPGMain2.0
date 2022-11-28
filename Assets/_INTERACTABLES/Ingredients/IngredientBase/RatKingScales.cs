using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class RatKingScales : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.RatKingScales; 

        public ItemType itemType => ItemType.Ingredients;

        public int itemName => (int)IngredNames.RatKingScales;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }

        public void OnHoverItem()
        {

        }

    }
}

