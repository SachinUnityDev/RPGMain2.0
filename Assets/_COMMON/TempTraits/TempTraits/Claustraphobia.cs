using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Claustraphobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Claustrophobia;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            // +60% fortitude loss in the sewers 
        }
        public override void OnEnd()
        {
            // unsubscribe
        }
    }

}

