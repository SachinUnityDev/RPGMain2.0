using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class DeepCut : PerkBase
    {
        public override PerkNames perkName => PerkNames.DeepCut;

        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "50% Low --> 100% Low Bleed";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 100f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillController.allSkillBases.Find(t => t.skillName == skillName).chance = _chance;
        }
        
        public override void ApplyFX1()
        {
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
            perkDesc = "<style=Bleed>Low Bleed</style> chance 50% -> 100%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

