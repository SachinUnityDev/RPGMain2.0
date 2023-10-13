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

        bool subscribed = false; 
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return; skillModel.targetPos.Clear();
                 skillModel.targetPos.Add(new CellPosData(myDyna.charMode, myDyna.currentPos));
        }
       
        void EndSubs()
        {
            CombatEventService.Instance.OnDmgDelivered -= ReduceHealingUntilEOC;
            CombatEventService.Instance.OnEOC -= EndSubs;
            subscribed = false;
        }

        void ReduceHealingUntilEOC(DmgData dmgData)
        {
           if(dmgData.dmgRecievedType == DamageType.Heal)
           {
                if(dmgData.targetController == charController)
                {
                    int healVal = (int)dmgData.dmgDelivered / 2;
                    charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.health
                        , -healVal, false);
                }               
           }
        }

        public override void ApplyFX1()
        {
            if (subscribed) return;
            CombatEventService.Instance.OnDmgDelivered += ReduceHealingUntilEOC;
            CombatEventService.Instance.OnEOC += EndSubs;
            subscribed = true;
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
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"-4 <style=Attributes>Haste</style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"-50%<style=Heal> Healing </style>recieved until eoc";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
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

