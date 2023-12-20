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
          int id =  charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                              LandscapeNames.Swamp, AttribName.vigor, -2);
            allLandBuffIds.Add(id);
            id =  charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                              LandscapeNames.Swamp, AttribName.willpower, -2);
            allLandBuffIds.Add(id);
        }


    }
}
