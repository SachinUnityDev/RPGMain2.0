using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
   // +2 morale in jungles and rainforests

    public class NaturalHabitat : PermTraitBase
    {

        CharController charController;
        bool prevPartyLocMatch; 
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.NaturalHabitat;
        public override void ApplyTrait(CharController _charController)
        {

            charController = _charController;
            prevPartyLocMatch = false;
            QuestEventService.Instance.OnPartyLocChange += IncMoraleInJungleNRainForest; 
            charID = charController.charModel.charID;
        }

        void IncMoraleInJungleNRainForest(LandscapeNames _partyLoc)
        {
            if (_partyLoc == LandscapeNames.Jungle || _partyLoc == LandscapeNames.Rainforest)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.morale, 2);
                prevPartyLocMatch = true;
            }
            else 
            {
                if (prevPartyLocMatch)
                {
                    charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.morale, -2);
                    prevPartyLocMatch = false;
                }
            }

        }
      
    }




}




