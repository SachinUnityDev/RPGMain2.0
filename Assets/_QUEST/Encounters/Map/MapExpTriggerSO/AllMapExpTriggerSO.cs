using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{

    [CreateAssetMenu(fileName = "AllMapExpTriggerSO", menuName = "Quest/AllMapExpTriggerSO")]

    public class AllMapExpTriggerSO : ScriptableObject
    {
        public List<MapExpTriggerSO> allMapETriggerSO = new List<MapExpTriggerSO>();
        public MapExpTriggerSO GetMapETriggerSO(NodeData startNode, NodeData endNode)
        {
            int index = allMapETriggerSO.FindIndex(t => 
            t.startNode.nodeType == startNode.nodeType && t.endNode == endNode); 
            if(index !=-1)
            {
                return allMapETriggerSO[index];
            }
            else
            {
                Debug.Log("Map trigger SO not found" + startNode+ " to " + endNode);
                return null;
            }                                 
        }
        


    }
}