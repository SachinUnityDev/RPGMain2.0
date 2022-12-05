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
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
         
        }
    }
}

