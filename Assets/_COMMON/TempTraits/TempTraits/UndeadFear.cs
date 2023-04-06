using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class UndeadFear : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.UndeadFear;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnTraitEnd()
        {
            
        }
    }
}
