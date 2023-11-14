using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class WristSpin : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.WristSpin;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "this is wristSpin";
        
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            for (int i = 1; i < 4; i++) //1,2,3
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                skillModel.targetPos.Add(cellPosData);
            }
        }
        public override void ApplyFX1()
        {
            if (targetController)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                            , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
        }

        public override void ApplyFX2()
        {
            chance = 50f; // bleed chance
            if (targetController && chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                            , charController.charModel.charID, CharStateName.BleedLowDOT);
        }
        public override void DisplayFX1()
        {  
            str1 = $"{skillModel.damageMod}% <style=Physical>Physical</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"50% <style=Bleed>Low Bleed</style> ";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void ApplyFX3()
        {

        }

        public override void DisplayFX3()
        {

        }

        public override void DisplayFX4()
        {

        }

        public override void ApplyVFx()
        {
        }

        public override void PopulateAITarget()
        {
        }

        public override void ApplyMoveFx()
        {
        }
    }



}
