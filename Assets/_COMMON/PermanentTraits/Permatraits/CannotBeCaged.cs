using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    //-2 morale in subterraneans, sewers, caves

    public class CannotBeCaged : PermaTraitBase
    {   
        LandscapeNames _prevPartyLoc; 
        public override PermaTraitName permaTraitName => PermaTraitName.CannotBeCaged;
        public override void ApplyTrait(CharController _charController)
        {
            _prevPartyLoc = LandscapeNames.None; 
            LandscapeService.Instance.OnLandscapeEnter += IncMoralwithLoc; 
        }

        void IncMoralwithLoc(LandscapeNames _partyLoc)
        {
            if (_prevPartyLoc == LandscapeNames.Sewers || _prevPartyLoc == LandscapeNames.Subterranean || _prevPartyLoc == LandscapeNames.Cave)
            {             
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.morale, 2.0f, false);                
            }
            if (_partyLoc == LandscapeNames.Sewers || _partyLoc == LandscapeNames.Subterranean || _partyLoc == LandscapeNames.Cave)
            {
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.morale, -2.0f, false);
            }
            _prevPartyLoc = _partyLoc; 
        }
    }


}

