using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Common
{
    public class SwornRivals : PermaTraitBase
    {
        CharController charController;        
        
        public override PermaTraitName permTraitName => PermaTraitName.SwornRivals;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;

            //event flee or die quest or combat
            charID = charController.charModel.charID;
            DecMoraleKhugarian();

            CombatEventService.Instance.OnDeathInCombat += IncMoraleKhugarian;
           // CombatEventService.Instance.OnFleeInCombat += IncMoraleKhugarian;
           // (to be uncommented)
            CombatEventService.Instance.OnCombatRejoin += DecMor4RejoinerKhugarian;

            QuestEventService.Instance.OnDeathInQuest += IncMoraleKhugarian;
            QuestEventService.Instance.OnFleeInQuest += IncMoraleKhugarian; 

        }

        void DecMor4RejoinerKhugarian(CharController _charChanged)
        {

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != _charChanged.charModel.charID) // other than myself
                {
                    if (c.charModel.cultType == CultureType.Kugharian)
                    {
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.morale, -4);
                    }
                }
            }
        }

        // -4 Mor when there is a Khugarian in team
        void IncMoraleKhugarian(CharController _charChanged)
        {
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != _charChanged.charModel.charID)
                {
                    if (c.charModel.cultType == CultureType.Kugharian)
                    {
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID,AttribName.morale, 4);
                    }
                }
            }
        }


        void DecMoraleKhugarian()
        {          

            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c.charModel.charID != charController.charModel.charID)   
                {
                    if (c.charModel.cultType == CultureType.Kugharian)
                    {
                        c.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID,AttribName.morale, -4); 
                    }
                }          

            }

        }

    }


}

