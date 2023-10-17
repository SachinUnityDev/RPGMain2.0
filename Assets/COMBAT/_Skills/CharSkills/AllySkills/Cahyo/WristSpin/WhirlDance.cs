using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class WhirlDance : PerkBase
    {
        public override PerkNames perkName => PerkNames.Whirldance;

        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.WideSwing };

        public override string desc => "(PR: Wide Swing.) /n  Hits all enemies./n  now 2 rd cd.  6 stamina cost";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            CombatService.Instance.mainTargetDynas.Clear(); skillModel.targetPos.Clear();
             CombatService.Instance.mainTargetDynas.AddRange(GridService.Instance.GetAllByCharMode(CharMode.Enemy));
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                skillModel.targetPos.Add(new CellPosData(dyna.charMode, dyna.currentPos));
            }
        }

        public override void BaseApply()
        {
            base.BaseApply();
            skillModel.cd = 2;
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

