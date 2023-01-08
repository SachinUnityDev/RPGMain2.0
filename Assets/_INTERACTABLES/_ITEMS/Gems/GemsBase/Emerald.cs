using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Emerald : GemBase, Iitems, IPreciousGem   
    {
        public override GemNames gemName => GemNames.Emerald;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemNames.Emerald;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public TGNames compatibleTg => TGNames.SimpleRing;
        public GenGewgawNames pdtGenGewgawName => GenGewgawNames.EmeraldRing;
        public NPCNames mergeManagerNPC => NPCNames.AmishTheMerchant;
        public List<int> allBuffs { get; set; } = new List<int>();

        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }

        public void OnCombineWithTgFX()
        {

        }
 

    }
}

