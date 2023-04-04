using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace Common
{
    public class BoldMover : PermTraitBase
    {
        // Gain 3 fortitude each dodge(6 when night)  
        //Double fortitude gain at night(except the Fortitude potion)
        CharController charController;
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;
        
        public override PermTraitName permTraitName => PermTraitName.BoldMover;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            charController.OnStatCurrValSet += DodgeCheck;
            charController.OnStatCurrValSet += IncreaseFortitude;
            charID = charController.charModel.charID;
        }

        void DodgeCheck(CharModData charModData)
        {
            if (charModData.statModified == AttribName.dodge)
            {
                // Gain 3 fortitude each dodge(6 when night)  
                if (QuestEventService.Instance.questTimeState == TimeState.Night)
                {
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID
                            , AttribName.fortitude, 6.0f, false);
                   
                }else            
                {                  
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.fortitude, 3.0f,false);
                }
            }
        }

        void IncreaseFortitude(CharModData charModData)
        {
            //Double fortitude gain at night(except the Fortitude potion)
            // to code except the fortitude potion or in fortitude potion add false
            if (charModData.statModified == AttribName.fortitude)
            {
                if (QuestEventService.Instance.questTimeState == TimeState.Night)                
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID
                        , AttribName.fortitude, charModData.modCurrVal, false); 
                //sending only x1 as one increase would have taken place before
                //reaching here

            }
        }

      
    }


}

