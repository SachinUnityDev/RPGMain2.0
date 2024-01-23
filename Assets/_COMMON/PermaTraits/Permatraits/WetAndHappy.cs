using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class WetAndHappy : PermaTraitBase
    {
        //Neglects negative specs of soaked status	
        //+3 morale when soaked
        public override PermaTraitName permaTraitName => PermaTraitName.WetAndHappy;
        public override void ApplyTrait()
        {
        }

    }
}

