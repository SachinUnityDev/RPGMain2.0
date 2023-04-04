using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class DarkKnight : PermTraitBase
    {
        // Can't be ambushed at night

        // +2 init at night

        // Start is called before the first frame update
        CharController charController; 
        public override traitBehaviour traitBehaviour => traitBehaviour.Positive;
        
        public override PermTraitName permTraitName => PermTraitName.DarkKnight;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            //CalendarEventService.Instance.OnStartOfTheNight += AddInitNight;
            //CalendarEventService.Instance.OnStartOfTheDay += AddInitDay;
            // TO BE FIXED ON REVISION WITH SEMIH 

            //QuestEventService.Instance.dayNightController.ONStartOfDay += AddInit;
            //QuestEventService.Instance.dayNightController.ONStartOfNight += NoAmbush;
            //QuestEventService.Instance.dayNightController.ONStartOfDay += NoAmbush;
            charID = charController.charModel.charID;
        }

        void AddInitNight()
        {
            charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.haste, 2);
        }
        void AddInitDay()
        {
            charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.haste, -2);
        }

        void NoAmbush(TimeState _timeState)
        {
            if (_timeState == TimeState.Night)
                charController.charModel.canBeAmbushed = false; 
            else
                charController.charModel.canBeAmbushed = true;
        }
    }
}

