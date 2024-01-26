using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class LandsOfShargadB : LoreBookBase, Iitems
    {
        public override LoreNames loreName => LoreNames.LandsOfShargad;

        public CharController charController;
        public ItemType itemType => ItemType.LoreBooks;
        public int itemName => (int)LoreNames.LandsOfShargad;
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