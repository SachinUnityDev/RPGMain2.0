using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class KillTheSnared : PermTraitBase
{
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.SnaredSlayer;
        public override void ApplyTrait(CharController charController)
        {


        }
    }




}

