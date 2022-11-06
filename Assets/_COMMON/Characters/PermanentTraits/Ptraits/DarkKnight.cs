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
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;
        
        public override PermanentTraitName permTraitName => PermanentTraitName.DarkKnight;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController;
            QuestEventService.Instance.dayNightController.ONStartOfNight += AddInit;
            QuestEventService.Instance.dayNightController.ONStartOfDay += AddInit;
            QuestEventService.Instance.dayNightController.ONStartOfNight += NoAmbush;
            QuestEventService.Instance.dayNightController.ONStartOfDay += NoAmbush;
            charID = charController.charModel.charID;
        }

        void AddInit(TimeState _timeState)
        {
            if (_timeState == TimeState.Night)
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.haste, 2);
            else
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.haste, -2);
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

