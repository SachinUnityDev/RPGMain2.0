using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Cucumber : FruitBase, Iitems, IConsumable
    {
        public override FruitNames fruitName => FruitNames.Cucumber;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Cucumber;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
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
        }

        public void ApplyConsumableFX()
        {
            ApplyInitNHPStaminaRegenFX();
            ApplyHungerThirstRegenFX();            
        }
    }
}