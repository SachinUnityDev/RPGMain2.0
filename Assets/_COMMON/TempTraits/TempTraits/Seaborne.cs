using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Seaborne : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Seaborne;

        public override void OnApply()
        {
            // +3 acc
            int Id = charController.landscapeController.ApplyLandscapeBuff(CauseType.TempTrait
                             , (int)tempTraitName, LandscapeNames.Coast, AttribName.acc, +3);
            allLandBuffIds.Add(Id);
            int buffId =
               charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                charID, AttribName.airRes, -10, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffId);
            buffId =
                   charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                     charID, AttribName.waterRes, 20, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffId);


        }
    }
}