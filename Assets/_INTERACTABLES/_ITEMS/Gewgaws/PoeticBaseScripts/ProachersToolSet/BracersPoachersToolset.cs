using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{

    public class BracersPoachersToolset : PoeticGewgawBase, Iitems, IEquipAble
    {
        //+2-3 Acc
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.BracersPoachersToolset;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.BracersPoachersToolset;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        int valDmg;
        public override void PoeticInit()
        {
            valDmg = UnityEngine.Random.Range(12,19) ;
            string str = $"+{valDmg}% Dmg vs Rooted";
            displayStrs.Add(str);
            str = "";
            displayStrs.Add(str);
            str = "<i>Verse 3: Fire: Execute your prey</i>";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            int index = charController.strikeController.ApplyCharStateDmgAltBuff(valDmg, CauseType.PoeticGewgaw
            , (int)poeticGewgawName, charController.charModel.charID, TimeFrame.Infinity, -1, true, CharStateName.Rooted); 
            allCharStateBuffID.Add(index);

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
