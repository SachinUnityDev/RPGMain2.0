using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Bread : Foodbase, Iitems
    {
        public override FoodNames foodName => FoodNames.Bread;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Bread;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void ApplyFX2()
        {

        }
    }
}