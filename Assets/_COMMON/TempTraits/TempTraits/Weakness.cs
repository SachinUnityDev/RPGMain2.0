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
                                                         charID, AttribName.waterRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.fireRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.earthRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.airRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.darkRes, -30, TimeFrame.Infinity, -1, true);


            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.lightRes, -30, TimeFrame.Infinity, -1, true);
        }


        public override void OnEnd()
        {
            
        }
    }
}