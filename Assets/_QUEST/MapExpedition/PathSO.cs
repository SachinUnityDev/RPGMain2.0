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
        public bool isSuccess;


        [Header(" TimeChg On Node Enter")]
        public int noOfHalfDaysChgOnEnter;

        [Header(" TimeChg On Node Exit")]
        public int noOfHalfDaysChgOnExit;

        public NodeInfo(int nodeSeq, bool isChecked, bool isSuccess)
        {
            this.nodeSeq = nodeSeq;
            this.isChecked = isChecked;
            this.isSuccess = isSuccess;
        }
    }
    //public enum PathState
    //{
    //    Locked, 
    //    UnLocked, 
    //    Completed, 
    //}
    [CreateAssetMenu(fileName = "PathSO", menuName = "Quest/PathSO")]
    public class PathSO : ScriptableObject
    {
        public QuestNames questName;
        public ObjNames objName;
        public QuestState questState;
        public QuestState objState;
        public List<NodeInfo> nodes = new List<NodeInfo>();
        public GameObject pathPrefab; 
       
    }
}