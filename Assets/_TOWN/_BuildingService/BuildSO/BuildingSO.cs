using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;

namespace Town
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "Town Service/BuildingSO")]
    public class BuildingSO : ScriptableObject, IBuildSO
    {

        [SerializeField] BuildingData _allbuildingData; 

        public BuildingData buildingData
        {
            get => _allbuildingData;
            set { _allbuildingData = value; }
        }
    }

  

    public interface IBuildSO
    {
        BuildingData buildingData { get; set; }   
    }


}

