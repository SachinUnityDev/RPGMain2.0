using Combat;
using Common;
using NUnit.Framework;
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
        public Currency currency { get; set; }

        SkillModel skillModel; 
        public override void PoeticInit()
        {
            string str = "<b>Throw and Perk:</b> +35% Dmg";
            displayStrs.Add(str);
            str = "";
            displayStrs.Add(str);
            str = "<i>Verse 3: Shredded his foes, the ones he couldn't tame</i>";
            displayStrs.Add(str);

        }
        public override void EquipGewgawPoetic()
        {  
             skillModel = charController.skillController.GetSkillModel(SkillNames.HatchetSwing);
            SkillService.Instance.OnPerkStateChg += OnPerkSelect;
            SkillService.Instance.OnPerkStateChg += OnPerkUnSelect;


        }
        public override void UnEquipPoetic()
        {
            SkillService.Instance.OnPerkStateChg -= OnPerkSelect;
            SkillService.Instance.OnPerkStateChg -= OnPerkUnSelect;
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

   

        void OnPerkSelect(PerkData perkData)
        {
            if(perkData.perkName == PerkNames.ThrowAndPick)
            {
                skillModel.damageMod += 35; 
            }
        }
        void OnPerkUnSelect(PerkData perkData) 
        {
            if (perkData.perkName == PerkNames.ThrowAndPick)
            {
                skillModel.damageMod -= 35;
            }
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