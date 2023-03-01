using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SidedWithTheWeak : PermTraitBase
    {
        //if enemy is higher in numbers, +2 luck 
        //    if enemy is higher in numbers, +2 initiative
        public override PermTraitName permTraitName => PermTraitName.SidedWithTheWeak;
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override void ApplyTrait(CharController charController )
        {
         



        }


    }


}

