using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class HiddenClaw : PermaTraitBase
    {
        //+3 acc in Stealth mode or night 
        //+6 acc if both Night and Stealth
        CharController charController;

        QuestMode prevQCMode = QuestMode.None;
        public override PermaTraitName permTraitName => PermaTraitName.HiddenClaw;
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
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.acc, -3.0f);

            if (qcMode == QuestMode.Stealth)
            {
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.acc, 3.0f);
                prevQCMode = QuestMode.Stealth; 
            }

        }

        void  IncAccInNight(TimeState _timeState)
        {
            if (_timeState == TimeState.Night)
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName,charID, AttribName.acc, 3);
            else
                charController.ChangeAttrib(CauseType.PermanentTrait, (int)permTraitName, charID, AttribName.acc, -3);


        }


    }


}

