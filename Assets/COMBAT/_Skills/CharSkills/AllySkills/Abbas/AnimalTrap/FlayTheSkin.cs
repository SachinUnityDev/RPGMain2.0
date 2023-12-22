using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class FlayTheSkin : PerkBase
    {
        public override PerkNames perkName => PerkNames.FlayTheSkin;

        public override PerkType perkType => PerkType.A2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is hunting season";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }
        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }
        public override void ApplyFX1()
        {
            if (targetController.charStateController.HasCharState(CharStateName.Bleeding) ||
             targetController.charStateController.HasCharState(CharStateName.Burning) ||
             targetController.charStateController.HasCharState(CharStateName.Poisoned))
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill,
                    (int)skillName, skillModel.dmgType[0], (skillModel.damageMod + 25f)
                    , skillModel.skillInclination,true);
            }
            else
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                , skillModel.dmgType[0], (skillModel.damageMod)
                                                , skillModel.skillInclination, true);
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
            str1 = "+25% Dmg vs targets with DoT";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Ignore Armor";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "+25% Dmg vs targets with DoT";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Ignore Armor";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

