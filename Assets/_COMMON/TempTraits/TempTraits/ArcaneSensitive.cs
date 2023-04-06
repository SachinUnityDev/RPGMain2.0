using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class ArcaneSensitive : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.ArcaneSensitive;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}
