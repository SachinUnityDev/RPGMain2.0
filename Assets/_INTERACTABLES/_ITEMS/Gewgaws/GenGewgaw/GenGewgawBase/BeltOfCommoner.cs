using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class BeltOfCommoner : GenGewgawBase, Iitems, IEquipAble
    {
        public override GenGewgawNames genGewgawNames => GenGewgawNames.BeltOfTheCommoner;
        public ItemType itemType => ItemType.GenGewgaws;
        public int itemName => (int)GenGewgawNames.BeltOfTheCommoner;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }

        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }

        //public override void EquipGenGewgawFX()
        //{
        //    EquipGenGewgawFX();

        //}

        public void ApplyEquipableFX(CharController charController)
        {
            this.charController = charController;
            EquipGenGewgawFX();
        }

        public void RemoveEquipableFX()
        {
            UnEquipGenGewgawFX();
            charController = null; 
        }
    }
}