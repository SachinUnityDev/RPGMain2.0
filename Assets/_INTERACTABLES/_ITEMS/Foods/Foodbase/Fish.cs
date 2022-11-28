using Common;
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
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        } 
        public override void ApplyFX2()
        {
            float chance = 25f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
                 charController.charModel.charID, StatsName.focus, 1, foodSO.timeFrame
                 , foodSO.castTime, true);
            }
            float chance1 = 40f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.Food, (int)foodName
                    , charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity, -1, false);
            }
        }
    }
}

