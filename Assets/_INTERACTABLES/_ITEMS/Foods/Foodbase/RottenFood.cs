using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class RottenFood : Foodbase, Iitems
    {
        public override FoodNames foodName => FoodNames.RottenFood;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.RottenFood;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }    
        public override void ApplyFX2()
        {
            float chance = 30f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
                 charController.charModel.charID, StatsName.morale, -1, foodSO.timeFrame
                 , foodSO.castTime, false);
            }
            float chance1 = 80f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.Food, (int)foodName
                    , charController.charModel.charID, CharStateName.PoisonedHighDOT, TimeFrame.Infinity, -1, false);
            }
        }
    }
}
