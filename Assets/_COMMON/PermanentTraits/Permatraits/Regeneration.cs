using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Common
{
    public class Regeneration : PermaTraitBase
    {
        // +1 hp regen in combats per round
        //Sicknesses recover faster(means if its supposed to be 10 days long, its now 5 days - halved)

        public override PermaTraitName permaTraitName => PermaTraitName.Regeneration;
        public override void ApplyTrait(CharController charController)
        {




        }
        public override void EndTrait()
        {
            base.EndTrait();
        }

    }
}
