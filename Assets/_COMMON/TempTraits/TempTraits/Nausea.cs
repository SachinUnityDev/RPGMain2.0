using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public class Nausea : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Nausea;
        //-2 Focus	-1 Morale
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.focus, -2, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.morale, -1, TimeFrame.Infinity, -1, true);
        }

        public override void OnEnd()
        {
            
        }
    }
}
