﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class UnLimitedAmo : PerkBase
    {
        public override PerkNames perkName => PerkNames.UnLimitedAmo;

        public override PerkType perkType => PerkType.A3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ConfuseThem
                ,PerkNames.ShootTheCripple  };

        public override string desc => "this is Unlimited Amo ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.cd = 1;
            skillModel.maxUsagePerCombat = -1; 
        }
        public override void ApplyFX1()
        {
            if(targetController)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                  , (int)skillName, DamageType.StaminaDmg, UnityEngine.Random.Range(4,7), false);
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
    }
}

