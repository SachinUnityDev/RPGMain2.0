using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Apple : FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.Apple;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Apple;
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
