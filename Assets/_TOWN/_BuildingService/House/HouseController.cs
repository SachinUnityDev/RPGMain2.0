using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{



    public class HouseController : MonoBehaviour
    {
        public HouseModel houseModel;
        [Header("to be ref")]
        public HouseViewController houseView; 


        private void Start()
        {
            BuildingSO houseSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            houseModel = new HouseModel(houseSO);
        }


    }
}