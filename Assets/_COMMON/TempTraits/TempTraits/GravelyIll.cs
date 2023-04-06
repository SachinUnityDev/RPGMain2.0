using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class GravelyIll : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.GravelyIll;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }


}

