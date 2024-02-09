using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class BeltPoachersToolset : PoeticGewgawBase, Iitems, IEquipAble
    {
        // -12-18% Hunger mod
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.BeltPoachersToolset;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.BeltPoachersToolset;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        int valHunger; 

        public override void PoeticInit()
        {
            valHunger = UnityEngine.Random.Range(12, 19);
            string str = $"-{valHunger}% Hunger";
            displayStrs.Add(str);      
            str = "";
            displayStrs.Add(str);
            str = "<i>Verse 1: Ready: Withstand your voracity</i>";
            displayStrs.Add(str);
             valHunger = UnityEngine.Random.Range(-12, -18);
        }
        public override void EquipGewgawPoetic()
        {
           int statBuffId = 
            charController.statBuffController.ApplyStatRecAltBuff(valHunger, StatName.hunger, CauseType.PoeticGewgaw
                , (int)itemName,  charController.charModel.charID, TimeFrame.Infinity, 1, true);
            allStatAltBuff.Add(statBuffId);          
        }
        

        public void InitItem(int itemId, int maxInvStackSize)
        {
           this.itemId = itemId;
           this.maxInvStackSize = maxInvStackSize;
            PoeticInit();
        }

        public void OnHoverItem()
        {
          
        }

        public void ApplyEquipableFX(CharController charController)
        {
            this.charController = charController;
            EquipGewgawPoetic();
        }
        public void RemoveEquipableFX()
        {
            UnEquipPoetic();
            charController = null;
        }
    }
}