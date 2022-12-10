using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class LionPelt : TGBase, Iitems, ITrophyable 
    {
        public override TGNames tgName => TGNames.LionPelt;
        public  TavernSlotType tgSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.LionPelt;
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