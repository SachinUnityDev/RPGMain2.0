using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Common;
using Interactables; 
namespace Quest
{
    public class StreetUrchin0 : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.StreetUrchin; 
        public override int seq => 0;

        public override void OnChoiceASelect()
        {
            CharModel charModel = 
                CharService.Instance.GetCharCtrlWithName(CharNames.Abbas_Skirmisher).charModel;

            if (charModel.skillPts > 1)
                charModel.skillPts--;

            EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
            resultStr = "Kid swore on your mother and ran away.";
            strFX = "Abbas lost a skill point"; 
        }

        public override void OnChoiceBSelect()
        {
            EcoServices.Instance.DebitPlayerInv(new Currency(0, 6));
            EncounterService.Instance.cityEController.UnLockNext(encounterName, seq);
            resultStr = "Maybe you gained nothing, but you made a poor kid happy.";
            strFX = "You lost 6 bronze coins";
        }
        public override bool PreReqChk()
        {
            return EcoServices.Instance.HasMoney(PocketType.Inv, new Currency(0, 6)); 
        }
        public override bool UnLockCondChk()
        {
            int dayFromGameStart = CalendarService.Instance.dayInGame;
            if (dayFromGameStart > 6)
                return true;
            return false;
        }
    }
}