using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class RoyalPurpleDye : TGBase , Iitems
    {
        public override TGNames tgName => TGNames.RoyalPurpleDye;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.RoyalPurpleDye;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
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

