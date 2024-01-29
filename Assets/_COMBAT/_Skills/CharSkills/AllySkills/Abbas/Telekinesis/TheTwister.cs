using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class TheTwister : PerkBase
    {
        public override PerkNames perkName => PerkNames.TheTwister;

        public override PerkType perkType => PerkType.B2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.TheHunter };

        public override string desc => "this is the twister";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }

        public override void AddTargetPos()
        {
            base.AddTargetPos();
            TargetAnyEnemy(); 
        }
        public override void ApplyFX1()
        {
            if(targetController)
            GridService.Instance.ShuffleCharMode(CharMode.Enemy); 
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
            str1 = "Target -> All enemies";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "<style=Move>Shuffle</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        //wipe push
        public override void InvPerkDesc()
        {
            perkDesc = "Target -> All enemies";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "<style=Move>Push</style> -> <style=Move>Shuffle</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
    }
}