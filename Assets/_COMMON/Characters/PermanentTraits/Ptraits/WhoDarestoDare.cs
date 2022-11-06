using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Common
{
    public class WhoDarestoDare : PermTraitBase
    {
        //  +2 morale when taunt mode
        //+1 morale vs axe and mace wielding enemies(Forest Bandit, Ogre, Boudingo Bully - more after prelaunch)
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.WhoDarestoDare;
        public override void ApplyTrait(CharController charController)
        {

        }



    }




}

