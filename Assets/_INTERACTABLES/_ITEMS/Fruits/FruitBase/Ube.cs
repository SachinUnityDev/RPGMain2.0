using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Ube : FruitBase ,Iitems
    {
        public override FruitNames fruitName => FruitNames.Ube;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Ube;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
        }
        public override void ApplyBuffFX()
        {
            float chance1 = 30f;
            if (chance1.GetChance())
            {
                charController.charStateController.ApplyDOTImmunityBuff(CauseType.Food
                    , (int)fruitName, charController.charModel.charID, CharStateName.BurnLowDOT, TimeFrame.Infinity, -1, false);

            }
        }
    }
}

