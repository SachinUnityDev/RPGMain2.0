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
        void FortitudeLossReduced(CharModData charModData)
        {        //Lose half Fortitude(everything, including Fortitude diminishing attacks)  // for chagestat Fortitude  

            if (charModData.statModified == AttribName.fortitude && charModData.modCurrVal < 0)
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID
                    , charModData.statModified, -(charModData.modCurrVal / 2));    // increase half the value , as value might be reduced on call
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
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID,AttribName.dodge,3.0f); 
            } 


        }

      

    }


}

