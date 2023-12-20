using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Strong : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Strong;
       // Always triggers max Melee Dmg
        public override void OnApply()
        {
        }

        public override void EndTrait()
        {
            base.EndTrait();  
        }
    }
}