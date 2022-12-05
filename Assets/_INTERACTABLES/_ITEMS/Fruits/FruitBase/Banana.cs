using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Banana : FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.Banana;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Banana;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
        }
        public override void ApplyBuffFX()
        {
        }
    }
}