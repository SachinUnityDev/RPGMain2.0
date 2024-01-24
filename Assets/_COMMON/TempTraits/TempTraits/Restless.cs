using Combat;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



namespace Common
{
    public class Restless : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Restless;
        //+2 Haste	-30% Healing Received	-5 Fortitude Origin
        public override void OnApply()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID
                , AttribName.haste, +2, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffID);
            buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName, charID
                , AttribName.fortOrg, -5, TimeFrame.Infinity, 1, false);
            allBuffIds.Add(buffID);

            int statBuffID =
                charController.statBuffController.ApplyStatRecAltBuff(-30f, StatName.health, CauseType.TempTrait, (int)tempTraitName
                                                   , charID, TimeFrame.Infinity, 1, false, true); 
            allStatAltBuff.Add(statBuffID);
        }
        public override void OnEndConvert()
        {
            base.OnEndConvert();
            List<float> chances = new List<float>() { 50f, 25f, 25f };

            switch (chances.GetChanceFrmList())
            {
                case 0:
                    charController.tempTraitController
                        .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Clumsy);
                    break;
                case 1:
                    charController.tempTraitController
                        .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Fragile);
                    break;
                case 2:

                    break;
                default:

                    break;
            }
        }

    }
}