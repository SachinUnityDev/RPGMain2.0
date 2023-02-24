using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables; 

namespace Common
{

    public class PotionAddict : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.PotionAddict;  

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }


}
