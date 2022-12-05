using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Interactables
{
    public class Ametyst : GemBase, Iitems, IPreciousGem
    {
       
        public override GemName gemName => GemName.Ametyst;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Gems; 
        public int itemName => (int)GemName.Ametyst;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public TGNames compatibleTg => TGNames.SimpleRing;
        public GenGewgawNames pdtGenGewgawName => GenGewgawNames.AmetystRing;
        public NPCNames mergeManagerNPC => NPCNames.AmishTheMerchant;

        public void OnCombineWithTgFX()
        {
           
        }
        public void OnHoverItem()
        {
           
        }
    }
}