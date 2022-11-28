using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Kiwano : FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.Kiwano;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Kiwano;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {
        }
        public override void ApplyBuffFX()
        {
        }

    }
}

