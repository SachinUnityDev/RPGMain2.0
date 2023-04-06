using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class FatesChild : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FatesChild;

        public override void OnApply(CharController charController)
        {
            //+2 Luck
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.luck, 2, TimeFrame.Infinity, -1, true);
        }

        public override void OnTraitEnd()
        {
            
        }
    }

}

