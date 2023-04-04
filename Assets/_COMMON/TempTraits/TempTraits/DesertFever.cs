using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

   // -4 Wp	-2 Morale	+10% Thirst
    public class DesertFever : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.DesertFever;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.willpower, -4, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.morale, -2, TimeFrame.Infinity, -1, true);
        }

        public override void OnEnd()
        {
            
        }
    }
}