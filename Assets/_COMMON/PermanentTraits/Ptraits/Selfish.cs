using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace Common
{
    public class Selfish :PermTraitBase
    {
        // -3 morale if not single in row
        public override traitBehaviour traitBehaviour => traitBehaviour.Negative;

        public override PermTraitName permTraitName => PermTraitName.Selfcentred;
        public override void ApplyTrait(CharController charController )
        {

        }



    }



}

