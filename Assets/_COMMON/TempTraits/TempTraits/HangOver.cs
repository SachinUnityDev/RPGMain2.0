using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class HangOver : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Hangover;

        public override void OnApply()
        {
            
        }
        public override void EndTrait()
        {
            base.EndTrait();
        }
    }
}
