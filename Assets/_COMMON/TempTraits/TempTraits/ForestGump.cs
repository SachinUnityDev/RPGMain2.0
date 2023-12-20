using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ForestGump : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.ForestGump;

        public override void OnApply()
        {
            //-3 Focus in Forest
            int id =  charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                          LandscapeNames.Rainforest, AttribName.focus, -3);
            allLandBuffIds.Add(id); 
        }

    }
}
