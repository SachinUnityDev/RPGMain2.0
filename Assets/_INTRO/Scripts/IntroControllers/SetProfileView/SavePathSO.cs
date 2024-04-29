using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    [CreateAssetMenu(fileName = "SavePathSO", menuName = "Common/SavePathSO")]

    public class SavePathSO : ScriptableObject
    {
        public List<SavePathData> allPaths = new List<SavePathData>();
    }

    public class SavePathData
    {
        public int profileSlot; 
        public string profileName;
        public string path; 
    }

}