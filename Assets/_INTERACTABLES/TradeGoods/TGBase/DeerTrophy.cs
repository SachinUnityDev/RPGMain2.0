using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class DeerTrophy : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.DeerTrophy;
        public TavernSlotType tgSlotType => TavernSlotType.Trophy;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.DeerTrophy;
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

