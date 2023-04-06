using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SoreThroat : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.SoreThroat;
       // -2 Wp	-1 Morale	+15% Thirst
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.willpower, -2, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.morale, -1, TimeFrame.Infinity, -1, true);

        }

        public override void OnTraitEnd()
        {
            
        }
    }



}
