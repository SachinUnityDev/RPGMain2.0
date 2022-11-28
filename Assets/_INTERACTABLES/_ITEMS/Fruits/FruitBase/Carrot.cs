using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Carrot : FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.Carrot;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Carrot;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
        }
        public override void ApplyBuffFX()
        {
        }
    }
}

