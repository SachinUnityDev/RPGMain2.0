using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


namespace Common
{
    public class CodesOfHonour : PermaTraitBase
    {
        public override PermaTraitName permaTraitName => PermaTraitName.CodesOfHonor;
        public override void ApplyTrait()
        {

            CombatEventService.Instance.OnCharOnTurnSet += ExtraAPWhenSOLO;
            charController.OnStatChg += FortitudeLossReduced;
            NeverFleesEvenFearFul();
        }
        void FortitudeLossReduced(StatModData statModData)
        {        //Lose half Fortitude(everything, including Fortitude diminishing attacks)  // for chagestat Fortitude  

            if (statModData.statModified == StatName.fortitude && statModData.modVal < 0)
                charController.ChangeStat(CauseType.PermanentTrait, (int)permaTraitName, charID
                    , statModData.statModified, -(statModData.modVal / 2));    // increase half the value , as value might be reduced on call
            //- minus added to increment by half value
        }

        void NeverFleesEvenFearFul()
        {
            charController.charModel.fleeBehaviour = FleeBehaviour.NeverFlees;
        }
        void ExtraAPWhenSOLO(CharController charController)
        {
            if (charController.charModel.charID != charID) return;
            if (CharService.Instance.ChkIfSOLO(charController))
            {
                charController.combatController.actionPts++;
            }
        }
    }
}

