using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class WetAndHappy : PermTraitBase
    {
        //Neglects negative specs of soaked status	
        //+3 morale when soaked
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.WetAndHappy;
        public override void ApplyTrait(CharController charController )
        {




        }

    }
}

