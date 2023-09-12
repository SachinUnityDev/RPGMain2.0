﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ForestGump : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.ForestGump;

        public override void OnApply(CharController charController)
        {
            //-3 Focus in Forest
            this.charController = charController;
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                          LandscapeNames.Rainforest, AttribName.focus, -3);
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}
