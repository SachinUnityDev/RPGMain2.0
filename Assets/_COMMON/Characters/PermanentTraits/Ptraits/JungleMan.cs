using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class JungleMan : PermTraitBase
    {
        //+3 morale in the jungle 	
        //+3 luck in the jungle

        LandscapeNames prevPartyLoc;
        CharController charController; 
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.JungleMan;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            QuestEventService.Instance.OnPartyLocChange += IncMoraleNLuck; 
            charID = charController.charModel.charID;
        }

        void IncMoraleNLuck(LandscapeNames _partyLoc)
        {
            if(_partyLoc  != LandscapeNames.Jungle && prevPartyLoc == LandscapeNames.Jungle)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.morale, -3);

                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.luck, -3);
                prevPartyLoc = _partyLoc;


            }


            if (_partyLoc == LandscapeNames.Jungle)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.morale, 3);

                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.luck, 3);
                prevPartyLoc = _partyLoc; 

            }

            // increase
            // store

        }


    }
}

