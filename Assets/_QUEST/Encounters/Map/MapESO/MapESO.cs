using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "MapESO", menuName = "Quest/MapESO")]

    public class MapESO : ScriptableObject
    {
        public MapENames mapEName;
        public string mapENameStr = "";        
        
        [TextArea(5, 10)]
        public string descTxt;
        public string choiceAStr;
        public string choiceBStr;

    }
}