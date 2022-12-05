using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class GreenVelvet : TGBase, Iitems
    {
        public override TGNames tgName => TGNames.GreenVelvet;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.GreenVelvet;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void ApplyFXOnSlot()
        {

        }
    }
}