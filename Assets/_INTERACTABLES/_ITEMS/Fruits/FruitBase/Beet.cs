using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class Beet : FruitBase, Iitems, IConsumable
    {
        public override FruitNames fruitName => FruitNames.Beet;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Beet;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
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
            {// get cast time from SO 
                charController.charStateController.ApplyImmunityBuff(CauseType.Food
                    , (int)fruitName, charController.charModel.charID, CharStateName.Bleeding, TimeFrame.EndOfRound, 8);
            }
        }     

        public void ApplyConsumableFX()
        {
            ApplyInitNHPStaminaRegenFX();
            ApplyHungerThirstRegenFX();          
        }
    }
}
