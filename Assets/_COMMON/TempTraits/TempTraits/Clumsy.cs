using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Clumsy : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Clumsy;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.acc, -3, TimeFrame.Infinity, -1, true);
        }
        public override void OnEnd()
        {
        }
    }
}
