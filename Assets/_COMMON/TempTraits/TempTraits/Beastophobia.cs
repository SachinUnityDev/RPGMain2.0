using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Beastophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Beastphobia;
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnTraitEnd()
        {
            
        }
    }
}

