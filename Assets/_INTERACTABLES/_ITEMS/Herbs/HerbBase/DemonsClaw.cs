using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DemonsClaw : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public override HerbNames herbName => HerbNames.DemonsClaw; 
        public ItemType itemType => ItemType.Herbs;
        public int itemName => (int) HerbNames.DemonsClaw;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
            
        }
    }
}

