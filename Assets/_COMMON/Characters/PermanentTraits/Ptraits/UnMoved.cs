using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common; 

namespace Common
{
    public class UnMoved :PermTraitBase
    {

        CharController charController; 
       
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Negative;

        public override PermanentTraitName permTraitName => PermanentTraitName.Unmoved;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            charID = charController.charModel.charID;
            DecInit4Safriman();

            CombatEventService.Instance.OnDeathInCombat += IncInit4Safriman;
          //  CombatEventService.Instance.OnFleeInCombat += IncInit4Safriman;
          // to be uncommented .. 
            CombatEventService.Instance.OnCombatRejoin += DecInit4ReJoinerSafriman;

            QuestEventService.Instance.OnDeathInQuest += IncInit4Safriman;
            QuestEventService.Instance.OnFleeInQuest += IncInit4Safriman;

        }


        void DecInit4ReJoinerSafriman(CharController _charChanged)
        {

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != _charChanged.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Safriman)
                    {
                        c.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.haste, -1);

                    }
                }
            }
        }


        void IncInit4Safriman(CharController _charChanged)
        {

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != _charChanged.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Safriman)
                    {
                        c.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.haste, -1);

                    }
                }
            }
        }

        // -1 init for each other Safriman in party
        void DecInit4Safriman()
        {

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != charController.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Safriman)
                    {
                        c.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.haste, -1);                        

                    }
                }
            }
        }
    }
}

