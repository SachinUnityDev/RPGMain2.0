using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using Quest;

namespace Combat
{
    public class UnBroken : PerkBase
    {
        public override PerkNames perkName => PerkNames.UnBroken;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "If full hp on cast,/n gains +20% Armor and +20 Elemental Res until EOC /n (If Morale 12, +40% and +40)";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.IgnorePain;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

       
        bool moraleChgesCasttime = false;

        float chgMin = 0;
        float chgMax = 0; 
      
        public override void ApplyFX1()
        {
            StatData hpStat = charController.GetStat(StatName.health);
            AttribData moraleStat = charController.GetAttrib(AttribName.morale);
            if (moraleStat.currValue == 12)
            {
                skillModel.timeFrame = TimeFrame.EndOfCombat; 
                skillModel.castTime = 1;
                moraleChgesCasttime = true;
            }

            if (hpStat.currValue == hpStat.maxLimit)
            {
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                    , AttribName.waterRes, 30f, skillModel.timeFrame, skillModel.castTime, true);
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                    , AttribName.earthRes, 20f, skillModel.timeFrame, skillModel.castTime, true);

             
                    AttribData armorMin = charController.GetAttrib(AttribName.armorMin);
                    AttribData armorMax = charController.GetAttrib(AttribName.armorMax);
                    float armorMinVal = armorMin.currValue;
                    float armorMaxVal = armorMin.currValue;
                    chgMin = armorMinVal * 0.6f;
                    chgMax = armorMaxVal * 0.6f; 

                    charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                                    , AttribName.armorMin, chgMin, skillModel.timeFrame, skillModel.castTime, true);
                    charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                                    , AttribName.armorMax, chgMax, skillModel.timeFrame, skillModel.castTime, true);

                 
            }            
        }
        public override void DisplayFX1()
        {
            //if (!moraleChgesCasttime)
            //{
            //    str0 = $"If full HP,+80% <style=Attributes> Armor </style>, {skillModel.castTime} rds";
            //    SkillService.Instance.skillModelHovered.AddDescLines(str0);
            //}
            //else
            //{
            //    str0 = $"If full HP,+80% <style=Attributes> Armor </style>, EOC";
            //    SkillService.Instance.skillModelHovered.AddDescLines(str0);
            //}
            
            str1 = $"If full HP, +40%<style=Water> Water Res </style>and +40%<style=Earth> Earth Res </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"If Morale 12, +40%<style=Water> Water Res </style> and +20%<style=Earth> Earth Res </style>, until eoq";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);      
        }
        public override void ApplyFX2()
        {
           
        }

        public override void ApplyFX3()
        {
          
        }

        public override void ApplyVFx()
        {
            
        }

        public override void ApplyMoveFX()
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

