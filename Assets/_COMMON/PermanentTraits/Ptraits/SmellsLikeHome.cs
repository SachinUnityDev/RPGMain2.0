using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SmellsLikeHome : PermTraitBase
    {
        // +1 stamina regen in Rainforest, Jungle, Savannah
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.SmellsLikeHome;
        public override void ApplyTrait(CharController charController)
        {

          


        }


    }


}

