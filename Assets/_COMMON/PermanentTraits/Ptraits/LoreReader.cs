using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class LoreReader :PermTraitBase
    {
        // +30% exp from lorestones

        public override traitBehaviour traitBehaviour => traitBehaviour.Positive; 
        public override PermTraitName permTraitName => PermTraitName.LoreReader;
        public override void ApplyTrait(CharController charController )
        {


        }


    }


}

