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
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }

        int valHunger; 

        public override void PoeticInit()
        {
            valHunger = UnityEngine.Random.Range(12, 19);
            string str = $"-{valHunger}% Hunger";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            charController = InvService.Instance.charSelectController;

            charController.charModel.hungerMod += valHunger;
       
        }
        public override void UnEquipPoetic()
        {
            charController.ChangeStat(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatName.hunger, valHunger, true);

            charController.charModel.hungerMod -= valHunger;
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
           this.itemId = itemId;
           this.maxInvStackSize = maxInvStackSize;
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