using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Telekinesis : SkillBase
    {
        public override CharNames charName { get; set; }

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        public override string desc => " Telekenesis";

        public override float chance { get; set; }

        public override void PopulateTargetPos()
        {
            FirstOnSamelane(); 
        }
        public override void ApplyFX1()
        {
           if(targetController)             // pure dmg
            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                            , skillModel.dmgType[0], UnityEngine.Random.Range(5,9), skillModel.skillInclination);
        }

        public override void ApplyFX2()
        {
            DynamicPosData dyna = GridService.Instance.GetDyna4GO(targetGO);
            if (targetController)
                GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Backward, 1);
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
            str1 = "<style=Pure>5-8 Pure</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "<style=Push>Push</style> 1";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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