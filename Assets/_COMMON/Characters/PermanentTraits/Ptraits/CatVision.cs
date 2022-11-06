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
        
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.CatVision;
        public override void ApplyTrait(CharController _charController)
        {

            charController = _charController;
            charID = charController.charModel.charID; 
           // QuestService.Instance.OnPartyLocChange += IncMoralwithLoc;


            if (QuestEventService.Instance.questTimeState == TimeState.Night)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.acc, 3f);
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.luck, 2f);
            }
        }

        void AccNLuck()
        {



        }



    }
}

