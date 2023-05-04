using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace Quest
{
    [Serializable]
    public class MapChanceData
    {
        public float chance;
        public MapENames mapEName;
    }
    [Serializable]
    public class MapTriggerData
    {
        public QuestMode questMode;
        public List<MapChanceData> chanceData = new List<MapChanceData>();
    }

    [CreateAssetMenu(fileName = "MapETriggerSO", menuName = "Quest/MapETriggerSO")]
    public class MapETriggerSO : ScriptableObject
    {
        public Nodes startNode;
        public Nodes endNode; 
        public List<MapTriggerData> allMapTriggerData = new List<MapTriggerData>();

    }
}