using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class DareDevil : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Daredevil;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }


}
