using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DeerSkin : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.DeerSkin;
        public TavernSlotType tgSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.DeerSkin;
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