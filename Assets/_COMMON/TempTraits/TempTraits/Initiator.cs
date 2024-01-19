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
           int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.haste, 2, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffId);
        }

  
    }

}

