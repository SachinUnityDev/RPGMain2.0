using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class DriedGrape : FruitBase, Iitems, IConsumable
    {
        public override FruitNames fruitName => FruitNames.DriedGrape;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.DriedGrape;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
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
            float chance1 = 30f;
            if (chance1.GetChance())
            {
                charController.tempTraitController.RemoveTraitByName(TempTraitName.Constipation);                 
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