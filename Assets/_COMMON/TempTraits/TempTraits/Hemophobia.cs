using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Hemophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Hemophobia;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }
}