using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class NeedSomeSpace : PermTraitBase
    {
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.NeedSomeSpace; 
        public  override void ApplyTrait(CharController charController )
        {

           // to be code
            // +2 morale if single in a row


        }



    }



}

