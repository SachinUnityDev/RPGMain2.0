using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class HideInTheBushes : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.HideInTheBushes;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Hide in the bushes";

        private float _chance = 0f;
        public override float chance { get; set; }
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override void PopulateTargetPos()
        {
            SelfTarget(); 
        }
        public override void BaseApply()
        {
            base.BaseApply();
            SkillService.Instance.OnSkillUsed -= AnimalTrapRegainAP;
            CombatEventService.Instance.OnEOT -= OnEOT;

            SkillService.Instance.OnSkillUsed += AnimalTrapRegainAP;
            CombatEventService.Instance.OnEOT += OnEOT;
        }
        void AnimalTrapRegainAP(SkillEventData skillEventData)
        {
            if (skillEventData.strikerController.charModel.charID != charID) return;
            if (50f.GetChance())
            {
                if (skillEventData.skillName == SkillNames.AnimalTrap)
                {
                    RegainAP();
                }
            }
            return;
        }
        void OnEOT()
        {
            SkillService.Instance.OnSkillUsed -= AnimalTrapRegainAP;
            CombatEventService.Instance.OnEOT -= OnEOT;
        }
        public override void ApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                 , AttribName.haste, +1, TimeFrame.EndOfCombat, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                 , AttribName.haste, -3, skillModel.timeFrame, skillModel.castTime, false);

            int stmGain = UnityEngine.Random.Range(5, 8); 
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID
                                                        , StatName.stamina, stmGain);
        }

        public override void ApplyFX2()
        {
            charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                            , charController.charModel.charID, CharStateName.Sneaky);
        }

        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {
            str1 = "-3 Haste for skill duration, +1 Haste until eoc";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Gain<style=Stamina> 5-7 Stm</style> and <style=States>Sneaky</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "Next Animal Trap: 50% Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }
      
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedSingleStrike(PerkType.None, strikeNos);
        }

        public override void ApplyMoveFx()
        {

        }

        public override void PopulateAITarget()
        {

        }
    }
    
}