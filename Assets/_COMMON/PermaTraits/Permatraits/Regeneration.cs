using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Common
{
    public class Regeneration : PermaTraitBase
    {
        // +1 hp regen in combats per round
        
        public override PermaTraitName permaTraitName => PermaTraitName.Regeneration;
        public override void ApplyTrait()
        {
            int buffid = charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
                                             charID, AttribName.hpRegen, +1, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(buffid);

            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                               , charID, TempTraitName.Rabies, TimeFrame.Infinity, 1);

            // get temptrait model 
            TempTraitService.Instance.OnTempTraitStart += OnTempTraitStart; 

        }

        void OnTempTraitStart(TempTraitBuffData tempTraitBuffData)
        {
            if(tempTraitBuffData.tempTraitName == TempTraitName.Flu)
            {
                TempTraitModel tempTraitModel = charController.tempTraitController.GetTempTraitModel(TempTraitName.Flu);
                tempTraitModel.ChangeRestingPeriodTo(0); 
            }
            if (tempTraitBuffData.tempTraitName == TempTraitName.SoreThroat)
            {
                TempTraitModel tempTraitModel = charController.tempTraitController.GetTempTraitModel(TempTraitName.SoreThroat);
                tempTraitModel.ChangeRestingPeriodTo(0);
            }
        }
    }
}
