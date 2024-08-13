using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class SluggerRungu : PerkBase
    {
        public override PerkNames perkName => PerkNames.Sluggerungu;

        public override PerkType perkType => PerkType.B3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is slugger rungu";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

        float initialdmg = 0;

        float dmgMod = 0;

        public override void PerkInit(SkillController1 skillController)
        {
            base.PerkInit(skillController);
            initialdmg = skillModel.damageMod;
            dmgMod = initialdmg; 
        }
        public override void BaseApply()
        {
            base.BaseApply();
            if (targetController)
            {
                dmgMod += 10f;
                skillModel.damageMod= dmgMod;
            }
                
            CombatEventService.Instance.OnEOC -= OnEOC;
            CombatEventService.Instance.OnEOC += OnEOC;
        }

        public override void ApplyFX1()
        {
                
        }

        void OnEOC()
        {
            skillModel.damageMod = initialdmg; 
            dmgMod= 0;
            CombatEventService.Instance.OnEOC -= OnEOC;
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
            str1 = "Each next Rungu Throw: +10% Dmg until eoc";
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
            perkDesc = "Each next Rungu Throw: +10% Dmg until eoc";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Stm cost: 7 -> 8";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

