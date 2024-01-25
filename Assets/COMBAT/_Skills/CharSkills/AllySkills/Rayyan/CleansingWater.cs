using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro; 
 

namespace Combat
{
    public class CleansingWater : SkillBase
    {     
        
        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeNos strikeNos => StrikeNos.Single; 
        public override string desc => "this is cleansing water";       

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

       
        public override void PopulateTargetPos()
        {         
            AllinCharModeExceptSelf(CharMode.Ally);
        }    
        public override void ApplyFX1()
        {
            Debug.Log("this is cleansing water Apply FX1 ");

            if (targetController && IsTargetMyAlly())
                    targetController.ChangeStat( CauseType.CharSkill, (int)skillName, charID, StatName.health, UnityEngine.Random.Range(4f, 7f));
        }
        public override void ApplyFX2()
        {           
           if(IsTargetMyAlly())
            targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                    , AttribName.haste, 2, skillModel.timeFrame, skillModel.castTime,true);
        }
        public override void ApplyFX3()
        {          
            targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                                                            , CharStateName.Soaked, skillModel.timeFrame, skillModel.castTime);
        }

        public override void DisplayFX1()
        {
            str1 = "<style=Heal>Heal</style> 4-7";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "+2 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "Apply <style=States>Soaked</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }
     
        public override void DisplayFX4()
        {
           
        }

        public override void ApplyVFx()
        {          
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, strikeNos); 

        } 
        public override void PopulateAITarget()
        {
           
        }

        public override void ApplyMoveFx()
        {

        }
    }
}

