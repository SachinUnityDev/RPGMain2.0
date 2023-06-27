using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace Quest
{
    [Serializable]
    public class NodeData
    {
        public NodeType nodeType;       
        public QuestNames questName;
        public LocationName locName;

        public NodeData(QuestNames questName)
        {
            this.nodeType = NodeType.QuestNode;
            this.questName = questName; 
        }
        public NodeData(LocationName locName)
        {
            nodeType = NodeType.TownNode;
            this.locName = locName; 
        }

    }
    [Serializable]
    public class MapChanceData
    {
        public float chance;
        public MapENames mapEName;
    }
    [Serializable]
    public class QModeChanceData
    {
        public QuestMode questMode;
        public List<MapChanceData> chanceData = new List<MapChanceData>();
    }

    [CreateAssetMenu(fileName = "MapExpTriggerSO", menuName = "Quest/MapExpTriggerSO")]
    public class MapExpTriggerSO : ScriptableObject
    {
        public NodeData startNode;
        public NodeData endNode; 
        public List<QModeChanceData> allMapTriggerData = new List<QModeChanceData>();


        public MapENames GetMapENameOnChanceCalc(QuestMode questMode)
        {
            List<float> chances = new List<float>();
            QModeChanceData mapData = new QModeChanceData();
            foreach (QModeChanceData mapTData in allMapTriggerData)
            {
                if(mapTData.questMode == questMode)
                {
                    mapData= mapTData;  
                    mapTData.chanceData.ForEach(t => chances.Add(t.chance)); 
                }
            }
            int index = chances.GetChanceFrmList(); 
            return mapData.chanceData[index].mapEName;
        }

    }
}