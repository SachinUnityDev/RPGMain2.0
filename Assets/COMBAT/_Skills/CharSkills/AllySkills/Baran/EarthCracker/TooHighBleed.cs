using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class TooHighBleed : PerkBase
    {
        public override PerkNames perkName => PerkNames.TooHighBleed;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.None };

        public override string desc => "Hits initial target only(No Earth dmg)/n 60% High Bleed/n 3 --> 2 rds cd ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 60f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();

            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3;

        }


        public override void SkillSelected()
        {
            base.SkillSelected();

            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX3;
        }
        public override void BaseApply()
        {
            base.BaseApply();
            skillModel.cd = 2; 
        }

        public override void ApplyFX1()
        {
            if (_chance.GetChance())
            {
                CharStatesService.Instance
                    .ApplyCharState(targetGO, CharStateName.BleedHighDOT
                                     , charController, CauseType.CharSkill, (int)skillName);
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
            str0 = $"{chance}%<style=Bleed> High Bleed </style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
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

        public override void PostApplyFX()
        {
           
        }

        public override void PreApplyFX()
        {
           
        }
    }
}


//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.EarthCracker;

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.TooHighBleed;

//        public override PerkType perkType => PerkType.A2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Too high bleed";

//        private float _chance = 60f;
//        public override float chance { get => _chance; set => _chance = value; }


//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                           .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            skillModel.cd = 2; 
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
//            && t.skillLvl == SkillLvl.Level0).WipeFX2;
//            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
//          && t.skillLvl == SkillLvl.Level0).WipeFX3;

//        }    

//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);
//            skillController.allSkillBases.Find(t => t.skillName == skillName
//                                            && t.skillLvl == SkillLvl.Level0).RemoveFX2(); // earth dmg
//            skillController.allSkillBases.Find(t => t.skillName == skillName
//                                            && t.skillLvl == SkillLvl.Level0).RemoveFX3(); // low bleed -> high bleed
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;

//        }

//        public override void BaseApply()
//        {
//            targetGO = GridService.Instance.GetInSameLaneOppParty
//                        (new CellPosData(CharMode.Ally, GridService.Instance.GetDyna4GO(charGO).currentPos))
//                             [0].charGO;
//            targetController = targetGO.GetComponent<CharController>();
//        }


//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            if (_chance.GetChance())
//            {
//                CharStatesService.Instance.SetCharState(targetGO, CharStateName.BleedHighDOT);
//            }

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
//            str1 = $"{_chance}%<style=Bleed> High Bleed</style>";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
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

