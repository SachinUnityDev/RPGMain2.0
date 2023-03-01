using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class Hale : PermTraitBase
    {
        // immune to sickness traits   suffers only low poison - not medium or high
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.Hale; 
        public override void ApplyTrait(CharController charController)
        {

        }


  



    }





}
