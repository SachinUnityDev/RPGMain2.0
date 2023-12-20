using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Hemophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Hemophobia;

        public override void OnApply()
        {
            // When Last Drop of Blood: -3 to Utility Stats          
            CharStatesService.Instance.OnCharStateStart += CharStateFX;
            CharStatesService.Instance.OnCharStateEnd += CharStateEndFX;
        }


        void CharStateFX(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.BleedLowDOT ||
                charStateModData.charStateName != CharStateName.Bleeding) return;

            if (charStateModData.effectedCharID != charController.charModel.charID) return;

            

        }

        void CharStateEndFX(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.LastDropOfBlood) return;

            if (charStateModData.effectedCharID != charController.charModel.charID) return;
            if (allBuffIds.Count <= 0) return;
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