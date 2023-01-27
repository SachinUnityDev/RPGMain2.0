using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{



    public class Ham : Foodbase, Iitems, IConsumable
    {
        public override FoodNames foodName => FoodNames.Ham;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Ham;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
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
            float chance = 40f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
                 charController.charModel.charID, StatsName.acc, 1, foodSO.timeFrame
                 , foodSO.castTime, true);
            }
            float chance1 = 30f;
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