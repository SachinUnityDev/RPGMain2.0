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
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        bool isAPRewardGained = false;
        public override void PopulateTargetPos()
        {
            SelfTarget(); 
        }        
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnEOR1 -= ResetReward;
            CombatEventService.Instance.OnEOR1 += ResetReward;

            SkillService.Instance.OnSkillUsed -= KrisLungeRegainAP;
            //CombatEventService.Instance.OnEOT -= OnEOT;

            SkillService.Instance.OnSkillUsed += KrisLungeRegainAP;
            //CombatEventService.Instance.OnEOT += OnEOT;
        }
        void ResetReward(int rd)
        {
            isAPRewardGained = false;
        }
        //public override void BaseApply()
        //{
        //    base.BaseApply();
        //    SkillService.Instance.OnSkillUsed -= KrisLungeRegainAP;
        //    CombatEventService.Instance.OnEOT -= OnEOT;

        //    SkillService.Instance.OnSkillUsed += KrisLungeRegainAP;
        //    CombatEventService.Instance.OnEOT += OnEOT;
        //}
        public override void ApplyFX1()
        {
            int stmRegen = UnityEngine.Random.Range(3, 5);
            if (targetController == null) return; 
                    targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.staminaRegen, stmRegen, skillModel.timeFrame, skillModel.castTime, true);

            charController.charStateController.RemoveCharState(CharStateName.Despaired);
            charController.charStateController.RemoveCharState(CharStateName.Feebleminded);
        }
        void KrisLungeRegainAP(SkillEventData skilleventData)
        {
            if (80f.GetChance())
            {
                if (skilleventData.skillName == SkillNames.KrisLunge && !isAPRewardGained)
                {
                    charController.combatController.IncrementAP();
                    isAPRewardGained = true;
                    SkillService.Instance.OnSkillUsed -= KrisLungeRegainAP;
                }
            }          
        }
        //void OnEOT()
        //{
        //    SkillService.Instance.OnSkillUsed -= KrisLungeRegainAP;
        //    CombatEventService.Instance.OnEOT -= OnEOT;
        //}
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
            str1 = "On use Kris Lunge this turn: %80 Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Clear <style=States>Despaired</style> and <style=States>Feebleminded</style> ";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "+3-4 Stm Regen";
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
    }

}

