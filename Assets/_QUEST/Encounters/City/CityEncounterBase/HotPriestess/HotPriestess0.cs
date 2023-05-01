using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class HotPriestess0 : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.HotPriestess; 

        public override int seq => 0;

        bool onChoiceASelect = false; 
        public override void OnChoiceASelect()
        {
            onChoiceASelect= true;
            EncounterService.Instance.cityEController.UnLockNext(encounterName, seq);
            resultStr = "Her voice is tempting. You can't resist but talk to such a nice and delicate lady.";
            strFX = ""; 
        }

        public override void OnChoiceBSelect()
        {
            DialogueModel dialogueModel = DialogueService.Instance.GetDialogueModel(DialogueNames.WaterVsFire);
            dialogueModel.isLocked = false;
            EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
            resultStr = "I am not a pious man, you replied with a serious face. I guess i don't need any gods' blessing. Now get that clevage out of my sight woman!";
            strFX = "";
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