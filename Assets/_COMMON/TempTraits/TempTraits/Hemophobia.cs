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
            if (charStateModData.charStateName != CharStateName.Bleeding) return;

            if (charStateModData.effectedCharID != charController.charModel.charID) return;

            int buff = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID,
                                                       AttribName.focus, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buff);
        }

        void CharStateEndFX(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.LastDropOfBlood) return;

            if (charStateModData.effectedCharID != charController.charModel.charID) return;
            if (allBuffIds.Count <= 0) return;
            EndTrait();
        }

        public override void OnEndConvert()
        {
            base.OnEndConvert();
            List<float> chances = new List<float>() { 60f, 20f, 20f };

            switch (chances.GetChanceFrmList())
            {
                case 0:
                    charController.tempTraitController
                        .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Anemic);
                    break;
                case 1:
                    charController.tempTraitController
                        .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Bloodthirsty);
                    break;
                case 2:

                    break;
                default:

                    break;
            }
        }


        public override void EndTrait()
        {
            base.EndTrait();
            CharStatesService.Instance.OnCharStateStart -= CharStateFX;
            CharStatesService.Instance.OnCharStateEnd -= CharStateEndFX;
        }

    }
}