using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class IgnorePain : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.IgnorePain;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "ignore pain";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }


        public override void PopulateTargetPos()
        {
            SelfTarget();
        }
   
        public override void ApplyFX1()
        {  
            charController.statBuffController.ApplyStatRecAltBuff(-50f, StatName.health,  CauseType.CharSkill, (int)skillName
                , charController.charModel.charID, TimeFrame.EndOfCombat, 1, false, true);            
        }

        public override void ApplyFX2()
        {
            charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                                    , CharStateName.Invulnerable, skillModel.timeFrame, skillModel.castTime);
        }

        public override void ApplyFX3()
        {
            if(targetController != null)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.haste
                                                                , -4, skillModel.timeFrame, skillModel.castTime, false );
        }
        public override void DisplayFX1()
        {           
            str1 = "Gain <style=States>Invunerable</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"-4 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"-50%<style=Heal> Healing </style>received until eoc";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);
        }


        public override void PopulateAITarget()
        {
        }

        public override void ApplyMoveFx()
        {
        }
    }



}

