using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class NothingToLose : PermTraitBase
    {
        //  immune to negative mental traits

        CharController charController; 
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.NothingToLose;
        public override void ApplyTrait(CharController _charController)
        {
                



        }


    }
}

