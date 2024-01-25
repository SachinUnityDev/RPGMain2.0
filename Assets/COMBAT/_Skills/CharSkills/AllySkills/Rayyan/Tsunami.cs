using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Tsunami : SkillBase
    {
        public override CharNames charName { get; set; }

        public override SkillNames skillName => SkillNames.Tsunami;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeNos strikeNos => StrikeNos.Single;

        public override string desc => "This is Tsunami";

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

        public override void ApplyMoveFx()
        {

        }

        public override void ApplyVFx()
        {

        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Allies> <style=Heal>Heal,</style> 4-7";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Allies> <style=Heal>Heal,</style> 4-7";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {

        }

        public override void DisplayFX4()
        {
        }

        public override void PopulateAITarget()
        {

        }

        public override void PopulateTargetPos()
        {

        }
    }
}