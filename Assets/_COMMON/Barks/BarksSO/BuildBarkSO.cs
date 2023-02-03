using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using System; 
namespace Common
{
    [Serializable]
    public class BuildBarkSOData
    {
        public BuildingNames BuildingName; 
        public List<BuildStateBarkData> BuildStates = new List<BuildStateBarkData> ();
    }
    [Serializable]
    public class BuildStateBarkData
    {
        public BuildingState buildState; 
        public TimeState timeState;
        [TextArea (2,10)]
        public string barkLine;
    }

    [CreateAssetMenu(fileName = "BuildBarkSO", menuName = "Common/BarkService/BuildingBark")]
    public class BuildBarkSO : ScriptableObject
    {
        public List<BuildBarkSOData> allBuildBarkSO = new List<BuildBarkSOData>();
    }
}