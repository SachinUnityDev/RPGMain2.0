using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "AllLandscapeSO", menuName = "Common/AllLandscapeSO")]
    public class AllLandscapeSO : ScriptableObject
    {
        public List<LandscapeSO> alllandscapeSO = new List<LandscapeSO>();  

        public LandscapeSO GetLandSO(LandscapeNames landName)
        {
            int index = alllandscapeSO.FindIndex(t => t.landscapeName == landName); 
            if(index != -1)
            {
                return alllandscapeSO[index];
            }
            Debug.Log("Land SO not found" + landName);
            return null; 
        }
    }
}

