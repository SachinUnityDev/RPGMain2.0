using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class GlovesLegacyOfSpida : PoeticGewgawBase, Iitems, IEquipAble 
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.GlovesLegacyOfTheSpida;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.GlovesLegacyOfTheSpida;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }

        // +2-3 Dodge
        int valDodge; 
        public override void PoeticInit()
        {
            valDodge = UnityEngine.Random.Range(2, 4);
            string str = $"+{valDodge} Dodge";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        { 
                charController = InvService.Instance.charSelectController;   
                int index =
                charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.dodge, valDodge, TimeFrame.Infinity, -1, true);
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