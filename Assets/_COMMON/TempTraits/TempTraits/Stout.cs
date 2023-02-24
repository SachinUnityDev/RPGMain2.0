using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Stout : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Stout;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnEnd()
        {
            
        }
    }
}
