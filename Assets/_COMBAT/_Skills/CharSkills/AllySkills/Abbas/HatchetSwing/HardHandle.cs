using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class HardHandle : PerkBase
    {
        public override PerkNames perkName => PerkNames.HardHandle;

        public override PerkType perkType => PerkType.A3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.CutTheKnee
                                                                                , PerkNames.DoubleSwing };
        public override string desc => "this is cut the knee";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

        public SkillBase runguThrowBase; 
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnEOT += OnEOT;
            runguThrowBase = skillController.GetSkillBase(SkillNames.RunguThrow);
            runguThrowBase.chance = -5; 
        }

        void OnEOT()
        {
            runguThrowBase.chance = 0;
            CombatEventService.Instance.OnEOT -= OnEOT;
        }
        public override void ApplyFX1()
        {
            if(targetController != null)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                   , charController.charModel.charID, CharStateName.Feebleminded, skillModel.timeFrame, skillModel.castTime);
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = "Apply <style=States>Feebleminded</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Next Rungu Throw this turn: True Strike";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Apply <style=States>Feebleminded</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Next Rungu Throw this turn: True Strike";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

