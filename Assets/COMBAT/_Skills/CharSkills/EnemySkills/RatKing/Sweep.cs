using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class Sweep : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillNames skillName => SkillNames.Sweep;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Sweep";

        private float _chance = 40f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> firstRowChar = new List<DynamicPosData>();

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear(); firstRowChar.Clear();
            firstRowChar = GridService.Instance.GetFirstRowChar(CharMode.Enemy);

            if (firstRowChar.Count > 0)
            {
                foreach (var dynaChar in firstRowChar)
                {
                    skillModel.targetPos.Add(new CellPosData(dynaChar.charMode, dynaChar.currentPos));
                    CombatService.Instance.mainTargetDynas.Add(dynaChar);

                }
            }
        }

        public override void ApplyFX1()
        {
            if (chance.GetChance())
                targetController.damageController.ApplyLowBleed(CauseType.CharSkill, (int)skillName
                                 , charController.charModel.charID);
        }

        public override void ApplyFX2()
        {
            if (30f.GetChance())
                targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, TempTraitName.RatBiteFever);
        }

        public override void ApplyFX3()
        {

        }

        public override void ApplyVFx()
        {

        }

        public override void ApplyMoveFx()
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

        public override void PopulateAITarget()
        {
            SkillService.Instance.currentTargetDyna = CombatService.Instance.mainTargetDynas[0];
        }
    }
}