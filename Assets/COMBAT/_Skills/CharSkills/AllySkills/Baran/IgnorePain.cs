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
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "ignore pain";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

       // bool subscribed = false; 
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return; 
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear(); 

                skillModel.targetPos.Add(new CellPosData(myDyna.charMode, myDyna.currentPos));
                CombatService.Instance.mainTargetDynas.Add(myDyna);    
        }
   
        public override void ApplyFX1()
        {           
            charController.strikeController.ApplyDmgAltBuff(-50f, CauseType.CharSkill, (int)skillName
                , charController.charModel.charID, TimeFrame.EndOfCombat, 1, false, AttackType.None, DamageType.Heal);
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
            str1 = $"<style=States>Invunerable</style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"-4 <style=Attributes>Haste</style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"-50%<style=Heal> Healing </style>recieved until eoc";
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

