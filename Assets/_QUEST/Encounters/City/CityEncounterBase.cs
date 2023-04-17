using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public abstract class CityEncounterBase
    {        
        public abstract CityEncounterNames encounterName { get; }
        public abstract int seq { get; }

        public string resultStr; 
        public abstract bool StartCondChk();
        public abstract bool PreReqChk();
        public abstract void OnChoiceASelect();
        public abstract void OnChoiceBSelect();
    }
}