using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Kiwano : FruitBase, Iitems, IConsumable, IOverConsume
    {
        public override FruitNames fruitName => FruitNames.Kiwano;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Kiwano;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public float OcWt { get; set; }
        public TempTraitName tempTraitName { get; set; }
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
            float chance1 = 45f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyDOTImmunityBuff(CauseType.Food
                    , (int)fruitName, charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity, -1, false);

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
            //charController.tempTraitController.ApplyTempTraitBuff(CauseType.Fruit, (int)itemName, charID
            //                   , tempTraitName, TimeFrame.Infinity, 1, true);
            charController.tempTraitController.ApplyTempTrait(CauseType.Fruit, (int)itemName, charID
                               , tempTraitName); 

        }
        public void ApplyConsumableFX()
        {
            ApplyInitNHPStaminaRegenFX();
            ApplyHungerThirstRegenFX();
            CheckNApplyOC();
        }
    }
}

