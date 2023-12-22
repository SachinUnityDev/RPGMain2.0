using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class KrisLunge : SkillBase
    {
        public override SkillNames skillName => SkillNames.KrisLunge;
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override string desc => "This is Kris Lunge";

        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public List<int> possibleTargetLoc = new List<int>();

        public override void PopulateTargetPos()
        {
            FirstOnSamelane();
        }

        public override void ApplyFX1()
        {
            if(targetController)
               targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                ,DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
        }
        public override void ApplyFX2()
        {   
            if (30f.GetChance())
                targetController.damageController.ApplyHighBleed(CauseType.CharSkill, (int)skillName
                             , charController.charModel.charID);
            if (chance.GetChance())
                targetController.damageController.ApplyHighPoison(CauseType.CharSkill, (int)skillName
                             , charController.charModel.charID);

        }
        public override void ApplyFX3()
        {
            if (targetController)            
                GridService.Instance.gridMovement.MovebyRow(myDyna, MoveDir.Forward, 1);            
        }
 
        public override void DisplayFX1()
        {
            str1 = $"{skillModel.damageMod}% <style=Physical>Physical</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"{chance}% <style=Bleed>High Bleed</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = $"<style=Move>Move</style> forward 1";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }
        public override void DisplayFX4()
        {
        }
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None);
        }
        public override void PopulateAITarget()
        {
           
        }
        public override void ApplyMoveFx()
        {
        }
    }


}
