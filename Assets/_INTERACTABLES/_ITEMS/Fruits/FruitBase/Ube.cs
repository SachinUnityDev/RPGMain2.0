using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Ube : FruitBase ,Iitems, IConsumable, IOverConsume
    {
        public override FruitNames fruitName => FruitNames.Ube;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Ube;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
        public TempTraitName tempTraitName { get; set; }
        public float OcWt { get; set; }
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
            float chance1 = 25f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyImmunityBuff(CauseType.Food
                    , (int)fruitName, charController.charModel.charID, CharStateName.Burning
                    , TimeFrame.EndOfRound, 4);

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
        }
    }
}

