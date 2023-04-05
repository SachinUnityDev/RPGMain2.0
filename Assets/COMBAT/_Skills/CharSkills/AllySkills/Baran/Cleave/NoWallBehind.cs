using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class NoWallBehind : PerkBase
    {
        public override PerkNames perkName => PerkNames.NoWallBehind;

        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.Pusher };

        public override string desc => "(PR:A1)/n If can't Push, Root, 2 rds./n +3 Fortitude per rooted enemy";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            CombatService.Instance.mainTargetDynas.Clear();
            foreach (var cell in skillModel.targetPos)
            {
                CombatService.Instance.mainTargetDynas.Add(GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode));
            }
        }

        //public override void SkillHovered()
        //{
        //    base.SkillHovered();
        //    SkillService.Instance.SkillWipe += skillController.allPerkBases.Find(t => t.skillName == skillName
        //                 && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).WipeFX1;

        //}

        public override void SkillSelected()
        {
            base.SkillSelected();

            SkillService.Instance.SkillFXRemove += skillController.allPerkBases.Find(t => t.skillName == skillName
                                                        && t.skillLvl == SkillLvl.Level1
                                                        && t.state == PerkSelectState.Clicked).RemoveMoveFX;
        }
        public override void ApplyFX1()
        {
            foreach (var dyna in CombatService.Instance.mainTargetDynas)
            {
                if (dyna != null)
                {
                    if (!GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Backward, 1))
                    {
                        CharStatesService.Instance
                                      .ApplyCharState(targetGO, CharStateName.Rooted
                                     , charController, CauseType.CharSkill, (int)skillName);
                        chance++;
                    }
                }
            }
        }

        public override void ApplyFX2()
        {
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.fortitude
                ,+3 * chance);
        }

        public override void ApplyMoveFX()
        {
              
        }

        public override void SkillEnd()
        {
            base.SkillEnd();       
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                CharStatesService.Instance.RemoveCharState(dyna.charGO, CharStateName.Rooted);
            }
        }
        public override void ApplyFX3()
        {
        }

       

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>If Can't <style=Move>Push,</style> apply<style=States> Rooted </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
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

//        public override SkillNames skillName => SkillNames.Cleave;

//        public override SkillLvl skillLvl => SkillLvl.Level2;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.NoWallBehind;

//        public override PerkType perkType => PerkType.A2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.Pusher };

//        public override string desc => "no wall behind";
//        private float _chance = 50f;
//        public override float chance { get => _chance; set => _chance = value; }
//        List<DynamicPosData> targetDynas = new List<DynamicPosData>(); 

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                             .Find(t => t.skillName == skillName);

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

//        }

//        public override void SkillSelected()
//        {
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;

//        }

//        public override void Tick()
//        {
//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//            targetDynas.Clear();
//            foreach (var cell in skillModel.targetPos)
//            {
//                targetDynas.Add(GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode));
//            }

//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;


//            foreach (var dyna in targetDynas)
//            {
//                if (dyna != null)
//                {
//                    if(!GridService.Instance.gridMovement.CanTargetBePushed(dyna, MoveDir.Backward, 1))
//                    {  // cannot be pushed
//                        CharStatesService.Instance.SetCharState(dyna.charGO, CharStateName.Rooted);
//                        charController.ChangeStat(StatsName.fortitude, 3, 0, 0); 
//                    }
//                }
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
//            str1 = $"<style=States>Root </style>if can't Push";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {
//            str2 = $"+3 Fortitude for each-" ;
//            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
//            str3 = "<style=States>Rooted</style> target";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
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



//        public override void WipeFX1()
//        {
//            SkillService.Instance.SkillHovered -= DisplayFX1;
//        }

//        public override void WipeFX2()
//        {
//            SkillService.Instance.SkillHovered -= DisplayFX2;
//        }

//        public override void WipeFX3()
//        {
//        }

//        public override void WipeFX4()
//        {
//        }