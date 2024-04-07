using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class FightInTheShip : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.FightInTheShip;
        public override int seq => 0;

        bool onChoiceASelect = false;
        public override void OnChoiceASelect()
        {
            onChoiceASelect = true;
            EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
            resultStr = "The crew couldn't resist the power of you two. The swing of his axe is a deadly move by itself, add to that your rungu throwing skills, the result is 2 dead 2 wounded - and a captain watching the scene. Soon you are surrounded by more people. They are not brave enough to fight you but the Captain shouts: 'Don't step into this Ship again, you bastards!'";
            strFX = "";
        }

        public override void OnChoiceBSelect()
        {         
            EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
            resultStr = " You slow down the big guy and the Buccaneer finishes him off. A brief moment of sadness you feel for an Abzazulu who lost his life away from home. Thankfully, he has some loot to pickup. If you don't act with haste, pirates will not leave you anything though.";
            strFX = "";
        }
        public override bool PreReqChk()
        {
            return true; // no pre req condition  
        }
        public override bool UnLockCondChk()
        {
            //int cal = CalendarService.Instance.dayInGame;
            //int proofOfPowerDoneDay = 1;  // to Be Coded 
            //if (cal > proofOfPowerDoneDay + 1)
            //    return true;
            return false;
        }

        public override void CityEContinuePressed()
        {
            base.CityEContinuePressed();
            if (onChoiceASelect)
                DialogueService.Instance.On_DialogueStart(DialogueNames.HotPriestess);
        }
    }
}