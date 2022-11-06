using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class LoreReader :PermTraitBase
    {
        // +30% exp from lorestones

        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive; 
        public override PermanentTraitName permTraitName => PermanentTraitName.LoreReader;
        public override void ApplyTrait(CharController charController )
        {


        }


    }


}

