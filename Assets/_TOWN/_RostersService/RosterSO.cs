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

        public Sprite rosterLock; 
        public Sprite rosterDisband; 

        public List<AvailOfCharStrData> allAvailStateStr = new List<AvailOfCharStrData>();

        public string GetAvailDescStr(AvailOfChar availOfChar)
        {
            int index = allAvailStateStr.FindIndex(t => t.availOfChar == availOfChar);
            if (index != -1)
                return allAvailStateStr[index].availStateDescStr; 
            Debug.Log("avail status Not FOUND " + availOfChar);
            return "";
        }

        public string GetAvailHeaderStr(AvailOfChar availOfChar)
        {
            int index = allAvailStateStr.FindIndex(t => t.availOfChar == availOfChar); 
            if(index != -1)
                return allAvailStateStr[index].availStateHeaderStr;
            return "";
        }
    }
}

