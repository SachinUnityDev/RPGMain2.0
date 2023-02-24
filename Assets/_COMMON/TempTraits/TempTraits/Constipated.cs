using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
 {
    public class Constipated : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Constipated; 
        //-3 Dodge	-2 Haste
        public override void OnApply()
        {
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.dodge, -3, TimeFrame.Infinity, -1, true);
            
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, StatsName.haste, -2, TimeFrame.Infinity, -1, true);
        }

        public override void OnEnd()
        {
            
        }
    }
}
