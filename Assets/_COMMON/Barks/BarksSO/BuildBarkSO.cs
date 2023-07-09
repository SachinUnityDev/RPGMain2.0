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

        public BarkLineData GetBarkLineData(BuildingNames buildName, BuildingState buildState, TimeState timeState)
        {
            List<BarkLineData> barkLines= new List<BarkLineData>();
            if(allBuildBarkSO.Count > 0) 
            foreach (BuildBarkSOData allbark in allBuildBarkSO)
            {
                if(allbark.BuildingName == buildName)
                {
                    foreach (BuildStateBarkData build in allbark.BuildStates)
                    {
                            if(build.timeState == TimeState.None)
                            {
                                if (build.buildState == buildState)
                                {
                                    barkLines.Add(build.barkLineData);
                                }
                            }
                            else
                            {
                                if (build.timeState == timeState && build.buildState == buildState)
                                {
                                    barkLines.Add(build.barkLineData);
                                }
                            }                        
                    }     
                }
            }
            if(barkLines.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, barkLines.Count);
                return barkLines[random];
            }else
                return null;
        }

    }
}