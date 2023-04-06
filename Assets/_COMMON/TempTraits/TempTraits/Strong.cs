using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Strong : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Strong;
       // Always triggers max Melee Dmg
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
           
        }
    }
}