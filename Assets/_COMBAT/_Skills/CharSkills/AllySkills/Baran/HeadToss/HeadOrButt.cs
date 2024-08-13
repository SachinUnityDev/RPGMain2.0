using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class HeadOrButt : PerkBase
    {
        public override PerkNames perkName => PerkNames.HeadOrButt;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "-2 Focus --> Confuse, 2 rds";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.HeadToss;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
    
        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
        }

        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;

        }


        public override void ApplyFX1()
        {
          if(targetController)
            charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, CharStateName.Confused, skillModel.timeFrame, skillModel.castTime);
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
            str0 = "Apply<style=States> Confused </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
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
        public override void InvPerkDesc()
        {
            perkDesc = "-2 Focus -> Confuse";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}



