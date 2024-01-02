using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class LandsOfShargad : Iitems, iReadScroll
    {
        public LoreScroll LoreScrollName { get; }
        public CharController charController;
        public ItemType itemType => ItemType.LoreBooks;
        public int itemName => (int)1;
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
            charController = InvService.Instance.charSelectController;
            LoreBookSO loreSO = ItemService.Instance.loreScrollSO;
            int expVal = UnityEngine.Random.Range(loreSO.expGainMin, loreSO.expGainMax + 1);
            charController.ExpPtsGain(expVal);

            // Unlock a Locked Lore Scroll 
            LoreService.Instance.UnLockRandomSubLore(LoreNames.LandsOfShargad);

        }
    }
}