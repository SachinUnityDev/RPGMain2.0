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

