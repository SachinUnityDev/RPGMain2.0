using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class FlaskOfWater : Foodbase, Iitems
    {
        public override FoodNames foodName => FoodNames.FlaskOfWater;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.FlaskOfWater;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        }
   
        public override void ApplyFX2()
        {

        }
    }
}