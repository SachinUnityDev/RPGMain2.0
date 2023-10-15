using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class InspiringTouch : PerkBase  // level 1 Apply FX ..         
    {
        public override CharNames charName => CharNames.Rayyan;
        public override PerkNames perkName => PerkNames.InspiringTouch;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Inspired, 2 rds";
      
        public override SkillNames skillName => SkillNames.ColdTouch;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void ApplyFX1()
        {
            if (targetController && IsTargetAlly())
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                                                            , CharStateName.Inspired, skillModel.timeFrame, skillModel.castTime);
            }
        }


        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyVFx()
        {
        }
        public override void ApplyMoveFX()
        {
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=States> Inspired </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

    }

}

