using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Unwavering : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Unwavering;

        public override void OnApply()
        {
            
            // +2 Focus
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.focus, 2, TimeFrame.Infinity, -1, true);
        }
        public override void EndTrait()
        {
            base.EndTrait();
        }
    }
}
