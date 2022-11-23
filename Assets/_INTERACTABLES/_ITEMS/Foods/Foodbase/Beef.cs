using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Beef : Foodbase, Iitems
    {
        public override FoodNames foodName => FoodNames.Beef; 
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Beef;
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