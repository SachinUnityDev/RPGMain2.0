using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
namespace Combat
{
    public class BloodyAxe : PerkBase
    {
        public override PerkNames perkName => PerkNames.BloodyAxe;
        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.TooHighBleed };

        public override string desc => "(PR: B1) /n + 50 % dmg on Bleeding targets /n Can hit any target";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();

            for (int i = 1; i < 8; i++)
            {                
                    CellPosData cellPosData = new CellPosData(charController.charModel.charMode.FlipCharMode(), i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                    }                
            }

        }

        public override void BaseApply()
        {
            base.BaseApply();
            if (CharStatesService.Instance.HasCharDOTState(targetGO, CharStateName.BleedHighDOT))
            {
                targetController.damageController
                      .ApplyDamage(charController, CauseType.CharSkill, (int) skillName, DamageType.Physical, 50f, false);
            }

        }
        public override void ApplyFX1()
        {
         
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
            str0 = $"50%<style=Physical> Physical </style>on Bleeding target";
            SkillServiceView.Instance.skillCardData.descLines.Add(str0);

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

//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.EarthCracker;

//        public override SkillLvl skillLvl => SkillLvl.Level2;
//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.BloodyAxe;

//        public override PerkType perkType => PerkType.B2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.TooHighBleed };

//        public override string desc => "Bloody Axe";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                           .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            skillModel.targetPos.Clear(); 

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
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);
//            skillController.allSkillBases.Find(t => t.skillName == skillName
//                                            && t.skillLvl == SkillLvl.Level0).RemoveFX1(); // physical Dmg 
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;

//        }

//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//        }

//        public override void ApplyFX1()
//        {

//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

//            if (CharStatesService.Instance.HasCharState(targetGO,CharStateName.BleedLowDOT)||
//                CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedMedDOT)||
//                CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedHighDOT))
//            {
//                targetController.dmgController.ApplyDamage(charController, DamageType.Physical
//                    , skillModel.damageMod +50f, false);
//            }
//            else
//            {
//                targetController.dmgController.ApplyDamage(charController, DamageType.Physical
//                 , skillModel.damageMod, false);
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