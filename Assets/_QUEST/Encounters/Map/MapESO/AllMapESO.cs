using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{


    [CreateAssetMenu(fileName = "AllMapESO", menuName = "Quest/AllMapESO")]
    public class AllMapESO : ScriptableObject
    {
        public List<MapESO> allMapESO = new List<MapESO>();

        public MapESO GetMapESO(MapENames mapEName)
        {
            int index = allMapESO.FindIndex(t => t.mapEName == mapEName);
            if (index != -1)
            {
                return allMapESO[index];
            }
            else
            {
                Debug.Log(" Map E not found" + mapEName);
                return null;
            }
        }
    }
}