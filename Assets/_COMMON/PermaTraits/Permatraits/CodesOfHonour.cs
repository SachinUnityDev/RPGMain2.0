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
           int buffID =  charController.statBuffController.ApplyStatRecAltBuff(+30f, StatName.fortitude, CauseType.PermanentTrait
                                                    , (int)permaTraitName, charID, TimeFrame.Infinity,1 , true);
            allStatAltBuff.Add(buffID); 
            NeverFleesEvenFearFul();
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

