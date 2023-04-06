using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Anemic : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Anemic;
        //When Bleeding: Receive +30% Dmg When Bleeding: -4 Luck
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnTraitEnd()
        {
            
        }
    }

}

