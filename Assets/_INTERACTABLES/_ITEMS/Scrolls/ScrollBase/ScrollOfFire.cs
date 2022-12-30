using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class ScrollOfFire : EnchantScrollBase, Iitems
    {
        public override ScrollNames scrollName => ScrollNames.ScrollOfFire;
        public ItemType itemType => ItemType.Scrolls;
        public int itemName => (int)ScrollNames.ScrollOfFire;
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
        public void ApplyScrollReadFX()
        {
            scrollSO = ItemService.Instance.GetScrollSO(scrollName);
            charController = ItemService.Instance.selectChar;        
            ItemService.Instance.OnScrollRead(scrollName);

            int expGained = UnityEngine.Random.Range(scrollSO.rechargeExpMin, scrollSO.rechargeExpMax + 1);
            charController.ExpPtsGain(expGained);
        }
    }
}