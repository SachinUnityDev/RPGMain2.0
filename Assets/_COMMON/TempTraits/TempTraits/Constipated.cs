using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
 {
    public class Constipated : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Constipated; 
        //-3 Dodge	-2 Haste
        public override void OnApply(CharController charController)
        {

            this.charController = charController;   
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.dodge, -3, TimeFrame.Infinity, -1, true);
            
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.haste, -2, TimeFrame.Infinity, -1, true);
        }

        public override void OnTraitEnd()
        {
            
        }
    }
}
