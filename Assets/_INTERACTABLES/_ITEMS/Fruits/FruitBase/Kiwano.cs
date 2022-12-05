using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Kiwano : FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.Kiwano;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Kiwano;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
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

    }
}

