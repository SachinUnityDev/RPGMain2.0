using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class FearOfTheDead : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FearOfTheDead;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }
}