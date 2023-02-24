using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Scabies : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Scabies;
        //-2 Morale	-3 Focus	+10 Elemental Res
        public override void OnApply()
        {
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.morale, -2, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.focus, -3, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.earthRes, +10, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.airRes, +10, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.waterRes, +10, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.fireRes, +10, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.darkRes, +10, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.lightRes, +10, TimeFrame.Infinity, -1, true);

        }

        public override void OnEnd()
        {
         
        }
    }
}
