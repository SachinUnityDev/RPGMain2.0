using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Arachnid : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.Arachnid;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
		
        //   allImmunityBuffs.Add(charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
          //         , charID, CharStateName.Rooted,  timeFrame.Infinity, -1, true));

				   
				   //ignore Armor vs Poisoned
				   
				   // Retaliates when Dodges
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Attacks inflict -15 Earth Res on target";
            PassiveSkillService.Instance.descLines.Add(str0);
            str1 = "Webby Spit drains Stm";            
            PassiveSkillService.Instance.descLines.Add(str1);
        }
    }


}
