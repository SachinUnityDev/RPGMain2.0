using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class BloodThirsty : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Bloodthirsty; 
        //+30% Dmg vs Bleeding target	-1 Focus per Bleeding target	-2 Acc vs Bleeding target
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnEnd()
        {
            
        }
    }
}