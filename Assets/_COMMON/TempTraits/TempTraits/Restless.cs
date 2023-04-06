using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



namespace Common
{
    public class Restless : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Restless;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}