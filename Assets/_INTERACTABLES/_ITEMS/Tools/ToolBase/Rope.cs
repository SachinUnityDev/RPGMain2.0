using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Rope : ToolBase, Iitems
    {
        public override ToolNames toolName => ToolNames.Rope;
        public ItemType itemType => ItemType.Tools;
        public int itemName => (int)ToolNames.Rope;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
    }

}


