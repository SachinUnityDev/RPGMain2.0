using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class FatesChild : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.FatesChild;

        public override void OnApply()
        {            
           int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                       charID, AttribName.luck, 2, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffId);   
        }

  
    }

}

