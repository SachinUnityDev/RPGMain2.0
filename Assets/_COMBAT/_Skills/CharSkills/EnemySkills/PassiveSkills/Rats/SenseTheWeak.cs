using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    public class SenseTheWeak : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.SenseTheWeak;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        public override void ApplyFX()
        {
            
        }
        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Scratch: +40% dmg vs Nausea";
            PassiveSkillService.Instance.descLines.Add(str0);
            str1 = "Nausea: +40% dmg vs Rat Bite Fever";
            PassiveSkillService.Instance.descLines.Add(str1);            
        }
    }



}

