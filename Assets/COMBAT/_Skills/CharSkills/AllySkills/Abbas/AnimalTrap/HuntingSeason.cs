using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class HuntingSeason : PerkBase
    {
        public override PerkNames perkName => PerkNames.HuntingSeason;

        public override PerkType perkType => PerkType.B1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is hunting season";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }

        public override void ApplyFX1()
        {
            if (targetController == null) return;
            if (targetController.charModel.raceType == RaceType.Animal)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                          , charController.charModel.charID, CharStateName.BleedHighDOT);
            else if(50f.GetChance())
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                         , charController.charModel.charID, CharStateName.BleedLowDOT);
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
