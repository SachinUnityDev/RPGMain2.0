using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class FeignDeath : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.FeignDeath;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Feign Death";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void PopulateTargetPos()
        {
            SelfTarget(); 
        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            StatData statData = charController.GetStat(StatName.health);
            if (statData != null)
            {
                float hpPercent = statData.currValue / statData.maxLimit;
                if (hpPercent > 0.4f)
                    skillModel.SetSkillState(SkillSelectState.UnClickable_Misc);
            }
        }
        public override void ApplyFX1()
        {
            if(targetController)
                    charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                                 , AttribName.dodge, +3, TimeFrame.EndOfCombat, 1, true);
        }

        public override void ApplyFX2()
        {
            if (targetController)
            {
                    charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Cloaked, skillModel.timeFrame, skillModel.castTime);
            }
        }

        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {
            str1 = "Req: Hp <40%";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Gain<style=States> Cloaked</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str3 = "+3 Dodge until eoc";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX4()
        {
 
        }

        public override void ApplyVFx()
        {

        }

        public override void ApplyMoveFx()
        {

        }

        public override void PopulateAITarget()
        {

        }

    }
}