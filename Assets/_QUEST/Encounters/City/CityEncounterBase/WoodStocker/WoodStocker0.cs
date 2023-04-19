using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class WoodStocker0 : CityEncounterBase
    {
        public override CityEncounterNames encounterName => CityEncounterNames.Woodstocker;

        public override int seq => 0; 

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

        public override bool UnLockCondChk()
        {
            return false;
        }
    }
}