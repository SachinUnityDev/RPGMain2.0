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
        public void OnHoverItem()
        {

        }
        public override void ApplyFXOnSlot()
        {

        }
    }
}

