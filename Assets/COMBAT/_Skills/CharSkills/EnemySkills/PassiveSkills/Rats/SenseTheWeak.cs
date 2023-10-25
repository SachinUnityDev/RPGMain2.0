using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    public class SenseTheWeak : PassiveSkillBase
    {
        public override PassiveSkillNames passiveSkillName => PassiveSkillNames.SenseTheWeak;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        public override void ApplyFX()
        {
            
        }
    }



}

