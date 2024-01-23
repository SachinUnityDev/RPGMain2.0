using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Rabies : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Rabies;

        //+3 Haste	-3 Vigor	-20 All Res
        public override void OnApply()
        {           
            int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.haste, 3, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.vigor, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);

            allBuffIds.AddRange(charController.buffController.BuffAllRes(CauseType.TempTrait
                           , (int)tempTraitName, charID, -20f, TimeFrame.Infinity, -1, false));
        }
        public override void OnEndConvert()
        {
            base.OnEndConvert();
            if (60f.GetChance())
            {
                charController.tempTraitController
                    .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Insane);
            }
        }
    }
}
