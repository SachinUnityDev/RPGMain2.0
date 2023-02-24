using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Insane : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Insane;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }
}