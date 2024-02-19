using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class CaveLurker : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.CaveLurker;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {

        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;

			str1 = "+2 Wp, Haste and Stm Regen in Cave";
            PassiveSkillService.Instance.descLines.Add(str1);
			str2 = "Immune to<style=States>Rooted</style>";
            PassiveSkillService.Instance.descLines.Add(str2);

        }
    }


}
