using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class ReleaseOfFate : PerkBase
    {
        public override PerkNames perkName => PerkNames.ReleaseOfFate;

        public override PerkType perkType => PerkType.B3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.EdgyHatchet };

        public override string desc => "this is release of Fate";

        public override CharNames charName => CharNames.Abbas_Skirmisher;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

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
    }
}
