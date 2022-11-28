using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Buchu : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public ItemType itemType => ItemType.Herbs;
        public int itemName => (int)HerbNames.Buchu;
        public override HerbNames herbName => HerbNames.Buchu;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
           
        }
    }
}


