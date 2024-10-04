using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class NoMatterWhoYouAre : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.NoMatterWhoYouAre;
        public override PerkType perkType => PerkType.B1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "No matter who you are !";        
        public override float chance { get; set; }
        bool isAPRewardGained = false;  
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnEOR1 -= ResetReward;
            CombatEventService.Instance.OnEOR1 += ResetReward;
        }

        void ResetReward(int rd)
        {
            isAPRewardGained = false;
        }

        public override void ApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.acc, +3
                , skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.dodge, +3
                , skillModel.timeFrame, skillModel.castTime, true);
        }

        public override void ApplyFX2()
        {
           if(CombatService.Instance.mainTargetDynas.Count ==1)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                       , charController.charModel.charID, CharStateName.Despaired);
        }
        public override void ApplyFX3()
        {
            AttribData moraleAttrib = charController.GetAttrib(AttribName.morale);
            if (moraleAttrib.currValue == 12 && !isAPRewardGained)        
            {
               charController.combatController.IncrementAP();
                isAPRewardGained = true;
            }
        }
        public override void DisplayFX1()
        {
            str0 = "+2 Dodge and Acc on self";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "Apply <style=States>Despaired</style> if enemy solo";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = "If Morale 12: Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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
            perkDesc = "+2 Dodge and Acc on self";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Apply <style=States>Despaired</style> if enemy solo";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "If Morale 12: Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }



}

