using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

  //  More chance to tame animals(+30% - but we dont show this to player)
    
    public class Beastmancer : PermTraitBase
    {
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.Beastmancer;
        public override void ApplyTrait(CharController charController)
        {
           CharModel _charModel =  charController.charModel;
            Debug.Log("perma trait applied beastmancer ");

            _charModel.tameAnimalsStrength += 30.0f; 
        }

    }


}


