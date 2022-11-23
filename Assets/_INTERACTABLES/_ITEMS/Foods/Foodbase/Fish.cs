using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Fish : Foodbase, Iitems
    {
        public override FoodNames foodName => FoodNames.Fish;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Fish;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public void OnHoverItem()
        {

        }
        public override void ApplyFX1()
        {

        }
        public override void ApplyFX2()
        {

        }
    }
}

