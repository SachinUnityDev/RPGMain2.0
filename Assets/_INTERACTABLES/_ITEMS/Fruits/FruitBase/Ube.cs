using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Ube : FruitBase ,Iitems
    {
        public override FruitNames fruitName => FruitNames.Ube;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Ube;
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

