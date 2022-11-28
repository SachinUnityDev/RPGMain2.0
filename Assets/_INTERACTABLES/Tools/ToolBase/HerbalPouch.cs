using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class HerbalPouch : ToolBase, Iitems
    {
        public override ToolNames toolName => ToolNames.HerbalPouch;
        public ItemType itemType => ItemType.Tools;
        public int itemName => (int)ToolNames.HerbalPouch;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        }
    }
}