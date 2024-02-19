using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Feline : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.Feline;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
		
        //Bleed time is reduced by 1 rd or neglect LowBleed
//+1 hp regen
//+3 dodge when hp < 40%"	
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Bleed time reduced";
            PassiveSkillService.Instance.descLines.Add(str0);
            str1 = "+1 Hp Regen";            
            PassiveSkillService.Instance.descLines.Add(str1);
			  str2 = "+3 Dodge when Hp < 40%";            
            PassiveSkillService.Instance.descLines.Add(str2);
        }
    }


}
