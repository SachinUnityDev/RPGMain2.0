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
        public override CityEncounterNames encounterName => CityEncounterNames.StreetUrchin; 

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
            }
            else
            {                
                 charModel.skillPts++;
            }
            EncounterService.Instance.cityEController.CloseCityETree(encounterName, seq);
        }

        public override void OnChoiceBSelect()
        {
            // nothing happens
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