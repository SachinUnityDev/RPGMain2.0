using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Common
{
    [CreateAssetMenu(fileName = "AllBarkSO", menuName = "Common/BarkService/AllBarkSO")]

    public class AllBarkSO : ScriptableObject
    {
        // Start is called before the first frame update
        public List<NPCSLBarkSO> allNPCSLBarkSO = new List<NPCSLBarkSO>();
        public CurioBarkSO curioBarkSO;
        public BuildBarkSO buildBarkSO;
        public QuestPrepBarkSO QuestPrepBarkSO; // ABBAS only


        public BuildBarkSOData GetBuildBarkSOdata(BuildingNames buildName)
        {
            int index = 
            buildBarkSO.allBuildBarkSO.FindIndex(t=>t.BuildingName== buildName);    
            if(index !=-1)
            {
              //  return build
            }
            return null;
        }

    }
}