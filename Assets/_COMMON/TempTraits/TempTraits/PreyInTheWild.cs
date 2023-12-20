using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class PreyInTheWild : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.PreyInTheWild; 

        public override void OnApply()
        {
        }

        public override void EndTrait()
        {
            base.EndTrait();    
        }
    }
}
