using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Confident : TempTraitBase
    {
        public override TempTraitName tempTraitName =>  TempTraitName.Confident;

        public override void OnApply()
        {   
            int buffid=   charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.morale, 2, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffid);
        }
    }

}

