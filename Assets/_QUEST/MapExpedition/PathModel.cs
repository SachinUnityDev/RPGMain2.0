using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    [Serializable]
    public class PathModel
    {
        public QuestNames questName;
        public ObjNames objName;
        public QuestState questState;
        public QuestState objState;
        public NodeInfo currNode; 
        public List<NodeInfo> nodes = new List<NodeInfo>();
        public PathModel(PathSO pathSO)
        {
            questName = pathSO.questName; 
            objName = pathSO.objName;   
            objState = pathSO.objState;
            nodes = pathSO.nodes.DeepClone();
        }

        //public bool IsAnyUnCrossedInterNode()
        //{
        //    if (interNodes.Count == 0) return false;
        //    foreach (InterNodeData node in interNodes)
        //    {
        //        if (!node.isCrossed)
        //            return true;
        //    }
        //    return false;
        //}

        //public MapENames GetMapENameFromInterNodeBasedOnChance(InterNodeData interNodeData)
        //{
        //    QuestMode questMode = QuestMissionService.Instance.currQuestMode;
        //    List<float> chances = new List<float>();

        //    foreach (QModeChanceData chanceData in interNodeData.allNodeChanceData)
        //    {
        //        if (chanceData.questMode == questMode)
        //        {
        //            chanceData.chanceData.ForEach(t => chances.Add(t.chance));
        //            int c = chances.GetChanceFrmList();
        //            return chanceData.chanceData[c].mapEName;
        //        }
        //    }
        //    return MapENames.None;

        //}

        //public InterNodeData GetNextUnCrossedInterNode()
        //{
        //    if (interNodes.Count == 0) return null;
        //    foreach (InterNodeData node in interNodes)
        //    {
        //        if (!node.isCrossed)
        //            return node;
        //    }
        //    return null;
        //}

        //public MapENames GetMapENameAfterChanceChk(NodeTimeData nodeTimeData, QuestMode questMode)
        //{
        //    List<float> chances = new List<float>();
        //    foreach (InterNodeData node in interNodes)
        //    {
        //        if (node.nodeTimeData.IsNodeTimeDataMatch(nodeTimeData))
        //        {
        //            for (int i = 0; i < node.allNodeChanceData.Count; i++)
        //            {
        //                if (node.allNodeChanceData[i].questMode == questMode)
        //                {
        //                    for (int j = 0; j < node.allNodeChanceData[i].chanceData.Count; j++)
        //                    {
        //                        MapChanceData chanceData = node.allNodeChanceData[i].chanceData[j];
        //                        chances.Add(chanceData.chance);
        //                    }
        //                    int c = chances.GetChanceFrmList();
        //                    return node.allNodeChanceData[i].chanceData[c].mapEName;
        //                }
        //            }
        //        }
        //    }
        //    return MapENames.None;
        //}




    }
}