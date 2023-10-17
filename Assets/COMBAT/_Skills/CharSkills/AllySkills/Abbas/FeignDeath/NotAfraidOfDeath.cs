using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 
namespace Combat
{

    public class NotAfraidOfDeath : PerkBase
    {
        public override PerkNames perkName => PerkNames.NotAfraidOfDeath;

        public override PerkType perkType => PerkType.B1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is Not afraid of death ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.FeignDeath;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }

        public override void ApplyFX1()
        {
            StatData fortStatData = charController.GetStat(StatName.fortitude);
            
            if (fortStatData.currValue < 0f)
            {
                charController.ChangeStat(CauseType.CharSkill, (int)skillName
                                , charController.charModel.charID, StatName.fortitude, 30f);
            }
        }

        public override void ApplyFX2()
        {
            AttribData moraleStat = charController.GetAttrib(AttribName.morale);
            if (moraleStat.currValue == 12)
            {
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName
                 , charID, AttribName.fortOrg, 2, TimeFrame.EndOfQuest, 1, true); 
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
