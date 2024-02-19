using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class BehindMilady : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.BehindMilady;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
		//+2 haste to each Lioness per Lion in party	
	
     
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Lionesses: +2 Haste per Lion in party";
            PassiveSkillService.Instance.descLines.Add(str0);

        }
    }


}
