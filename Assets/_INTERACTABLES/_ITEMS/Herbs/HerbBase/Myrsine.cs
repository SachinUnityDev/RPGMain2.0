using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Myrsine : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems    
    {
        public ItemType itemType => ItemType.Herbs;

        public int itemName => (int)HerbNames.Myrsine;

        public override HerbNames herbName => HerbNames.Myrsine;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
           
        }
    }
}
