using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{


    public class CityHallController : MonoBehaviour
    {
        public CityHallModel cityHallModel;
        //[Header("to be ref")]
        //public HouseView houseView;

        public void InitCityHallController()
        {
            BuildingSO cityHallSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.CityHall);
            cityHallModel = new CityHallModel(cityHallSO);
            BuildingIntService.Instance.allBuildModel.Add(cityHallModel);
        }
        public void InitCityHallController(BuildingModel buildModel)
        {
            this.cityHallModel = new CityHallModel(buildModel); 
            BuildingIntService.Instance.allBuildModel.Add(buildModel);
        }   
    }
}