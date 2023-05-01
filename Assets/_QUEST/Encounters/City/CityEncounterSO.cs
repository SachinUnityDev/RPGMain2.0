using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    [CreateAssetMenu(fileName = "CityEncounterSO", menuName = "Quest/CityEncounterSO")]
    public class CityEncounterSO : ScriptableObject
    {
        public CityENames cityEName;
        public int encounterSeq;
        public CityEState state; 

        [TextArea(5,10)]
        public string descTxt;
        public string choiceAStr; 
        public string choiceBStr;
    }
}