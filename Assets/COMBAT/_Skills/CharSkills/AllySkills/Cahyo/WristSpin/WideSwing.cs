using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class WideSwing : PerkBase
    {
        public override PerkNames perkName => PerkNames.WideSwing;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "100% --> 80% dmg /n If on pos 1, hit pos 1 /n If on pos 2/3, hit pos 1+2/3 /n ";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();

            if (currDyna.currentPos == 1)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                skillModel.targetPos.Add(cellPosData);
            }
            else if (currDyna.currentPos == 2)
            {

                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                skillModel.targetPos.Add(cellPosData);
                CellPosData cellPosData2 = new CellPosData(CharMode.Enemy, 2);
                skillModel.targetPos.Add(cellPosData2);

            }
            else if (currDyna.currentPos == 3)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                skillModel.targetPos.Add(cellPosData);
                CellPosData cellPosData2 = new CellPosData(CharMode.Enemy, 3);
                skillModel.targetPos.Add(cellPosData2);
            }

            foreach (CellPosData cell in skillModel.targetPos)
            {

                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                CombatService.Instance.mainTargetDynas.Add(dyna);
            }

        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.damageMod = 80f;
        }
        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController
                                                .allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
            SkillService.Instance.SkillFXRemove += skillController
                                              .allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;
        }

        public override void ApplyFX1()
        {
            if (CombatService.Instance.mainTargetDynas.Count > 0)
                CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>().damageController
                    .ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical, skillModel.damageMod
                    , skillModel.skillInclination));
        }
        public override void ApplyFX2()
        {
            chance = 50f; // bleed chance
            if (targetController && chance.GetChance())
                if (CombatService.Instance.mainTargetDynas.Count > 0)
                    CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
                                    .charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                             , charController.charModel.charID, CharStateName.BleedLowDOT));
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
            str0 = "If on pos 1: Hit 1, if on 2, hit 1+2, if on 3, hit 1+3";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
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

        public override void InvPerkDesc()
        {
            perkDesc = "100% -> 80%<style=Physical> Physical </style>Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "If on pos 1: Hit 1, if on 2, hit 1+2, if on 3, hit 1+3";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}

