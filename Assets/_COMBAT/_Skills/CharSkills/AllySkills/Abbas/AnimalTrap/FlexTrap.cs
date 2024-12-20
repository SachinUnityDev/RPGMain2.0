﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class FlexTrap : PerkBase
    {
        public override PerkNames perkName => PerkNames.Flextrap;

        public override PerkType perkType => PerkType.A1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is Flex Trap ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.castPos.Clear();
            skillModel.castPos = new List<int> { 1,2,3,4,5,6,7 };
            skillModel.maxUsagePerCombat = 6; 
        }
        public override void ApplyFX1()
        {
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
            perkDesc = "Can cast from anywhere";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Use 4 -> 6";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}
