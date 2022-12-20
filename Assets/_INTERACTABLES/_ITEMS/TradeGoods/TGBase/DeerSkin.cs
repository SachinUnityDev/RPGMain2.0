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
        public override void ApplyFXOnSlot()
        {

        }

        public void TrophyInit()
        {
        }

        public void OnTrophyWalled()
        {
           
        }

        public void OnTrophyRemoved()
        {
           
        }
    }
}