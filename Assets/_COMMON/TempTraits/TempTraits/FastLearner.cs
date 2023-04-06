using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public class FastLearner : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FastLearner;
        //+30% Exp
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
          
        }
    }
}