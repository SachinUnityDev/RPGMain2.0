using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class WellRested : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.WellRested;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.focus, 1, TimeFrame.EndOfDay, castTime, true);
            
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.haste, 1, TimeFrame.EndOfDay, castTime, true);
            
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.morale, 1, TimeFrame.EndOfDay, castTime, true);
            
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.luck, 1, TimeFrame.EndOfDay, castTime, true);

        }

        public override void OnTraitEnd()
        {
            
        }
    }
}