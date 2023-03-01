using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CrackedMind : PermTraitBase
    {
        //Plus 30% chance of getting negative mental traits - on Temp traits added 

        public override traitBehaviour traitBehaviour => traitBehaviour.Negative;

        public override PermTraitName permTraitName => PermTraitName.Shapeshifter;
        public override void ApplyTrait(CharController charController )
        {
            // action temp traits .... check if negative mental trait

        }

    }

}

