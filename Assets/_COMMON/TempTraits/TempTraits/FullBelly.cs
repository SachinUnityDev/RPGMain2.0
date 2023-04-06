using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class FullBelly : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FullBelly; 

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}