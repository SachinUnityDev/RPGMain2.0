using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Common
{
    public class Regeneration : PermTraitBase
    {
        // +1 hp regen in combats per round
        //Sicknesses recover faster(means if its supposed to be 10 days long, its now 5 days - halved)

        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.Regeneration;
        public override void ApplyTrait(CharController charController)
        {




        }

    }
}
