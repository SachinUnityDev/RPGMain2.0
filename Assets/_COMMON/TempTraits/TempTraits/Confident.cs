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
            //  +2 Morale         
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.morale, 2, TimeFrame.Infinity, -1, true);
        }
        public override void EndTrait()
        {
            base.EndTrait();
        }
    }

}

