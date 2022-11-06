using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SmellsLikeHome : PermTraitBase
    {
        // +1 stamina regen in Rainforest, Jungle, Savannah
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.SmellsLikeHome;
        public override void ApplyTrait(CharController charController)
        {

          


        }


    }


}

