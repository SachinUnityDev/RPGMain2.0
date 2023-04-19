using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class HotPriestess0 : CityEncounterBase
    {
        public override CityEncounterNames encounterName => CityEncounterNames.HotPriestess; 

        public override int seq => 0;

        bool onChoiceASelect = false; 
        public override void OnChoiceASelect()
        {
            onChoiceASelect= true;
            EncounterService.Instance.cityEController.UnLockNext(encounterName, seq);
        }

        public override void OnChoiceBSelect()
        {
            DialogueModel dialogueModel = DialogueService.Instance.GetDialogueModel(DialogueNames.WaterVsFire);
            dialogueModel.isLocked = false;
            EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
        }
        public override bool PreReqChk()
        {
            return true; // no pre req condition  
        }
        public override bool UnLockCondChk()
        {
            int cal = CalendarService.Instance.dayInGame;
            int proofOfPowerDoneDay = 1;  // to Be Coded 
            if (cal > proofOfPowerDoneDay + 1)
                return true;
            return false;
        }

        public override void CityEContinuePressed()
        {
            base.CityEContinuePressed();
            if(onChoiceASelect)
                DialogueService.Instance.StartDialogue(DialogueNames.HotPriestess); 
        }
    }
}