using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class Initiator : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Initiator;

        public override void OnApply()
        {
            // +2 haste
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.haste, 2, TimeFrame.Infinity, -1, true);
        }

        public override void EndTrait()
        {
            base.EndTrait();
        }
    }

}

