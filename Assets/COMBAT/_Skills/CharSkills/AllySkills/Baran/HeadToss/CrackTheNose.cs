using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class CrackTheNose : PerkBase
    {
        public override PerkNames perkName => PerkNames.CrackTheNose;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "%66 Low Bleed";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.HeadToss;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 66f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void ApplyFX1()
        {
            if (targetController)
                if (_chance.GetChance())
                    charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                        , charController.charModel.charID, CharStateName.BleedLowDOT);
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
            str1 = $"{_chance}%<style=Bleed> Low Bleed </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
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
            perkDesc = "{_chance}%<style=Bleed> Low Bleed </style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

