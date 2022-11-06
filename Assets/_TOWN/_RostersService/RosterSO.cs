using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
   [System.Serializable]
    public class AvailOfCharStrData
    {
        public AvailOfChar availOfChar;
        public string availStateHeaderStr;
        public string availStateDescStr; 

    }

    [CreateAssetMenu(fileName = "RosterSO", menuName = "Common/RosterSO")]
    public class RosterSO : ScriptableObject
    {

        public GameObject rosterPanelPrefab;
        public GameObject charPortPreFab;

        

        public List<AvailOfCharStrData> allAvailStateStr = new List<AvailOfCharStrData>();

    }
}

