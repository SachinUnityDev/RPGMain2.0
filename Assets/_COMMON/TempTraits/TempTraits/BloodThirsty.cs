using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class BloodThirsty : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Bloodthirsty; 
        //+30% Dmg vs Bleeding target	-1 Focus 	-1 Acc 
        public override void OnApply()
        {
            int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                            charID, AttribName.focus, -1, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffId);
            buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                            charID, AttribName.acc, -1, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffId);

            int index = charController.strikeController.ApplyCharStateDmgAltBuff(+30f, CauseType.TempTrait
                , (int)tempTraitName, charID, TimeFrame.Infinity, -1, true, CharStateName.Bleeding);
            allCharStateDmgAltBuffIds.Add(index);
            
        }

    }
}