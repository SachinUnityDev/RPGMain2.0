using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class NyalaPelt : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.NyalaPelt;
        public TavernSlotType tgSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int) TGNames.NyalaPelt;
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
    }
}

