using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Overslept : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Overslept;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnEnd()
        {
            
        }
    }
}
