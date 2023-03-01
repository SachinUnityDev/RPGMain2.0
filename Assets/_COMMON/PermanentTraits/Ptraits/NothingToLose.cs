using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class NothingToLose : PermTraitBase
    {
        //  immune to negative mental traits

        CharController charController; 
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.NothingToLose;
        public override void ApplyTrait(CharController _charController)
        {
                



        }


    }
}

