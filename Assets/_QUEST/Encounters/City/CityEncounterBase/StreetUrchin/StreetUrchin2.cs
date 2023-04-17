using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class StreetUrchin2 : CityEncounterBase
    {
        public override CityEncounterNames encounterName => CityEncounterNames.StreetUrchin;

        public override int seq => 2;

        public override void OnChoiceASelect()
        {
            
        }

        public override void OnChoiceBSelect()
        {
            
        }

        public override bool PreReqChk()
        {
            return false;
        }

        public override bool StartCondChk()
        {
            return false;
        }
    }
}