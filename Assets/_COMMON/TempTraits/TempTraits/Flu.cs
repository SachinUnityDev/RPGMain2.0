using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class Flu : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Flu;
       // -3 Vigor	-3 Willpower
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.vigor, -3, TimeFrame.Infinity, -1, true);

            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.willpower, -3, TimeFrame.Infinity, -1, true);
        }

        public override void OnEnd()
        {
            
        }
    }

}

