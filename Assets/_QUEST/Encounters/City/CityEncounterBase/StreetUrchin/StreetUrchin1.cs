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