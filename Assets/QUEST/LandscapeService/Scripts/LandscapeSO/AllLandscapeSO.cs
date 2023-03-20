using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "AllLandscapeSO", menuName = "Common/AllLandscapeSO")]
    public class AllLandscapeSO : ScriptableObject
    {
        public List<LandscapeSO> alllandscapeSO = new List<LandscapeSO>();  
    }
}

