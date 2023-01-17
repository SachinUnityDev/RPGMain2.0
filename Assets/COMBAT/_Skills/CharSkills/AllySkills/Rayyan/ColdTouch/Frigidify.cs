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
    
        public override void ApplyFX1()
        {
            if(targetController && IsTargetAlly())
            {
                CharStatesService.Instance
                     .ApplyCharState(targetGO, CharStateName.Frigid
                                     , charController, CauseType.CharSkill, (int)skillName);
            }
        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Frigid);
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
            str1 = $"<style=States> Frigid </style>, {skillModel.castTime} rds";
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

