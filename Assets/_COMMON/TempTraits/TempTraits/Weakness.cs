using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Weakness : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Weakness;
        // Always triggers min Dmg	 Always triggers min Armor	 -30 All Res
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.waterRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.fireRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.earthRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.airRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.darkRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.lightRes, -30, TimeFrame.Infinity, -1, true);
        }


        public override void OnEnd()
        {
            
        }
    }
}