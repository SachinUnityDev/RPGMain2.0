using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Common
{
    public class WhoDarestoDare : PermTraitBase
    {
        //  +2 morale when taunt mode
        //+1 morale vs axe and mace wielding enemies(Forest Bandit, Ogre, Boudingo Bully - more after prelaunch)
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.Derring;
        public override void ApplyTrait(CharController charController)
        {

        }



    }




}

