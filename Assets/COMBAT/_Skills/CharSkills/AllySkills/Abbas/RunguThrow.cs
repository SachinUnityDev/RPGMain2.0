using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{

    public class RunguThrow : SkillBase
    {

        public override CharNames charName { get; set; }
        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeNos strikeNos => StrikeNos.Single; 

        public override string desc => "Rungu throw ";

        public override float chance { get; set; }
        DynamicPosData targetDyna; 
        List<DynamicPosData> sameLaneTargets= new List<DynamicPosData>();
        public override void PopulateTargetPos()
        {
            if (skillModel != null)
            {
                skillModel.targetPos.Clear();
                CombatService.Instance.mainTargetDynas.Clear();
                CellPosData cellPos = new CellPosData(CharMode.Ally, myDyna.currentPos);
                sameLaneTargets = GridService.Instance.GetInSameLaneOppParty(cellPos);
                
                if(sameLaneTargets.Count > 0 ) 
                    targetDyna = sameLaneTargets[0];
                else
                    targetDyna = null;                
                if(targetDyna != null)
                {
                    CombatService.Instance.mainTargetDynas.Add(targetDyna);
                    skillModel.targetPos.Add(new CellPosData(targetDyna.charMode, targetDyna.currentPos));
                }
            }
        }

        public override void ApplyFX1()
        {
            if (targetController == null) return; 

            if(chance == -5)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                              , DamageType.Physical, skillModel.damageMod
                              ,skillModel.skillInclination, false, true);
            else
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                              , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);

            chance = 0; 

        }

        public override void ApplyFX2()
        {
            if (targetController & !IsDodged())
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                         , AttribName.focus, -2, skillModel.timeFrame, skillModel.castTime, false);
        }

        public override void ApplyFX3()
        {
           
        }

        public override void ApplyMoveFx()
        {
           
        }

        public override void ApplyVFx()
        {
            if (targetController & !IsDodged())
                SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, strikeNos);
        }

        public override void DisplayFX1()
        {
            str1 = $"{skillModel.damageMod}% <style=Physical>Physical</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "-2 Focus";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            
        }

        public override void DisplayFX4()
        {
           
        }

     

 
    }
}