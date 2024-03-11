using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Grape : FruitBase, Iitems, IConsumable, IOverConsume
    {
        public override FruitNames fruitName => FruitNames.Grape;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Grape;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        public float OcWt { get; set; }
        public TempTraitName tempTraitName { get; set; }
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
                int buffID =
                charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName,
                charController.charModel.charID, AttribName.willpower, 1, fruitSO.timeFrame, fruitSO.castTime, true);
                allBuffs.Add(buffID);
            }
        }
        public void CheckNApplyOC()
        {
            tempTraitName = fruitSO.tempTraitName;
            OcWt = fruitSO.weightOfSickeness;
            OCData ocData = new OCData(itemType, itemName, OcWt);
            charController.itemController.ChecknApplyOC(ocData, tempTraitName, this);
        }

        public void ApplyOC_FX()
        {
            charController.tempTraitController.ApplyTempTrait(CauseType.Fruit, (int)itemName, charID
                                , tempTraitName);
        }
        public void ApplyConsumableFX()
        {
            ApplyInitNHPStaminaRegenFX();
            ApplyHungerThirstRegenFX();
            CheckNApplyOC();
            ApplyBuffFX();
        }
    }
}
