using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Ruby : GemBase, Iitems, IPreciousGem   
    {
        public override GemName gemName => GemName.Ruby;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemName.Ruby;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public TGNames compatibleTg => TGNames.SimpleRing;
        public GenGewgawNames pdtGenGewgawName => GenGewgawNames.RubyRing;
        public NPCNames mergeManagerNPC => NPCNames.AmishTheMerchant;

        public void OnCombineWithTgFX()
        {

        }
        public void OnHoverItem()
        {

        }

    }
}

