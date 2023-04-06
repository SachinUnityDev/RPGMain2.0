using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class RatBiteFever : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.RatBiteFever;
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            // -1 Utility stats(foc, haste, mor, luck)
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.focus, -1, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.haste, -1, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.morale, -1, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.luck, -1, TimeFrame.Infinity, -1, true);
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}