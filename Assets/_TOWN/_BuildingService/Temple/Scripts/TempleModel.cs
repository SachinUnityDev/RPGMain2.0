using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class TempleModel
    {
        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public TempleModel(BuildingSO templeSO)
        {
            buildIntTypes = templeSO.buildingData.buildIntTypes.DeepClone();
        }
    }
}