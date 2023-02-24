using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using UnityEngine;

namespace Common
{
    public class ForestGump : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.ForestGump;

        public override void OnApply()
        {
            //-3 Focus in Forest
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                          LandscapeNames.Rainforest, StatsName.focus, -3);
        }

        public override void OnEnd()
        {
            
        }
    }
}
