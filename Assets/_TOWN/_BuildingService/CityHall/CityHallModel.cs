using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Town
{
    public class CityHallModel : BuildingModel
    {
        public CityHallModel(BuildingSO cityHallSO)
        {
            buildingName = cityHallSO.buildingName;
            buildState = cityHallSO.buildingState;
        }
        public CityHallModel(BuildingModel buildModel)
        {
            buildingName = buildModel.buildingName;
            buildState = buildModel.buildState;
        }
    }
}