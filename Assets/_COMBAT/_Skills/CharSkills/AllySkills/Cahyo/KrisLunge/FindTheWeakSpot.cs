using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class FindTheWeakSpot : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.KrisLunge;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.FindTheWeakSpot;
        public override PerkType perkType => PerkType.A2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is find the weak spot";
        private float _chance = 50f;
        public override float chance { get => _chance; set => _chance = value; }

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
            if(targetController)
                targetController.ChangeStat(CauseType.CharSkill
                             , (int)skillName, charController.charModel.charID, StatName.stamina,
                               -UnityEngine.Random.Range(3, 6));
        }

        public override void ApplyFX2()
        {
            if (targetController.charStateController.HasCharState(CharStateName.Bleeding))
                if (chance.GetChance() && !isAPRewardGained)
                {
                    charController.combatController.IncrementAP();
                    isAPRewardGained = true; 
                }
                    
        }
        public override void DisplayFX1()
        {
            str1 = "50% regain AP vs <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Drain <style=Stamina>3-5 Stm</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void ApplyFX3()
        {
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {

        }
        public override void ApplyVFx()
        {
        }

        public override void ApplyMoveFX()
        {
        }
        public override void InvPerkDesc()
        {
            //perkDesc = "<style=Bleed>High Bleed</style> chance: 30% -> 60%";
            //SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "50% regain AP vs <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(perkDesc);

            perkDesc = "Stm drain.. 3-5";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

