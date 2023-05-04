using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    [CreateAssetMenu(fileName = "CityEncounterSO", menuName = "Quest/CityEncounterSO")]
    public class CityEncounterSO : ScriptableObject
    {
        public CityENames cityEName;
        public string cityENameStr = "";
        public int encounterSeq;
        public CityEState state; 
        public LocationName locationName;   

        [TextArea(5,10)]
        public string descTxt;
        public string choiceAStr; 
        public string choiceBStr;



        private void Awake()
        {
            cityENameStr = cityEName.ToString().CreateSpace();
        }
    }
}