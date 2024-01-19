using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class WellRested : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.WellRested;

        public override void OnApply()
        {
            int buffId =  
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.focus, 1, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffId);
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.haste, 1, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffId);
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.morale, 1, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffId);
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.luck, 1, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffId);
        }

    }
}