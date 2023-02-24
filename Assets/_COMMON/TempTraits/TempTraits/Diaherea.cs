using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Diaherea : TempTraitBase
    {
        //+2 Dodge	-4 Acc	-2 Focus
        public override TempTraitName tempTraitName => TempTraitName.Diarrhea;
        public override void OnApply()
        {
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.dodge, 2, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.acc, -4, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.focus, -2, TimeFrame.Infinity, -1, true);

        }

        public override void OnEnd()
        {
           
        }
    }
}