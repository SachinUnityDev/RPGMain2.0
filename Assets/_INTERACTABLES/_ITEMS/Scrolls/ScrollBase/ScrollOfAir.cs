using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ScrollOfAir : EnchantScrollBase ,Iitems
    {
        public override ScrollNames scrollName => ScrollNames.ScrollOfAir;

        public ItemType itemType => ItemType.Scrolls;
        public int itemName => (int)ScrollNames.ScrollOfAir;
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
        public void ApplyScrollReadFX()
        {
            scrollSO = ItemService.Instance.GetScrollSO(scrollName);
            charController = InvService.Instance.charSelectController;           
            ItemService.Instance.OnScrollRead(scrollName);

            int expGained = UnityEngine.Random.Range(scrollSO.rechargeExpMin, scrollSO.rechargeExpMax+1);
            charController.ExpPtsGain(expGained); 
        }
    }
}