using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class Fermentor : ToolBase, Iitems
    {
        public override ToolNames toolName => ToolNames.Fermentor;
        public ItemType itemType => ItemType.Tools;
        public int itemName => (int)ToolNames.Fermentor;
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
    }
}
