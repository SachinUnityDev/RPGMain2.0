using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class AnimalTrap : SkillBase
    {
        public override CharNames charName { get; set; }

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        public override string desc => "This is animal trap";

        public override float chance { get; set; }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear();

            for (int i = 1; i < 8; i++)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView
                                        .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna == null)  // null pos targetted 
                {
                    skillModel.targetPos.Add(cellPosData);                    
                }
            }
        }


        public override void ApplyFX1()
        {
            if (targetController == null) return;
            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                                            , (int)skillName, skillModel.dmgType[0]
                                                , skillModel.damageMod,skillModel.skillInclination, false, true); 

        }

        public override void ApplyFX2()
        {

            if (targetController == null) return;
            if (targetController.charModel.raceType == RaceType.Animal)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                     , charController.charModel.charID, CharStateName.Rooted, skillModel.timeFrame, skillModel.castTime);
            else
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                 charController.charModel.charID, AttribName.haste, -3, skillModel.timeFrame, skillModel.castTime, false);
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
            str1 = $"{skillModel.damageMod}% <style=Physical>Physical</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"Vs Animal apply<style=States> Rooted</style>, else -3 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = $"True Strike";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }
    }
}