using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class Agile : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Agile; 
       // +2 Dodge	+2 Acc
        public override void OnApply()
        {
           int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.dodge, 2, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffID);
           buffID =
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.acc, 2, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffID);
        }
    }
}

