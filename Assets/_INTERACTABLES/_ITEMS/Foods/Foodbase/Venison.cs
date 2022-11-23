using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Venison : Foodbase, Iitems
    {
        public override FoodNames foodName => FoodNames.Venison;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Venison;
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

