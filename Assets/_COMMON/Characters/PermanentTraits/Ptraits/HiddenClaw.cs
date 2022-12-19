using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class HiddenClaw : PermTraitBase
    {
        //+3 acc in Stealth mode or night 
        //+6 acc if both Night and Stealth
        CharController charController;

        QuestMode prevQCMode = QuestMode.None;
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.HiddenClaw;
        public override void ApplyTrait(CharController _charController)
        {
            charController = _charController; 

            QuestEventService.Instance.OnQuestModeChange += IncAccINStealth;
            // TO BE FIXED ON REVISION WITH SEMIH 
            //QuestEventService.Instance.dayNightController.ONStartOfNight += IncAccInNight;
            //QuestEventService.Instance.dayNightController.ONStartOfDay += IncAccInNight;

        }

        void IncAccINStealth(QuestMode qcMode)
        {
 
            if (prevQCMode == QuestMode.Stealth && qcMode != QuestMode.Stealth)
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.acc, -3.0f);

            if (qcMode == QuestMode.Stealth)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.acc, 3.0f);
                prevQCMode = QuestMode.Stealth; 
            }

        }

        void  IncAccInNight(TimeState _timeState)
        {
            if (_timeState == TimeState.Night)
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName,charID, StatsName.acc, 3);
            else
                charController.ChangeStat(CauseType.PermanentTrait, (int)permTraitName, charID, StatsName.acc, -3);


        }


    }


}

