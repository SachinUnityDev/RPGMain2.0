using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Apple : FruitBase, Iitems, IConsumable
    {
        public override FruitNames fruitName => FruitNames.Apple;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Apple;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
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
        public override void ApplyBuffFX()
        {
            float chance1 = 30f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyDOTImmunityBuff(CauseType.Food
                    , (int)fruitName, charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity, -1, false);
            }
        }

        public void ApplyConsumableFX()
        {
           ApplyInitNHPStaminaRegenFX();
           ApplyHungerThirstRegenFX();
           ApplyBuffFX();
        }
    }
}
