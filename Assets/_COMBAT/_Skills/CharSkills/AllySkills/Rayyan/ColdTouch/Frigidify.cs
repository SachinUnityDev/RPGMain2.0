using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class Frigidify : PerkBase
    {
        public override PerkNames perkName => PerkNames.Frigidify;
        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.FringesOfIce    
                                                                                ,PerkNames.EnemyOfFire };
        public override string desc => "(PR: B1 + B2) /n Frigidify, 2 rds";
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.ColdTouch;
        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
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
            if(targetController && IsTargetAlly())
            {
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                        , charController.charModel.charID, CharStateName.Aquaborne, skillModel.timeFrame, skillModel.castTime);
            }
        }
    

        public override void ApplyFX2()
        {
            if (50f.GetChance() && !isAPRewardGained)
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
            str1 = "Apply <style=States>Aquaborne</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "50% Regain AP upon cast";
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
            perkDesc = "Apply <style=States>Aquaborne</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "50% Regain AP upon cast";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

