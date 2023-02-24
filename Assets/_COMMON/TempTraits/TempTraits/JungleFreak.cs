﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class JungleFreak : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.JungleFreak;

        public override void OnApply()
        {
            // -3 Morale in Jungle
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait, (int)tempTraitName,
                                                               LandscapeNames.Jungle , StatsName.morale, -3); 


        }
        public override void OnEnd()
        {
            
        }
    }
}