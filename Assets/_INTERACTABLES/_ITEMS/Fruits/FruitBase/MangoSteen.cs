using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    public class MangoSteen : FruitBase, Iitems, IConsumable
    {
        public override FruitNames fruitName => FruitNames.Mangosteen;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Mangosteen;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
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
        public override void ApplyBuffFX()
        {
            float chance = 30f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName,
                 charController.charModel.charID, StatsName.haste, 2, fruitSO.timeFrame, fruitSO.castTime, true);
            }
            float chance1 = 20f;
            if (chance1.GetChance())
            {
                charController.tempTraitController.RemoveTraitByName(TempTraitName.Diarrhea);
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
