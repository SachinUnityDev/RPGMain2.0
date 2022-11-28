using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CatechuDye : TGBase, Iitems
    {
        public override TGNames tgName => TGNames.CatechuDye;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.CatechuDye;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        }
        public override void ApplyFXOnSlot()
        {

        }
    }
}