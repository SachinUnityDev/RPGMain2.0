using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class BrokenHorn : PerkBase
    {
        public override PerkNames perkName => PerkNames.BrokenHorn;

        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "If self hp< 40%, +60% dmg /n +60% dmg on Bleeding target /n 5 --> 7 Stamina cost";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.HeadToss;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        int buffID = -1; 
        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }

        public override void BaseApply()
        {
            base.BaseApply();
            skillModel.staminaReq = 7;

            StatData hpData = charController.GetStat(StatName.health);
            float percentHP = hpData.currValue / hpData.maxLimit;
            if (percentHP < 0.4f)
            {
              buffID = charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.luck
                    , +6, TimeFrame.Infinity, -1,true);
            }
        }
         // luck increase for skill use only   
        public override void ApplyFX1()
        {   
            if(targetController!= null)
            {
                if(targetController.charStateController.HasCharState(CharStateName.Bleeding))
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                          , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination, false, true);     
                }
                else
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                          , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
                }
                charController.buffController.RemoveBuff(buffID);
                buffID = -1;
            }
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
            str1 = "If self HP < 40%: Strike with +6 Luck";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "True Strike vs <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {          
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "If self HP < 40%: Strike with +6 Luck";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "True Strike vs <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Stm cost: 5 -> 7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}
