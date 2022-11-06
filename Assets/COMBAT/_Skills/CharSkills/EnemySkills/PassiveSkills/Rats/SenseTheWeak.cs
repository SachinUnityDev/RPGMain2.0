using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class SenseTheWeak : PassiveSkillBase
    {
        public override PassiveSkillNames passiveSkillName => PassiveSkillNames.SenseTheWeak;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        private SkillNames _skillName;
        public override SkillNames skillName { get => _skillName; set => _skillName = value; }


        public override void AddTargetPos()
        {
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

        public override void PostApplyFX()
        {
        }

        public override void PreApplyFX()
        {
        }
    }



}

