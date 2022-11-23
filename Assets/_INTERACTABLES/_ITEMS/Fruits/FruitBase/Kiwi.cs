using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Kiwi: FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.Kiwi;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Kiwi;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public void OnHoverItem()
        {
        }
        public override void ApplyBuffFX()
        {
        }
    }
}