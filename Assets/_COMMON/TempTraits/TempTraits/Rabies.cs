using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Rabies : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Rabies;

        //+3 Haste	-3 Vigor	-20 All Res
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.haste, 3, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.vigor, -3, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.earthRes, -20, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.airRes, -20, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.waterRes, -20, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.fireRes, -20, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.darkRes, -20, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.lightRes, -20, TimeFrame.Infinity, -1, true);


        }
        public override void OnEnd()
        {
            
        }
    }
}
