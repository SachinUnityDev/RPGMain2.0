using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class TheTrickster : PerkBase
    {
        public override PerkNames perkName => PerkNames.TheTrickster;

        public override PerkType perkType => PerkType.A3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.TheTwister, PerkNames.TheHunter };

        public override string desc => "this is the trickster";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

        bool isAPRewardGained = false;
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnEOR1 -= ResetReward;
            CombatEventService.Instance.OnEOR1 += ResetReward;
        }
        void ResetReward(int rd)
        {
            isAPRewardGained = false;
        }
        public override void ApplyFX1()
        {
            chance = 50f; 
            if (chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                , charController.charModel.charID, CharStateName.Confused, skillModel.timeFrame, skillModel.castTime);
        }

        public override void ApplyFX2()
        {
            if (targetController && !isAPRewardGained)
            {
                charController.combatController.IncrementAP();
                isAPRewardGained = true;
            }               
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
            str1 = "50% apply <style=States>Confused</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void InvPerkDesc()
        {
            perkDesc = "50% apply <style=States>Confused</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
    }
}