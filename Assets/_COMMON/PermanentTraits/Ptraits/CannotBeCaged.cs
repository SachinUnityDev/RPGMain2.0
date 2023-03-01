using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    //-2 morale in subterraneans, sewers, caves

    public class CannotBeCaged : PermTraitBase
    {

        CharController charController;
        LandscapeNames _prevPartyLoc; 
        public override PermTraitName permTraitName => PermTraitName.CannotBeCaged;
       

        public override traitBehaviour traitBehaviour => traitBehaviour.Negative;

        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            _prevPartyLoc = LandscapeNames.None; 
            QuestEventService.Instance.OnPartyLocChange += IncMoralwithLoc; 
            charID = charController.charModel.charID;
        }

        void IncMoralwithLoc(LandscapeNames _partyLoc)
        {
            if (_prevPartyLoc == LandscapeNames.Sewers || _prevPartyLoc == LandscapeNames.Subterranean || _prevPartyLoc == LandscapeNames.Cave)
            {             
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.morale, 2.0f, false);                
            }
            if (_partyLoc == LandscapeNames.Sewers || _partyLoc == LandscapeNames.Subterranean || _partyLoc == LandscapeNames.Cave)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.morale, -2.0f, false);
            }
            _prevPartyLoc = _partyLoc; 
        }
    }


}

