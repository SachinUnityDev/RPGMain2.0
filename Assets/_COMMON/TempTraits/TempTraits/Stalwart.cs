using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Stalwart : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Stalwart;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnEnd()
        {
            
        }
    }
}
