﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Common
{
    public class Tapeworm : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Tapeworm;
       //  Upon consume Food: Half Hunger and Thirst relief	-2 Vigor and Wp
        public override void OnApply()
        {
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.willpower, -2, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.vigor, -2, TimeFrame.Infinity, -1, true);
        }

        public override void EndTrait()
        {
            base.EndTrait();    
        }
    }
}