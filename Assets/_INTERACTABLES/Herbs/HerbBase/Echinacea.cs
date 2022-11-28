using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Echinacea : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public ItemType itemType => ItemType.Herbs;
        public int itemName => (int)HerbNames.Echinacea;
        public override HerbNames herbName => HerbNames.Echinacea;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {
           
        }
    }
}

