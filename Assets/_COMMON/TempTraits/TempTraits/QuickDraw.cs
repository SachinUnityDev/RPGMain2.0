using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class QuickDraw : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Quickdraw;
       // When First Blood: +20% Dmg until eoc(dmg attribuıte) When First Blood: +3 Acc until eoc
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnTraitEnd()
        {
            
        }
    }
}
