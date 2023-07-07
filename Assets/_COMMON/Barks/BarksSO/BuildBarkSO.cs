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
        public BarkLineData barkLineData;
    }

    [CreateAssetMenu(fileName = "BuildBarkSO", menuName = "Common/BarkService/BuildingBark")]
    public class BuildBarkSO : ScriptableObject
    {
        public List<BuildBarkSOData> allBuildBarkSO = new List<BuildBarkSOData>();

        public BarkLineData GetBuildBarkSOData(BuildingNames buildName, BuildingState buildState, TimeState timeState)
        {
            List<BarkLineData> barkLines= new List<BarkLineData>();
            foreach (BuildBarkSOData allbark in allBuildBarkSO)
            {
                if(allbark.BuildingName == buildName)
                {
                    foreach (BuildStateBarkData build in allbark.BuildStates)
                    {
                        if(build.timeState == timeState && build.buildState == buildState)
                        {
                            barkLines.Add(build.barkLineData); 
                        }
                    }     
                }
            }
            int random = UnityEngine.Random.Range(0, barkLines.Count);  
            return barkLines[random]; 
        }

    }
}