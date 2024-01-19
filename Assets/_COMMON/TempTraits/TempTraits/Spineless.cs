using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Spineless : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Spineless;

        public override void OnApply()
        {
           int buffId = 
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                        charID, AttribName.willpower, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffId);
        }
        public override void OnEndConvert()
        {
            base.OnEndConvert();
            if (50f.GetChance())
            {
                charController.tempTraitController
                .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Fragile);
            }   

        }

    }
}
