using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class AnimalHunter : PerkBase
    {
        public override PerkNames perkName => PerkNames.AnimalHunter;

        public override PerkType perkType => PerkType.B2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is animal hunter ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }

        public override void ApplyFX1()
        {

            if(targetController)
                charController.strikeController.ApplyDmgAltBuff(-12f, CauseType.CharSkill, (int)skillName
                 , charController.charModel.charID, TimeFrame.EndOfCombat, 1, false, skillModel.attackType, skillModel.dmgType[0],
                    CultureType.None, RaceType.Animal);
            else
                charController.strikeController.ApplyDmgAltBuff(-6f, CauseType.CharSkill, (int)skillName
                , charController.charModel.charID, TimeFrame.EndOfCombat, 1, false, skillModel.attackType, skillModel.dmgType[0],
                CultureType.None, RaceType.None);
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
