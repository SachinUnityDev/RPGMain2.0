using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class CityEModel
    {
        public CityENames cityEName;
        public string cityENameStr=""; 
        public int encounterSeq;
        public CityEState state;

        [TextArea(5, 10)]
        public string descTxt;
        public string choiceAStr;
        public string choiceBStr;
        public int dayEventTaken = 0;
        public bool isCompleted; 
        

        public CityEModel(CityEncounterSO cityEncounterSO)
        {
            cityEName= cityEncounterSO.cityEName;
            encounterSeq= cityEncounterSO.encounterSeq; 
            state= cityEncounterSO.state;
            cityENameStr = cityEncounterSO.cityENameStr;

            descTxt= cityEncounterSO.descTxt;
            choiceAStr= cityEncounterSO.choiceAStr;
            choiceBStr= cityEncounterSO.choiceBStr; 
            isCompleted = false;
        }
    }
}