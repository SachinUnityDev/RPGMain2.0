using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class SoulOfTheTamboti : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.SoulOfTheTamboti;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Soul of Tamboti";

        private float _chance = 0f;
        public override float chance { get; set; }
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        public override void ApplyFX1()
        {
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
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

        public override void RemoveFX1()
        {
        }

        public override void RemoveFX2()
        {
        }

        public override void RemoveFX3()
        {
        }



        public override void SkillEnd()
        {
        }

        public override void Tick()
        {
        }

        public override void WipeFX1()
        {
        }

        public override void WipeFX2()
        {
        }

        public override void WipeFX3()
        {
        }

        public override void WipeFX4()
        {
        }

        public override void PopulateTargetPos()
        {

        }

        public override void ApplyVFx()
        {

        }

        public override void ApplyMoveFx()
        {

        }

        public override void PopulateAITarget()
        {

        }
    
    }
}