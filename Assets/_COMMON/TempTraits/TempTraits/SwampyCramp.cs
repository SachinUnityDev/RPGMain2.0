using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SwampyCramp : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.SwampyCramp;
       // -2 Vigor in Swamp	-2 Wp in Swamp
        public override void OnApply()
        {
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                              LandscapeNames.Swamp, StatsName.vigor, -2);

            charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                              LandscapeNames.Swamp, StatsName.willpower, -2);
        }

        public override void OnEnd()
        {
           
        }
    }
}
