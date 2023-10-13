﻿using System.Collections;
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
                    if (GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Backward, 1))
                    { 
                        chance++;
                    }
                }
            }

            if(chance >= 3)
            {
                RegainAP(); 
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

        //public override void SkillEnd()
        //{
        //    base.SkillEnd();       
          
        //}
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