using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class Initiator : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Initiator;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            //+2 Haste
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, StatsName.haste, 2, TimeFrame.Infinity, -1, true);
        }

        public override void OnEnd()
        {
            
        }
    }

}

