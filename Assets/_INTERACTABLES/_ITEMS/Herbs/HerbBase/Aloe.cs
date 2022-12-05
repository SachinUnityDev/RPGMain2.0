using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;


namespace Interactables
{
    public class Aloe : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public override HerbNames herbName => HerbNames.Aloe;
        public ItemType itemType => ItemType.Herbs; 
        public int itemName => (int)HerbNames.Aloe;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }

        public void OnHoverItem()
        {
            // find view controller and name and type 
        }
    }
}

