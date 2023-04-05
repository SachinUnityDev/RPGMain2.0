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
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;

        public override PermTraitName permTraitName => PermTraitName.JungleMan;
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
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.morale, -3);

                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.luck, -3);
                prevPartyLoc = _partyLoc;


            }


            if (_partyLoc == LandscapeNames.Jungle)
            {
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.morale, 3);

                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.luck, 3);
                prevPartyLoc = _partyLoc; 

            }

            // increase
            // store

        }


    }
}

