using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Shovel : ToolBase, Iitems
    {
        public override ToolNames toolName => ToolNames.Shovel;
        public ItemType itemType => ItemType.Tools;
        public int itemName => (int)ToolNames.Shovel;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public void OnHoverItem()
        {

        }
    }
}