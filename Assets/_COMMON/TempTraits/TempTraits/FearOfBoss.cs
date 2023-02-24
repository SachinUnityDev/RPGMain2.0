using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class FearOfBoss : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FearOfBoss;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
        }
    }
}