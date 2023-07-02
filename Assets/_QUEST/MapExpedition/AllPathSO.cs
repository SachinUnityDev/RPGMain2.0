using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{

    [CreateAssetMenu(fileName = "AllPathSO", menuName = "Quest/AllPathSO")]
    public class AllPathSO : ScriptableObject
    {
        
        public List<PathSO> allPathSO= new List<PathSO>();
        
        public PathSO GetPathSO(NodeData startNode, NodeData endNode)
        {
            //int index = allPathSO.FindIndex(t=>t.startNode.IsNodeDataMatch(startNode)
            //                && t.endNode.nodeData.IsNodeDataMatch(endNode));
            //if(index !=-1)
            //{
            //    return allPathSO[index];    
            //}
            //Debug.Log(" path so not found"); 
            return null;
        }
    }
}