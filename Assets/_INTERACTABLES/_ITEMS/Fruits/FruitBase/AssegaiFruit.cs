using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class AssegaiFruit : FruitBase, Iitems
    {
        public override FruitNames fruitName => FruitNames.AssegaiFruit;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.AssegaiFruit;
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

