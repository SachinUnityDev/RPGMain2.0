using Ink;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Thanatophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Thanatophobia; 
        
        public override void OnApply()
        {
            // When Last Drop of Blood: -3 to Utility Stats          
            CharStatesService.Instance.OnCharStateStart += CharStateFX;
            CharStatesService.Instance.OnCharStateEnd += CharStateEndFX; 
        }


        void CharStateFX(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.LastDropOfBlood) return;
            
            if(charStateModData.effectedCharID != charController.charModel.charID) return;

            int buff = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID,
                                                           AttribName.focus, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buff);

            buff = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID,
                                                           AttribName.luck, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buff);

            buff = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID,
                                                           AttribName.morale, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buff);

            buff = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID,
                                                           AttribName.haste, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buff);

        }

        void CharStateEndFX(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.LastDropOfBlood) return;

            if (charStateModData.effectedCharID != charController.charModel.charID) return;
            if(allBuffIds.Count<= 0) return;
                EndTrait(); 
        }
        public override void EndTrait()
        {
            base.EndTrait();
            CharStatesService.Instance.OnCharStateStart -= CharStateFX;
            CharStatesService.Instance.OnCharStateEnd -= CharStateEndFX;

        }
    }
}
