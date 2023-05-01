using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class StreetUrchin1 : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.StreetUrchin; 
        public override int seq => 1;

        public override void OnChoiceASelect()
        {
            CharModel charModel =
                CharService.Instance.GetCharCtrlWithName(CharNames.Abbas_Skirmisher).charModel;
            float chance1 = 40f;
            if (chance1.GetChance())
            {
                if (charModel.skillPts > 1)
                    charModel.skillPts--;
                resultStr = "You turned to the kid and said Kid, take this and get lost!";
                strFX = "Abbas lost a skill point"; 
            }
            else
            {                
                charModel.skillPts++;
                resultStr = "Don't take it for granted, you preach the kid, it is not easy to make some coins these days...";
                strFX = "Abbas gained a skill point";
            }
                EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
        }

        public override void OnChoiceBSelect()
        {
            // nothing happens
            resultStr = "It is good to make kids happy but maybe it's too much? You asked to yourself...";
            strFX = "";
        }

        public override bool PreReqChk()
        {
            return EcoServices.Instance.HasMoney(PocketType.Inv, new Currency(1, 6));
        }

        public override bool UnLockCondChk()
        {
            int cal = CalendarService.Instance.dayInGame;
            int urchin0Day = EncounterService.Instance.cityEController.GetPreModel(encounterName, seq).dayEventTaken;
            if (cal > urchin0Day + 4)
                return true; 
            return false;
        }
    }
}