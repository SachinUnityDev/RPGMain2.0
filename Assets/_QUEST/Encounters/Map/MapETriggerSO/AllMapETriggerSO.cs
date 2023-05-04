using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{

    [CreateAssetMenu(fileName = "AllMapETriggerSO", menuName = "Quest/AllMapETriggerSO")]

    public class AllMapETriggerSO : ScriptableObject
    {
        public List<MapETriggerSO> allMapETriggerSO = new List<MapETriggerSO>();


        public MapETriggerSO GetMapETriggerSO(Nodes startNode, Nodes endNode)
        {
            int index = allMapETriggerSO.FindIndex(t => t.startNode == startNode && t.endNode == endNode); 
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