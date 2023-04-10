using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CrackedMind : PermaTraitBase
    {
        //Plus 30% chance of getting negative mental traits - on Temp traits added 

        public override PermaTraitName permTraitName => PermaTraitName.Shapeshifter;
        public override void ApplyTrait(CharController charController )
        {
            // action temp traits .... check if negative mental trait

        }

    }

}

