using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Fragile : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Fragile;
        //Always triggers Min Armor
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}