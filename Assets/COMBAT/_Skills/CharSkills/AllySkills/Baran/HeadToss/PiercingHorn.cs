using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class PiercingHorn : PerkBase
    {
        public override PerkNames perkName => PerkNames.PiercingHorn;

        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.CrackTheNose };

        public override string desc => "(PR: B1) /n Ignores armor /n Self trigger Max Armor, 2 rds";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.HeadToss;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        float armorChg; 

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            skillModel.cd = 1; 
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }

        public override void ApplyFX1()
        {
            if (targetController)  // ignore armor
                targetController.damageController.ApplyDamage(charController,CauseType.CharSkill, (int)skillName
                                                                    , DamageType.Physical, skillModel.damageMod
                                                                    ,skillModel.skillInclination, true);

        }

        public override void ApplyFX2()
        {
      
        }

    
        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = "Ignore<style=Attributes> Armor </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
           
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Ignore<style=Attributes> Armor </style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Cd: 2 -> 1 rd";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

