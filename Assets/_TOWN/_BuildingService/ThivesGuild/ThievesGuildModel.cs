using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Town
{


    public class ThievesGuildModel : BuildingModel
    {
        public ThievesGuildModel(BuildingSO thievesGuildSO)
        {
            buildingName = thievesGuildSO.buildingName;

            buildState = thievesGuildSO.buildingState;
        }
    }
}