using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class GoldenrodDye : TGBase, Iitems
    {
        public override TGNames tgName => TGNames.GoldenrodDye;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.GoldenrodDye;    
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