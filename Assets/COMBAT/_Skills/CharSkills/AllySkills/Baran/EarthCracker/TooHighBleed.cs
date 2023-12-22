using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class TooHighBleed : PerkBase
    {
        public override PerkNames perkName => PerkNames.TooHighBleed;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Hits initial target only(No Earth dmg)/n 60% High Bleed/n 3 --> 2 rds cd ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 60f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();

            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3;

        }


        public override void SkillSelected()
        {
            base.SkillSelected();

            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX3;
        }
        public override void BaseApply()
        {
            base.BaseApply();
            skillModel.cd = 2;
        }

        public override void ApplyFX1()
        {
            if (_chance.GetChance())
            {
                targetController.damageController.ApplyHighBleed(CauseType.CharSkill, (int)skillName, charController.charModel.charID);
            }
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
            str0 = $"{chance}%<style=Bleed> High Bleed </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
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
            perkDesc = "Hits initial target only";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "60% Low -> <style=Bleed>High Bleed</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Cd: 3 -> 2 rds";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}



