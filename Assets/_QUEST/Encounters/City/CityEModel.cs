using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CityEModel
    {
        public CityEncounterNames encounterName;
        public int encounterSeq;
        public CityEState state;

        [TextArea(5, 10)]
        public string descTxt;
        public string choiceAStr;
        public string choiceBStr;
        public int dayEventTaken = 0; 

        public CityEModel(CityEncounterSO cityEncounterSO)
        {
            encounterName= cityEncounterSO.encounterName;
            encounterSeq= cityEncounterSO.encounterSeq; 
            state= cityEncounterSO.state;

            descTxt= cityEncounterSO.descTxt;
            choiceAStr= cityEncounterSO.choiceAStr;
            choiceBStr= cityEncounterSO.choiceBStr; 
        }
    }
}