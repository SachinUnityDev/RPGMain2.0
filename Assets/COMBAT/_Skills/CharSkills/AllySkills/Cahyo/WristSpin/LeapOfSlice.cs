using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;


namespace Combat
{
    public class LeapOfSlice : PerkBase
    {
        public override PerkNames perkName => PerkNames.LeapToSlice;

        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.DeepCut };

        public override string desc => "(PR: Deep Cut) /n * Can hit anyone from any spot. /n *now 7 stamina cost /n*+25% damage if target is at pos. 5,6,7. /n *+50% dmg if both self and target at 7.";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();

            for (int i = 1; i < 8; i++)
            {
                CellPosData cell = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cell.pos, cell.charMode);

                if(dyna != null)
                {
                    skillModel.targetPos.Add(cell); 
                }
            }
        }

        public override void SkillSelected()
        {
            base.SkillSelected();

            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1; 

        }

        public override void BaseApply()
        {
            base.BaseApply();

            skillModel.staminaReq = 7;

        }
        public override void ApplyFX1()
        {
            // if positions 5 67 
            // if position 4
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO); 

            if (targetDyna.currentPos == 5 || targetDyna.currentPos == 6 || targetDyna.currentPos == 7)
            {
                if(currDyna.currentPos ==7 && targetDyna.currentPos ==7)
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical
                                               , skillModel.damageMod +50f, false);
                }
                else
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical
                                             , skillModel.damageMod + 25f, false);
                }             
            }        
            else
            {
                targetController.damageController.ApplyDamage(charController,CauseType.CharSkill, (int)skillName, DamageType.Physical
                                               , skillModel.damageMod, false);
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
            //Can hit anyone from any spo
            str0 = $"Can hit anyone from any pos";
            SkillService.Instance.skillCardData.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $" if target at 5,6,7, +25% <style=Physical>Physical</style> ";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
            str2 = $" if striker & target at 7, +50% <style=Physical>Physical</style> ";
            SkillService.Instance.skillCardData.descLines.Add(str2);
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


//        public override CharNames charName => CharNames.Cahyo;

//        public override SkillNames skillName => SkillNames.WristSpin;
//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.LeapToSlice;

//        public override PerkType perkType => PerkType.B3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.DeepCut };

//        public override string desc => "LeapOfSlice ";
//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillHovered()
//        {
//            SkillInit();

//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;

//            SkillService.Instance.SkillHovered += DisplayFX1;

//            //SkillService.Instance.SkillWipe += skillController.allSkillBases
//            //              .Find(t => t.skillName == skillName).WipeFX2;
//        }

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                             .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            skillModel.staminaReq = 7; 
//        }

//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);
//            skillModel.castPos.Clear();
//            skillModel.castPos.AddRange(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
//            skillModel.targetPos.Clear();
//            for (int i = 1; i < 8; i++)
//            {
//                CellPosData cell = new CellPosData(CharMode.Enemy, i);
//                skillModel.targetPos.Add(cell); 
//            }


//            if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//                return;

//           // skillController.allPerkBases.Find(t => t.skillName == skillName).RemoveFX2();
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            GridService.Instance.HLTargetTiles(skillModel.targetPos); // overriden by ne


//        }
//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            CombatEventService.Instance.OnEOR += Tick;
//        }

//        public override void ApplyFX1()
//        {
//              // override the damage of the base skill  
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



//        public override void RemoveFX1()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RemoveFX2()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RemoveFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RemoveFX4()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void SkillEnd()
//        {
//            throw new System.NotImplementedException();
//        }



//        public override void Tick()
//        {
//            throw new System.NotImplementedException();
//        }



//        public override void DisplayFX1()
//        {

//        }

//        public override void DisplayFX2()
//        {

//        }

//        public override void DisplayFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void DisplayFX4()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX1()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX2()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX4()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void PreApplyFX()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void PostApplyFX()
//        {
//            throw new System.NotImplementedException();
//        }

