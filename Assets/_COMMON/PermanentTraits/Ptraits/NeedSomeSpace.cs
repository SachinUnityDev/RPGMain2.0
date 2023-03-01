using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class NeedSomeSpace : PermTraitBase
    {
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.NeedSomeSpace; 
        public  override void ApplyTrait(CharController charController )
        {

           // to be code
            // +2 morale if single in a row


        }



    }



}

