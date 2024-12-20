﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EdgyHatchet : PerkBase
    {
        public override PerkNames perkName => PerkNames.EdgyHatchet;

        public override PerkType perkType => PerkType.B1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is Edgy Hatchet";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillController.allSkillBases.Find(t => t.skillName == skillName).chance = 100f;  
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
            perkDesc = "<style=Bleed>Low Bleed</style> chance: 50% -> 100%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

