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

        public override void SkillSelected()
        {
            base.SkillSelected();
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
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.luck
                    , +6, false);
            }
        }
         // luck increase for skill use only   
        public override void ApplyFX1()
        {   
            if(targetController!= null)
            {
                if(targetController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT))
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                          , DamageType.Physical
                                          , skillModel.damageMod, skillModel.skillInclination, false, true);
                }
                else
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                          , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
                }
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
            str1 = $"Hits with +6<style=Attributes> Luck </style> on Bleeding targets";
            SkillService.Instance.skillModelHovered.AddDescLines(str1); 
        }

        public override void DisplayFX2()
        {

            str2 = $"+6<style=Attributes> Acc </style>if self HP < 40%";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);

        }

        public override void DisplayFX3()
        {
          
        }

        public override void DisplayFX4()
        {
        }

        public override void PostApplyFX()
        {
            base.PostApplyFX();          
            charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.luck
                    , -6, false);
        }

   
    }




}


//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.HeadToss;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.BrokenHorn;

//        public override PerkType perkType => PerkType.A3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => " Broken Horn";

//        private float _chance = 60f;
//        public override float chance { get => _chance; set => _chance = value; }      

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                .Find(t => t.skillName == skillName);
//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
//            && t.skillLvl == SkillLvl.Level0).WipeFX1;
//        }
//        public override void SkillSelected()
//        {
//            SkillController skillController = SkillService.Instance.currSkillMgr;
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            skillController.allSkillBases.Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level0).RemoveFX1();
//        }
//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//                StatData hpData = targetController.GetStat(StatsName.health); 


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
//        }

//        public override void RemoveFX2()
//        {
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
