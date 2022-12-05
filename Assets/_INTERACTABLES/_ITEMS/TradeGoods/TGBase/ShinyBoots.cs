using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ShinyBoots : TGBase, Iitems
    {
        public override TGNames tgName => TGNames.ShinyBoots;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.ShinyBoots;
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