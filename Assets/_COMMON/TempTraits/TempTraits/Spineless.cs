using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Spineless : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Spineless;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            //-3 Willpower
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                        charID, AttribName.willpower, -3, TimeFrame.Infinity, -1, true);
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}
