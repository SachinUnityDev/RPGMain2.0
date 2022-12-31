using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Interactables
{
    public class Ametyst : GemBase, Iitems, IPreciousGem
    {
        public override GemNames gemName => GemNames.Ametyst;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Gems; 
        public int itemName => (int)GemNames.Ametyst;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public TGNames compatibleTg => TGNames.SimpleRing;
        public GenGewgawNames pdtGenGewgawName => GenGewgawNames.AmetystRing;
        public NPCNames mergeManagerNPC => NPCNames.AmishTheMerchant;
        public List<int> allBuffs { get; set; }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public void OnCombineWithTgFX()
        {
           
        }
        public void OnHoverItem()
        {
           
        }
    }
}