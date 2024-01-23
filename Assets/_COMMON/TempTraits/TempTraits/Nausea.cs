using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public class Nausea : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Nausea;
        //-2 Focus	-1 Morale
        public override void OnApply()
        {      
            int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                 charID, AttribName.focus, -2, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                charID, AttribName.morale, -1, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);
        }
    }
}
