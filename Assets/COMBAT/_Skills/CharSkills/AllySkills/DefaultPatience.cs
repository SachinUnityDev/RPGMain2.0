using Common;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    public class DefaultPatience : SkillBase
    {
        public override SkillNames skillName => SkillNames.DefaultPatience;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override string desc => "this is default patience";

     
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            SelfTarget(); 
        }
     
        public override void ApplyFX1()
        {
            if (charController)
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.staminaRegen, 2, TimeFrame.EndOfRound, skillModel.castTime, false);
        }

        public override void ApplyFX2()
        {
            if (charController)
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.haste, -2, TimeFrame.EndOfRound, skillModel.castTime,false);
        }

        public override void ApplyFX3()
        {
        }





        public override void DisplayFX1()
        {
            str1 = $"Wait 1 turn";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"+2<style=Stamina> Stamina Regen </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"-2<style=Attribute> Haste </style>, 2 rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.None);
        }

      

        public override void PopulateAITarget()
        {
            // add self target .. 
            SkillService.Instance.currentTargetDyna = myDyna;

        }

        public override void ApplyMoveFx()
        {
        }
    }

}
