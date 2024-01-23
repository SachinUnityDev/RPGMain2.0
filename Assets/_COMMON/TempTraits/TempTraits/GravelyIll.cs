using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class GravelyIll : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.GravelyIll;

        public override void OnApply()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                               charID, AttribName.willpower, -6, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                             charID, AttribName.vigor, -6, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);
        }
        public override void OnEndConvert()
        {
            base.OnEndConvert();
            
            if(charController.charModel.charName != CharNames.Abbas)
                CharService.Instance.On_CharDeath(charController, charID); 
            
        }
    }
}

