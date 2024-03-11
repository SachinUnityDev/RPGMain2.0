using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DriedMeat : Foodbase, Iitems, IConsumable
    {
        public override FoodNames foodName => FoodNames.DriedMeat;
        public ItemType itemType => ItemType.Foods;
        public int itemName => (int)FoodNames.DriedMeat;
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
            //float chance = 25f;
            //if (chance.GetChance())
            //{
            //    charController.buffController.ApplyBuff(CauseType.Food, (int)foodName,
            //     charController.charModel.charID, AttribName.focus, 1, foodSO.timeFrame
            //     , foodSO.castTime, true);
            //}
            //float chance1 = 40f;
            //if (chance1.GetChance())
            //{
            //    charController.charStateController.ApplyCharStateBuff(CauseType.Food, (int)foodName
            //        , charController.charModel.charID, CharStateName.Poisoned, foodSO.timeFrame, 3);
            //}
        }

        public void ApplyConsumableFX()
        {
            ApplyInitNFX();
            ApplyFX2();
        }
    }
}