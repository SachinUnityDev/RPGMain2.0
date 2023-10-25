using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Rodent : PassiveSkillBase
    {
        public override PassiveSkillNames passiveSkillName => PassiveSkillNames.Rodent;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
           
        }

      
    }


}
