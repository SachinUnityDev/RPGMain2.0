using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class RatBiteFever : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.RatBiteFever;
        public override void OnApply()
        {
           // -1 Utility stats(foc, haste, mor, luck)
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.focus, -1, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.haste, -1, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.morale, -1, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.luck, -1, TimeFrame.Infinity, -1, true);
        }

        public override void OnEnd()
        {
            
        }
    }
}