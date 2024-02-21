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
            if (targetController.charModel.raceType == RaceType.Animal)
                targetController.strikeController.ApplyDmgAltBuff(-12f, CauseType.CharSkill, (int)skillName
                 , charController.charModel.charID, TimeFrame.EndOfCombat, 1, false, AttackType.None, skillModel.dmgType[0]);
            else
                targetController.strikeController.ApplyDmgAltBuff(-6f, CauseType.CharSkill, (int)skillName
                , charController.charModel.charID, TimeFrame.EndOfCombat, 1, false, AttackType.None, skillModel.dmgType[0]);
               
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
            str1 = "Vs Animal: -12% Dmg <style=Physical>Physical</style> Skills until eoc";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Vs else: -6% Dmg <style=Physical>Physical</style> Skills until eoc";
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
            perkDesc = "Vs Animal: -12% Dmg <style=Physical>Physical</style> Skills until eoc";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Vs else: -6% Dmg <style=Physical>Physical</style> Skills until eoc";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
