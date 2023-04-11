using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Common
{
    public class TheMoretheMerrier : PermaTraitBase
    {
        //1 morale for each other Safriman in party
        //+1 focus for each other Safriman in party
        public override PermaTraitName permaTraitName => PermaTraitName.TheMoreTheMerrier;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            charID = charController.charModel.charID;
            IncMoraleSafriman();
      
            //  END conditions .. TO BE Corrected 
            //CombatEventService.Instance.OnDeathInCombat += DecMorNFoc4Safriman;
            CombatEventService.Instance.OnFleeInCombat += DecMorNFoc4Safriman;
            CombatEventService.Instance.OnCombatRejoin += DecMorNFoc4ReJoinerSafriman; 

            //QuestEventService.Instance.OnDeathInQuest += DecMorNFoc4Safriman;
            //QuestEventService.Instance.OnFleeInQuest += DecMorNFoc4Safriman;

        }

        void DecMorNFoc4ReJoinerSafriman(CharController _charChanged)
        {

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != _charChanged.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Safriman)
                    {
                        c.ChangeAttrib(CauseType.PermanentTrait,(int)permaTraitName, charID, AttribName.haste, -1);

                    }
                }
            }
        }

        //1 morale for each other Safriman in party
        //+1 focus for each other Safriman in party
        void DecMorNFoc4Safriman(CharController charController)
        {
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != charController.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Safriman)
                    {
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.morale, -1);
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.focus, -1);

                    }
                }
            }
        }


        void IncMoraleSafriman()
        {

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != charController.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Safriman)
                    {
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID,AttribName.morale, +1);
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.focus, +1);

                    }
                }
            }
        }



    }
}

