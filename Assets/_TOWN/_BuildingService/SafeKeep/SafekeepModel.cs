using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class SafekeepModel
    {
        [Header("Build State")]
        public BuildingState buildState;

        public SafekeepModel(BuildingSO safekeepSO)
        {
            buildState = safekeepSO.buildingData.buildingState;

         
        }

    }
}