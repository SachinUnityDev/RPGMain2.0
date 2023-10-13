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
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    skillModel.targetPos.Add(cellPosData);
                }                
            }
        }
   
        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }
    
        public override void ApplyFX1()
        {
            if (targetController.charStateController.HasCharDOTState(CharStateName.BleedHighDOT))
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill,
                    (int)skillName, DamageType.Physical, (skillModel.damageMod + 50f), false);
            else
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical, (skillModel.damageMod), false);
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

    }
}
