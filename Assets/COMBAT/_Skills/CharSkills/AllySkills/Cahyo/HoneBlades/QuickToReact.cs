using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class QuickToReact : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.QuickToReact;
        public override PerkType perkType => PerkType.A1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Quick To react";
        private float _chance = 20f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.cd = 2;           
        }


        public override void ApplyFX1()
        {
            if (chance.GetChance())
                RegainAP(); 
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {
            str1 = $"20% Regain AP";
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
        public override void ApplyVFx()
        {
        }

        public override void ApplyMoveFX()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "20% Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Cd: 3 -> 2";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

