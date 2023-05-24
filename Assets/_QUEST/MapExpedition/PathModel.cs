using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    [Serializable]
    public class PathModel
    {
        public NodeData startNode;
        public NodeTimeData endNode;
        public float timeInCalday;

        [Header(" Intermittant Node")]
        public List<InterNodeData> interNodes = new List<InterNodeData>();

        
        public PathModel(PathSO pathSO)
        {
            startNode = pathSO.startNode;
            endNode = pathSO.endNode; 
            timeInCalday = pathSO.timeInCalday;            
            interNodes = pathSO.allInterNodes.DeepClone(); 
        }
   
        public bool IsAnyUnCrossedInterNode()
        {
            if (interNodes.Count == 0) return false; 
            foreach (InterNodeData node in interNodes)
            {
                if(!node.isCrossed)
                    return false;
            }
            return true;
        }
        public InterNodeData GetAnyUnCrossedInterNode()
        {
            if (interNodes.Count == 0) return null;          
            foreach (InterNodeData node in interNodes)
            {
                if (!node.isCrossed)
                    return node;
            }
            return null;
        }

        public MapENames GetMapENameAfterChanceChk(NodeTimeData nodeTimeData, QuestMode questMode)
        {
            List<float> chances = new List<float>();
            foreach (InterNodeData node in interNodes)
            {
                if (node.nodeTimeData.IsNodeTimeDataMatch(nodeTimeData))
                {
                    for (int i = 0; i < node.allNodeChanceData.Count; i++)
                    {
                        if (node.allNodeChanceData[i].questMode == questMode)
                        {
                            for (int j = 0; j < node.allNodeChanceData[i].chanceData.Count; j++)
                            {
                                MapChanceData chanceData = node.allNodeChanceData[i].chanceData[j];
                                chances.Add(chanceData.chance);
                            }
                            int c = chances.GetChanceFrmList();
                            return node.allNodeChanceData[i].chanceData[c].mapEName;
                        }
                    }
                }
            }
            return MapENames.None;
        }




    }
}