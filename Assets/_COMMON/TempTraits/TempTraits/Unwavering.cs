using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Unwavering : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Unwavering;

        public override void OnApply()
        {
            int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.focus, 2, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffId);
        }
    }
}
