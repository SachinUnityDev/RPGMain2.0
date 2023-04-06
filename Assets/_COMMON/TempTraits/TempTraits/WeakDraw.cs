using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class WeakDraw : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Weakdraw;
        //Always triggers min Ranged Physical Dmg
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}