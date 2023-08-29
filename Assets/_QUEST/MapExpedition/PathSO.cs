using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class NodeInfo
    {
        public int nodeSeq;
        public bool isChecked; 
    }
    public enum PathState
    {
        Locked, 
        UnLocked, 
        Completed, 
    }
    [CreateAssetMenu(fileName = "PathSO", menuName = "Quest/PathSO")]
    public class PathSO : ScriptableObject
    {
        public QuestNames questName;
        public ObjNames objName;
        public QuestState questState;
        public QuestState objState;
        public List<NodeInfo> nodes = new List<NodeInfo>();
        public GameObject pathPrefab; 
        
        //public NodeData endNode;
        //public float timeInCalday;

        //[Header(" Intermittant Node")]
        //public List<InterNodeData> allInterNodes = new List<InterNodeData>();  

        //public bool IsAnyInterNode()
        //{
        //    if (allInterNodes.Count > 0) return true;
        //    else return false; 
        //}
    }
}