using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class NyalaTrophy : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.NyalaTrophy;
        public TavernSlotType tgSlotType => TavernSlotType.Trophy;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.NyalaTrophy;
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