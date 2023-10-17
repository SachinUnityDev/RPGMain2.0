using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class BreakTheirBalance : PerkBase
    {
        public override PerkNames perkName => PerkNames.BreakTheirBalance;

        public override PerkType perkType => PerkType.B1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is Break Their Balance ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }


        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.maxUsagePerCombat = 5; 

        }
        public override void ApplyFX1()
        {
            if (targetController != null)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                         , AttribName.haste, -2, skillModel.timeFrame, skillModel.castTime, false);

        }

        public override void ApplyFX2()
        {
            if (40f.GetChance())
                GridService.Instance.ShuffleCharSet(CombatService.Instance.mainTargetDynas); 
              
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
    }
}
