using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "PathSO", menuName = "Quest/PathSO")]
    public class PathSO : ScriptableObject
    {
        public QuestNames questName;
        public ObjNames objName; 
        public NodeData startNode;
        public NodeData endNode;
        public float timeInCalday;

        [Header(" Intermittant Node")]
        public List<InterNodeData> allInterNodes = new List<InterNodeData>();  

        public bool IsAnyInterNode()
        {
            if (allInterNodes.Count > 0) return true;
            else return false; 
        }

    

    }
}