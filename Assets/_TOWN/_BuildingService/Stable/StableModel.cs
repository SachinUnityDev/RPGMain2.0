using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class StableModel : BuildingModel
    {

        public StableModel(BuildingSO stableSO)
        {
            buildingName = stableSO.buildingName;

            buildState = stableSO.buildingState;

        }
        public StableModel(BuildingModel buildModel)
        {
            buildingName = buildModel.buildingName;
            buildState = buildModel.buildState;
        }   

    }
}