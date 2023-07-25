using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Mutton : Foodbase, Iitems, IConsumable
    {
        public override FoodNames foodName => FoodNames.Mutton;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Mutton;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
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
            float chance = 40f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
                charController.charModel.charID, AttribName.dodge, 1, foodSO.timeFrame
                 , foodSO.castTime, true);
            }
            float chance1 = 50f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.Food, (int)foodName
                    , charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity, -1);
            }
        }

        public void ApplyConsumableFX()
        {
            ApplyInitNFX();
            ApplyFX2();
        }
    }
}