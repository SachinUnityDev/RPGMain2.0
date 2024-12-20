﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class CutTheKnee : PerkBase
    {
        public override PerkNames perkName => PerkNames.CutTheKnee;

        public override PerkType perkType => PerkType.A2; 

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is cut the knee";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get;set; }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 6; 
        }
        public override void ApplyFX1()
        {
            if(targetController)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.haste, -2f, skillModel.timeFrame, skillModel.castTime, false);

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
            str1 = "-2 Haste";
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
            perkDesc = "-2 Haste";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Stm cost: 5 -> 6";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

