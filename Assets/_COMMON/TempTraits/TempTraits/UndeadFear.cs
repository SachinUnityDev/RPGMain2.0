using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class UndeadFear : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.UndeadFear;

        public override void OnApply()
        {
            
        }
        public override void EndTrait()
        {
            base.EndTrait();
        }
    }
}
