﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class EnjoyTheTrap : PerkBase
    {
        public override PerkNames perkName => PerkNames.EnjoyTheTrap;

        public override PerkType perkType => PerkType.B2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.HuntingSeason };

        public override string desc => "this is enjoy th etrap";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }

        public override void ApplyFX1()
        {
            if (targetController == null) return;
            if (targetController.charModel.raceType == RaceType.Animal)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                  charController.charModel.charID, AttribName.morale, -3, skillModel.timeFrame, skillModel.castTime, false);
            else
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                 charController.charModel.charID, AttribName.morale, -2, skillModel.timeFrame, skillModel.castTime, false);
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
            str1 = "Vs Animal: -3 Morale, else -2";
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
            perkDesc = "Vs Animal: -3 Morale, else -2";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

