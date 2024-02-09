using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class TambotiFruit : HerbBase, IConsumableByAbzazulu, IIngredient, IhealingHerb, Iitems
    {
        public override HerbNames herbName => HerbNames.TambotiFruit;
        public ItemType itemType => ItemType.Herbs; 
        public int itemName => (int)HerbNames.TambotiFruit;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
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



    }
}


