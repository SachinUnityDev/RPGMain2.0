using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class DeepSearcher : PermTraitBase
    {
        // Curios in jungle and rainforests have 100% chance to give loot	
        //+1 loot from curios in jungle and rainforests
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.DeepSearcher;
        public override void ApplyTrait(CharController charController)
        {

        }
    }


}

