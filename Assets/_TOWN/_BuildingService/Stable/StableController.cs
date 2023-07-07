using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class StableController : MonoBehaviour
    {
        public StableModel stableModel;
        //[Header("to be ref")]
        //public HouseView houseView;

        private void Start()
        {

        }
        public void InitStableController()
        {
            BuildingSO stableSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Stable);
            stableModel = new StableModel(stableSO);
            BuildingIntService.Instance.allBuildModel.Add(stableModel);
        }


    }
}