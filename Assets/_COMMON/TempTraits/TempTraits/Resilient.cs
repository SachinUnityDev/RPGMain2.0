using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Resilient : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Resilient; 

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }

}

