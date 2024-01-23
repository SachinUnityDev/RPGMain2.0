using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Scabies : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Scabies;
        //-2 Morale	-3 Focus	+10 Elemental Res
        public override void OnApply()
        {
            int buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.morale, -2, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffid);
            buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.focus, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffid);
            buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.earthRes, +10, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffid);
            buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.airRes, +10, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffid); 
            buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName
                                            , charID, AttribName.waterRes, +10, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffid);
            buffid = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.fireRes, +10, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffid); 
    
        }

    }
}
