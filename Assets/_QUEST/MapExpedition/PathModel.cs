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
        public bool isDsplyed = false; 
        public bool isCompleted = false; // current node set the place for the pawn
        public PathModel(PathSO pathSO)
        {
            questName = pathSO.questName; 
            objName = pathSO.objName;   
            objState = pathSO.objState;
            nodes = pathSO.nodes.DeepClone();
        }
    }
}