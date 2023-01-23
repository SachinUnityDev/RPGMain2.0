using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class ThrowingKnifeHunterArsenal : PoeticGewgawBase, Iitems, IEquipAble
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.ThrowingKnifeFirstHuntersArsenal;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.ThrowingKnifeFirstHuntersArsenal;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public override void PoeticInit()
        {

        }
        public override void EquipGewgawPoetic()
        {

        }
        public override void UnEquipPoetic()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
           
        }

        public void OnHoverItem()
        {
            
        }

        public void ApplyEquipableFX()
        {
           
        }

        public void RemoveEquipableFX()
        {
          
        }
    }
}