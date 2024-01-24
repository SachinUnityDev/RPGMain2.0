using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

   // -4 Wp	-2 Morale	+10% Thirst
    public class DesertFever : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.DesertFever;

        public override void OnApply()
        {
            int buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.willpower, -4, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffid);
            buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.morale, -2, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffid);

            int buffID = charController.statBuffController.ApplyStatRecAltBuff(+10f, StatName.thirst, CauseType.TempTrait
                                                  , (int)tempTraitName, charID, TimeFrame.Infinity, 1, false);
            allStatAltBuff.Add(buffID);

        }

    }
}