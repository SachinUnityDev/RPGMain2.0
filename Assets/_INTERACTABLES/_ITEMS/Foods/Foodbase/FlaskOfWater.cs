using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class FlaskOfWater : Foodbase, Iitems,IConsumable
    {
        public override FoodNames foodName => FoodNames.FlaskOfWater;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.FlaskOfWater;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        
        public void OnHoverItem()
        {
        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;            
        }

        public override void ApplyFX2()
        {

        }

        public void ApplyConsumableFX()
        {
            ApplyInitNFX();// some thirst relief here           
        }
    }
}