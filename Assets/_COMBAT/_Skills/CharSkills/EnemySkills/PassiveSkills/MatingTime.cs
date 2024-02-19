using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class MatingTime : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.MatingTime;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
		//+2 Morale to each Lion per Lioness in party	
	
     
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Lions: +2 Morale per Lioness in party";
            PassiveSkillService.Instance.descLines.Add(str0);

        }
    }


}
