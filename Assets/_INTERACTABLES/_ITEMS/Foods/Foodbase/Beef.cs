using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Beef : Foodbase, Iitems, IConsumable
    {
        public override FoodNames foodName => FoodNames.Beef; 
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Beef;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
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
            float chance = 25f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
                 charController.charModel.charID, StatsName.morale, 1, foodSO.timeFrame
                 , foodSO.castTime, true);
            }
            float chance1 = 50f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.Food, (int)foodName
                    , charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity, -1, false);
            }
        }
        public void ApplyConsumableFX()
        {
            ApplyInitNFX();
            ApplyFX2(); 
        }

    
    }
}