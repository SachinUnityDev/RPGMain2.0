using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Telekinesis : SkillBase
    {
        public override CharNames charName { get; set; }

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        public override string desc => " Telekenesis";

        public override float chance { get; set; }

        public override void ApplyFX1()
        {
           
        }

        public override void ApplyFX2()
        {
         
        }

        public override void ApplyFX3()
        {
          
        }

        public override void ApplyMoveFx()
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

        public override void PopulateAITarget()
        {
            
        }

        public override void PopulateTargetPos()
        {
          
        }
    }
}