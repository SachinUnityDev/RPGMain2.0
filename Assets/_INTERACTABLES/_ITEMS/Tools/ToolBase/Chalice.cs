using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Chalice : ToolBase, Iitems
    {
        public override ToolNames toolName => ToolNames.Chalice;
        public ItemType itemType => ItemType.Tools;
        public int itemName => (int)ToolNames.Chalice;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
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

