using System.Collections;
using System.Collections.Generic;
using UnityEngine;



 namespace Common
{
    public class Frail : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Frail;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.vigor, -3, TimeFrame.Infinity, -1, true);
        }

        public override void OnTraitEnd()
        {
            
        }
    }


}

