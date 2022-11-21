using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Hemp : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public ItemType itemType => ItemType.Herbs;

        public int itemName => (int)HerbNames.Hemp;

        public override HerbNames herbName => HerbNames.Hemp;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public void OnHoverItem()
        {
          
        }
    }
}

