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
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
    
    }
}