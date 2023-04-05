using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 


 namespace Common
{
    public class CodesOfHonour : PermTraitBase
    {
        // Never flees from combat, never flees when Fearful 
        //Lose half Fortitude(everything, including Fortitude diminishing attacks)  // for chagestat Fortitude  
        //+3 Dodge if he is last man standing in hero party

        CharController charController;
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;
        public override PermTraitName permTraitName => PermTraitName.CodesofHonour;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            charController.OnStatChg += FortitudeLossReduced;
            NeverFleesEvenFearFul();
            charID = charController.charModel.charID;
        }
        void FortitudeLossReduced(StatModData statModData)
        {        //Lose half Fortitude(everything, including Fortitude diminishing attacks)  // for chagestat Fortitude  

            if (statModData.statModified == StatName.fortitude && statModData.modVal < 0)
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID
                    , statModData.statModified, -(statModData.modVal / 2));    // increase half the value , as value might be reduced on call
            //- minus added to increment by half value
        }

        void NeverFleesEvenFearFul()
        {
            charController.charModel.fleeBehaviour = FleeBehaviour.NeverFlees; 
        }         
        void ExtraDodgeIfLastMan()
        {
            if (CombatEventService.Instance.combatController.IsLastManInHeroes(charController))
            {
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID,AttribName.dodge,3.0f); 
            } 
        }
    }


}

