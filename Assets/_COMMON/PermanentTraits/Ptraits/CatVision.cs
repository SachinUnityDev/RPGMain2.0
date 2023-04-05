using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CatVision : PermTraitBase
    {
        //+3 acc when night	
        //+2 luck when night
        CharController charController; 
        
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.CatVision;
        public override void ApplyTrait(CharController _charController)
        {

            charController = _charController;
            charID = charController.charModel.charID; 
           // QuestService.Instance.OnPartyLocChange += IncMoralwithLoc;


            if (QuestEventService.Instance.questTimeState == TimeState.Night)
            {
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.acc, 3f);
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.luck, 2f);
            }
        }

        void AccNLuck()
        {



        }



    }
}

