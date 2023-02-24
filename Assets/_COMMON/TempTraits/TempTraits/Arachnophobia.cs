using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Arachnophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName =>  TempTraitName.Arachnophobia;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }
}