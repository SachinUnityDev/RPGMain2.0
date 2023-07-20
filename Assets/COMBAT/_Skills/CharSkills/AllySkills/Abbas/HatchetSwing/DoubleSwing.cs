using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class DoubleSwing : PerkBase
    {
        public override PerkNames perkName => PerkNames.DoubleSwing;

        public override PerkType perkType => PerkType.A1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Double Swing";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level1; 

        public override float chance { get;set; }

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

