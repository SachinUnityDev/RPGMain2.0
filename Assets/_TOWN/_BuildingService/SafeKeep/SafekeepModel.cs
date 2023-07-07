using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class SafekeepModel: BuildingModel
    {
    

        public SafekeepModel(BuildingSO safekeepSO)
        {
            buildState = safekeepSO.buildingState;
        }
    }
}