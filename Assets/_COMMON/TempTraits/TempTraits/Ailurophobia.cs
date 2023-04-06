using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Ailurophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Ailurophobia;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}