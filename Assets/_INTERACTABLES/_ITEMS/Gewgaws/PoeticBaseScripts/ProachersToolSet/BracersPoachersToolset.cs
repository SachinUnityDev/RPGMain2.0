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
        public int itemName => (int)PoeticGewgawNames.BeltPoachersToolset;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        int valAcc;
        public override void PoeticInit()
        {
            valAcc = UnityEngine.Random.Range(2, 4);
            string str = $"+{valAcc} Accuracy";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            int index = charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.acc, valAcc, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        public override void UnEquipPoetic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
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
