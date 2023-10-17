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
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single; 

        public override string desc => "Rungu throw ";

        public override float chance { get; set; }
        DynamicPosData targetDyna; 
        public override void PopulateTargetPos()
        {
            if (skillModel != null)
            {
                skillModel.targetPos.Clear();
                CombatService.Instance.mainTargetDynas.Clear();
                CellPosData cellPos = new CellPosData(CharMode.Enemy, targetDyna.currentPos);
                skillModel.targetPos.Add(cellPos);
                targetDyna = GridService.Instance.GetInSameLaneOppParty(cellPos)[0];
                CombatService.Instance.mainTargetDynas.Add(targetDyna);
            }
        }

        public override void ApplyFX1()
        {
            if (targetController == null) return; 

            if(chance == -5)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                              , DamageType.Physical, skillModel.damageMod, false, true);
            else
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                              , DamageType.Physical, skillModel.damageMod, false);

        }

        public override void ApplyFX2()
        {
            if (targetController != null)
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
           
        }

        public override void DisplayFX1()
        {
           
        }

        public override void DisplayFX2()
        {
           // -2 focus string here
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

 
    }
}