using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{


    public class ThievesGuildController : MonoBehaviour
    {

        public ThievesGuildModel thievesGuildModel;
        //[Header("to be ref")]
        //public HouseView houseView;

        private void Start()
        {

        }
        public void InitThievesGuildController()
        {
            BuildingSO thievesGuildSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.ThievesGuild);
            thievesGuildModel = new ThievesGuildModel(thievesGuildSO);
            BuildingIntService.Instance.allBuildModel.Add(thievesGuildModel);
        }

    }
}