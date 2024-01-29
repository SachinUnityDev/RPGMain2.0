using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using Quest; 

namespace Combat
{
    public class FuryOfPain : PerkBase
    {
        public override PerkNames perkName => PerkNames.FuryOfPain; 

        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "If self hp< 40% on cast, gains Enraged after 2 rds /n No Luck and Haste handicap./n If Luck 12; gains +3 fort origin until eoq ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.IgnorePain;

        public override SkillLvl skillLvl => SkillLvl.Level1;


        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        
        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }


        public override void ApplyFX1()
        {
            StatData hpStatData = charController.GetStat(StatName.health);
            float hpPercent = hpStatData.currValue / hpStatData.maxLimit;
            if (hpPercent < 0.4f)
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                , charController.charModel.charID, CharStateName.Enraged, skillModel.timeFrame, skillModel.castTime);
            }
        }

    

        public override void ApplyFX2()
        {
            AttribData luckAttrib = charController.GetAttrib(AttribName.luck);

            if (luckAttrib.currValue == 12)          
                 RegainAP(); 
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
            str0 = $"If self Hp < 40%: Gain<style=States> Enraged</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"If Luck 12: Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "If self Hp < 40%: Gain<style=States> Enraged</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "If Luck 12: Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }

}
