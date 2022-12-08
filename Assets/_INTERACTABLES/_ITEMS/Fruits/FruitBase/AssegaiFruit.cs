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
        public void OnHoverItem()
        {

        }
        public override void ApplyBuffFX()
        {
            float chance = 40f;
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuffOnRange(CauseType.Fruit, (int)fruitName,
                 charController.charModel.charID, StatsName.damage, 0,1, fruitSO.timeFrame, fruitSO.castTime, true);
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

