﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

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

        bool subscribed = false;
        bool resIncr = false;

        float chgMin = 0;
        float chgMax = 0; 
        
        //If he has full hp when casting Ignore Pain, 
        //    gives +40% Armor and +20 water and earth resistances 2 rds.
        //    If Morale 12, until EOC


  

        public override void ApplyFX1()
        {
            StatData hpStat = charController.GetStat(StatName.health);
            AttribData moraleStat = charController.GetAttrib(AttribName.morale);
            if (moraleStat.currValue == 12 && !subscribed)
            {
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.waterRes, 40f, false);
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, 20f, false);
                QuestEventService.Instance.OnEOQ += EndOnEOQ;
                subscribed = true;
            }
            else if (hpStat.currValue == hpStat.maxLimit)
            {
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.waterRes, 40f, false);
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, 20f, false);

             
                    AttribData statData = charController.GetAttrib(AttribName.armor);
                    float armorMin = statData.minRange;
                    float armorMax = statData.maxRange;
                    chgMin = statData.minRange * 0.8f;
                    chgMax = statData.maxRange * 0.8f;

                    charController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charID
                                                    , AttribName.armor, chgMin, chgMax);
                

                resIncr = true; 
            }            
        }

        void EndOnEOQ()
        {
            charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.waterRes, -40f, false);
            charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, -20f, false);
            QuestEventService.Instance.OnEOQ -= EndOnEOQ;

            subscribed = false; 

        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            if (resIncr)
            {
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.waterRes, -40f, false);
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, -20f, false);
                targetController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charID
                                               , AttribName.armor, -chgMin, -chgMax);

                resIncr = false;
            }
        }
        public override void DisplayFX1()
        {
            str0 = $"If full HP,+80% <style=Attributes> Armor </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
            str1 = $"If full HP, +40%<style=Water> Water Res </style>and +40%<style=Earth> Earth Res </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"If Morale 12, +40%<style=Water> Water Res </style> and +20%<style=Earth> Earth Res </style>, until eoq";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);      
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

