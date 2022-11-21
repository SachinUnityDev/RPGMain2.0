using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class PoisonIvy : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public override HerbNames herbName => HerbNames.PoisonIvy;

        public ItemType itemType => ItemType.Herbs;

        public int itemName => (int)HerbNames.PoisonIvy;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public void OnHoverItem()
        {
            
        }
    }
}

