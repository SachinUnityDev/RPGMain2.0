using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Key : ToolBase,Iitems
    {
        public override ToolNames toolName => ToolNames.Key;
        public ItemType itemType => ItemType.Tools;
        public int itemName => (int)ToolNames.Key;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        }
    }
}