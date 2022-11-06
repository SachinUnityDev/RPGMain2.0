using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CrackedMind : PermTraitBase
    {
        //Plus 30% chance of getting negative mental traits - on Temp traits added 

        public override TraitBehaviour traitBehaviour => TraitBehaviour.Negative;

        public override PermanentTraitName permTraitName => PermanentTraitName.CrackedMind;
        public override void ApplyTrait(CharController charController )
        {
            // action temp traits .... check if negative mental trait

        }

    }

}

