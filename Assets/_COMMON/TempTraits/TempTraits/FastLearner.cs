using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public class FastLearner : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FastLearner;
        //+30% Exp
        public override void OnApply()
        {
            int buffID = charController.buffController.ApplyExpByPercent(CauseType.TempTrait, (int)tempTraitName,
                charID, +30f, TimeFrame.Infinity, 1, true); 
            allBuffIds.Add(buffID);
        }
    }
}