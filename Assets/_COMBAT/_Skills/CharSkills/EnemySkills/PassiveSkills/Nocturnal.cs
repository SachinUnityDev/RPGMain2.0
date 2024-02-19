using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Nocturnal : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.Nocturnal;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
		
		//+2 Acc at night 
		//+2 Dodge in Sewers and Cave Catacombs Desert Cave
			
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "+2 Acc at night";
            PassiveSkillService.Instance.descLines.Add(str0);
            str1 = "+2 Dodge in Sewers and Cave Catacombs Desert Cave";            
            PassiveSkillService.Instance.descLines.Add(str1);
        }
    }


}
