using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class MasterSmuggler : PermaTraitBase
    {
        //Can't get caught when smuggling items to merchant
        //1 more loot in coast 
        CharController charController;
        LandscapeNames prevPartyLoc;
        public override PermaTraitName permTraitName => PermaTraitName.MasterSmuggler;
        public override void ApplyTrait(CharController _charController )
        {
            prevPartyLoc = LandscapeNames.None; 
            charController = _charController;
            charController.charModel.canBeCaught = false;
            QuestEventService.Instance.OnPartyLocChange += IncLootInCoast; 

        }

        void IncLootInCoast(LandscapeNames _partyLoc)
        {
            if (_partyLoc == LandscapeNames.Coast)
            {
                charController.charModel.lootBonus++;                
            }
            else if (prevPartyLoc == LandscapeNames.Coast)
            {
                charController.charModel.lootBonus--;
            }

            prevPartyLoc = _partyLoc;

        }





    }
}

