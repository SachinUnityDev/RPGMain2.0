using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class InTheNameOfUsmu : SkillBase
    {
        public override SkillNames skillName => SkillNames.InTheNameOfUsmu;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override string desc => "This is in the name of usmu";
        public override StrikeNos strikeNos => StrikeNos.Single;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            SelfTarget(); 
        }
        public override void BaseApply()
        {
            base.BaseApply();
            SkillService.Instance.OnSkillUsed -= KrisLungeRegainAP;
            CombatEventService.Instance.OnEOT -= OnEOT;

            SkillService.Instance.OnSkillUsed += KrisLungeRegainAP;
            CombatEventService.Instance.OnEOT += OnEOT;
        }
        public override void ApplyFX1()
        {
            int stmRegen = UnityEngine.Random.Range(3, 6);
            if(targetController != null)
                    targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.staminaRegen, stmRegen, skillModel.timeFrame, skillModel.castTime, true); 
        }
        void KrisLungeRegainAP(SkillEventData skilleventData)
        {
            if (80f.GetChance())
            {
                if (skilleventData.skillName == SkillNames.KrisLunge)
                {
                    RegainAP();
                }
            }          
        }
        void OnEOT()
        {
            SkillService.Instance.OnSkillUsed -= KrisLungeRegainAP;
            CombatEventService.Instance.OnEOT -= OnEOT;
        }
        public override void ApplyFX2()
        {
           
        }

        public override void ApplyFX3()
        {
           
        }

        public override void ApplyMoveFx()
        {
        }
        public override void DisplayFX1()
        {
            str1 = "On use Kris Lunge this turn:";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "%80 Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "+3-5 Stm Regen";
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
    }

}

