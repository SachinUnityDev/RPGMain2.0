using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class RaffiaTrapHunterArsenal : PoeticGewgawBase, Iitems, IEquipAble
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.RaffiaTrapFirstHuntersArsenal;

        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.RaffiaTrapFirstHuntersArsenal;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        int orgStmReq;
        SkillModel skillModel; 
        public override void PoeticInit()
        {
            string str = "<b>Confuse Them:</b> 100% apply Confused";
            displayStrs.Add(str);
            str = "";
            displayStrs.Add(str);
            str = "<i>Verse 1: Born in Idikum, suffered great pain</i>";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {   
            skillModel = charController.skillController.GetSkillModel(SkillNames.AnimalTrap);
            orgStmReq = skillModel.staminaReq;
            skillModel.staminaReq = 0; 
        }
        public override void UnEquipPoetic()
        {
            base.UnEquipPoetic();
            skillModel.staminaReq = orgStmReq;
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
           this.charController= charController;
            EquipGewgawPoetic();
        }

        public void RemoveEquipableFX()
        {
            UnEquipPoetic();
            charController = null;  
        }
    }
}