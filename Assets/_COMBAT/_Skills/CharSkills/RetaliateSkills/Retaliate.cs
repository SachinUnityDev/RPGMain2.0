using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Retaliate : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Retaliate;
        public override string desc => "this is Retaliate";
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
                AnyWithCharMode(CharMode.Enemy); 
        }

        public override void ApplyFX1()
        {
            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                             , skillModel.dmgType[0], skillModel.damageMod);
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

        public override void ApplyVFx()
        {
        }

        public override void PopulateAITarget()
        {

        }

        public override void ApplyMoveFx()
        {

        }
    }
}