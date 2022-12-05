using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Spice: TGBase , Iitems
    {
        public override TGNames tgName => TGNames.Spice;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.Spice;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void ApplyFXOnSlot()
        {

        }
    }
}

