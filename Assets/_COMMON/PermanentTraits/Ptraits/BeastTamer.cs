using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

  //  More chance to tame animals(+30% - but we dont show this to player)
    
    public class BeastTamer : PermaTraitBase
    {
        public override PermaTraitName permTraitName => PermaTraitName.BeastTamer;
        public override void ApplyTrait(CharController charController)
        {
           CharModel _charModel =  charController.charModel;
            Debug.Log("perma trait applied beastmancer ");

            _charModel.tameAnimalsStrength += 30.0f;
            CharService.Instance.OnPartyLocked += OnPartyLocked; 
        }

        void OnPartyLocked()
        {



        }


    }


}


