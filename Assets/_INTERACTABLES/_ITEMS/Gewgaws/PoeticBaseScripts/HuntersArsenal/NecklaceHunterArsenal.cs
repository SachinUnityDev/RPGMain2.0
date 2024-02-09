using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Town;
using UnityEngine;
using static Spine.Unity.Editor.SkeletonBaker.BoneWeightContainer;


namespace Interactables
{

    public class NecklaceHunterArsenal : PoeticGewgawBase, Iitems, IEquipAble
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.NecklaceFirstHuntersArsenal;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.NecklaceFirstHuntersArsenal;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        float orgVal;
        PerkBase perkBase; 
        public override void PoeticInit()
        {
            string str = "<b>Animal Trap:</b> No Stm cost";
            displayStrs.Add(str);
            str = "";
            displayStrs.Add(str);
            str = "<i>Verse 2: A tireless trapper, N'kundo was his name</i>";
            displayStrs.Add(str);
            
        }
        public override void EquipGewgawPoetic()
        {
            perkBase = charController.skillController.GetPerkBase(SkillNames.RunguThrow, PerkNames.ConfuseThem);
            orgVal = perkBase.chance;
            perkBase.chance = 100f;
        }
        public override void UnEquipPoetic()
        {
            base.UnEquipPoetic();
            perkBase.chance = orgVal; 
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
