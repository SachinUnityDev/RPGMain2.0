using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

  //  More chance to tame animals(+30% - but we dont show this to player)
    
    public class Beastmancer : PermTraitBase
    {
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.Beastmancer;
        public override void ApplyTrait(CharController charController)
        {
           CharModel _charModel =  charController.charModel;
            Debug.Log("perma trait applied beastmancer ");

            _charModel.tameAnimalsStrength += 30.0f; 
        }

    }


}


