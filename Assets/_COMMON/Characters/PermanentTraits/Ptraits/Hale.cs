using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class Hale : PermTraitBase
    {
        // immune to sickness traits   suffers only low poison - not medium or high
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.Hale; 
        public override void ApplyTrait(CharController charController)
        {

        }


  



    }





}
