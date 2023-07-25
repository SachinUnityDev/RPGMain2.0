using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class AssegaiFruit : FruitBase, Iitems, IConsumable
    {
        public override FruitNames fruitName => FruitNames.AssegaiFruit;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.AssegaiFruit;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
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
        public override void ApplyBuffFX()
        {
            float chance = 40f;
            if (chance.GetChance())
            {
                int buffID = 
                charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName,
                 charController.charModel.charID, AttribName.dmgMax, 1, fruitSO.timeFrame, fruitSO.castTime, true);
                allBuffs.Add(buffID);

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

