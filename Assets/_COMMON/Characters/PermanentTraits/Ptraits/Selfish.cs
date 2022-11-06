using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace Common
{
    public class Selfish :PermTraitBase
    {
        // -3 morale if not single in row
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Negative;

        public override PermanentTraitName permTraitName => PermanentTraitName.Selfish;
        public override void ApplyTrait(CharController charController )
        {

        }



    }



}

