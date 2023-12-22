using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Poultry : Foodbase, Iitems, IConsumable
    {
        public override FoodNames foodName => FoodNames.Poultry;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.Poultry;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
        public Currency currency { get; set; }
        public int castTime { get; set; }

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
            float chance = 20f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
                 charController.charModel.charID, AttribName.luck, 1, foodSO.timeFrame
                 , foodSO.castTime, true);
            }
            float chance1 = 30f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.Food, (int)foodName
                    , charController.charModel.charID, CharStateName.Poisoned, TimeFrame.EndOfRound, 2 );
            }
        }

        public void ApplyConsumableFX()
        {
            ApplyInitNFX();
            ApplyFX2();
        }
    }
}

