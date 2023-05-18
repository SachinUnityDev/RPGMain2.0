using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class NodeTimeData
    {
        public NodeData nodeData;
        public float time;
        public NodeTimeData(NodeData nodeData, float time) 
        {
            this.nodeData = nodeData;
            this.time = time;
        }
    }

    [Serializable]
    public class InterNodeData
    {
        public NodeTimeData nodeTimeData;
        public List<QModeChanceData> allNodeChanceData = new List<QModeChanceData>();
        public bool isCrossed =false;
    }


    [CreateAssetMenu(fileName = "PathSO", menuName = "Quest/PathSO")]
    public class PathSO : ScriptableObject
    {
        public NodeData startNode;         
        public NodeTimeData endNode;
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