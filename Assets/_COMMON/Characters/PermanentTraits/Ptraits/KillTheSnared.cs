using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class KillTheSnared : PermTraitBase
{
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.KillTheSnared;
        public override void ApplyTrait(CharController charController)
        {


        }
    }




}

