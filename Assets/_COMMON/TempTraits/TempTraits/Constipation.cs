using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
 {
    public class Constipation : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Constipation; 
        //-3 Dodge	-2 Haste
        public override void OnApply()
        {
            int buffID =  charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                        charID, AttribName.dodge, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID); 
            buffID = 
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                        charID, AttribName.haste, -2, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);
        }
    }
}
