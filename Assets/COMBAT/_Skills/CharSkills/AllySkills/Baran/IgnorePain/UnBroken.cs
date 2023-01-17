using System.Collections;
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
            StatData hpStat = charController.GetStat(StatsName.health);
            StatData moraleStat = charController.GetStat(StatsName.morale);
            if (moraleStat.currValue == 12 && !subscribed)
            {
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.waterRes, 40f, false);
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, 20f, false);
                QuestEventService.Instance.OnEOQ += EndOnEOQ;
                subscribed = true;
            }
            else if (hpStat.currValue == hpStat.maxLimit)
            {
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.waterRes, 40f, false);
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, 20f, false);

             
                    StatData statData = charController.GetStat(StatsName.armor);
                    float armorMin = statData.minRange;
                    float armorMax = statData.maxRange;
                    chgMin = statData.minRange * 0.8f;
                    chgMax = statData.maxRange * 0.8f;

                    charController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charID
                                                    , StatsName.armor, chgMin, chgMax);
                

                resIncr = true; 
            }            
        }

        void EndOnEOQ()
        {
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.waterRes, -40f, false);
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, -20f, false);
            QuestEventService.Instance.OnEOQ -= EndOnEOQ;

            subscribed = false; 

        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            if (resIncr)
            {
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.waterRes, -40f, false);
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, -20f, false);
                targetController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charID
                                               , StatsName.armor, -chgMin, -chgMax);

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
//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.IgnorePain;

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.NoPainForLife;

//        public override PerkType perkType => PerkType.B1;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }


//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                         .Find(t => t.skillName == skillName);
//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            // skillModel Updates
//            skillModel.damageMod = 120f;
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            Debug.Log("skill hovered" + perkName);
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillHovered += DisplayFX3;

//            SkillService.Instance.SkillWipe += skillController.allPerkBases.Find(t => t.skillName == skillName
//            && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).WipeFX1;
//        }

//        public override void SkillSelected()
//        {
//            SkillController skillController = SkillService.Instance.currSkillMgr;

//            skillController.allPerkBases.Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1
//            && t.state == PerkSelectState.Clicked).RemoveFX1();
//            Debug.Log("pressurised water is selected" + skillController.allPerkBases.Find(t => t.skillName == skillName
//                       && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).perkName);

//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            SkillService.Instance.SkillApply += ApplyFX3;
//            SkillService.Instance.SkillApply += ApplyFX4;
//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            targetController.dmgController.ApplyDamage(CharacterService.Instance.GetCharCtrlWithName(charName)
//                             , DamageType.MagicalWater, 120f, false);
//        }

//        public override void ApplyFX2()
//        {
//        }

//        public override void ApplyFX3()
//        {
//        }

//        public override void ApplyFX4()
//        {
//        }


//        public override void DisplayFX1()
//        {
//        }

//        public override void DisplayFX2()
//        {
//        }

//        public override void DisplayFX3()
//        {
//        }

//        public override void DisplayFX4()
//        {
//        }

//        public override void PostApplyFX()
//        {
//        }

//        public override void PreApplyFX()
//        {
//        }

//        public override void RemoveFX1()
//        {
//            SkillService.Instance.SkillApply -= ApplyFX1;

//        }

//        public override void RemoveFX2()
//        {
//            SkillService.Instance.SkillApply -= ApplyFX2;

//        }

//        public override void RemoveFX3()
//        {
//        }

//        public override void RemoveFX4()
//        {
//        }

//        public override void SkillEnd()
//        {
//        }



//        public override void Tick()
//        {
//            if (roundEnd >= 2)
//                SkillEnd();
//            roundEnd++;
//        }

//        public override void WipeFX1()
//        {

//        }

//        public override void WipeFX2()
//        {
//        }

//        public override void WipeFX3()
//        {
//        }

//        public override void WipeFX4()
//        {
//        }


