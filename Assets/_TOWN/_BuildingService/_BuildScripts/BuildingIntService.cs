using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using Interactables;

namespace Town
{ 
    /// <summary>
    ///  To provide a as singleton for the interior interactions of the building
    /// </summary>

    public class BuildingIntService : MonoSingletonGeneric<BuildingIntService>
    {
        public event Action<Iitems, TavernSlotType> OnItemWalled; 

        [Header("Char and NPC Select")]
        public CharNames selectChar;
        public NPCNames selectNpc; 

        public AllBuildSO allBuildSO; 
        public List<BuildingModel> allBuildModel= new List<BuildingModel>();


        public HouseController houseController;
        public TavernController tavernController; 
        public TempleController templeController;
        public MarketController marketController; 
        public ShipController shipController;
        public SafekeepController safekeepController;
        public StableController stableController; 
        public ThievesGuildController thieveController;
        public CityHallController cityHallController;
        void Start()
        {
            

        }

        public void InitBuildIntService()
        {
            houseController = GetComponent<HouseController>();
            templeController = GetComponent<TempleController>();
            tavernController = GetComponent<TavernController>();
            marketController = GetComponent<MarketController>();
            shipController = GetComponent<ShipController>();
            safekeepController = GetComponent<SafekeepController>();
            stableController = GetComponent<StableController>();
            thieveController = GetComponent<ThievesGuildController>();
            cityHallController= GetComponent<CityHallController>();

            houseController.InitHouseController();
            templeController.InitTempleController();            
            tavernController.InitTavernController();
            marketController.InitMarketController();    
            shipController.InitShipController();
            
            stableController.InitStableController();
            thieveController.InitThievesGuildController() ;
            cityHallController.InitCityHallController();
        }

        public BuildingModel GetBuildModel(BuildingNames buildName)
        {
            int index = allBuildModel.FindIndex(t=>t.buildingName== buildName);
            if(index != -1)
            {
                return allBuildModel[index];
            }
            else
            {
                Debug.Log(" building Model Not Found" + buildName);
            }
            return null; 
        }


        #region   

        public void On_ItemWalled(Iitems item, TavernSlotType tavernSlotType)
        {   
            if (tavernSlotType == TavernSlotType.Trophy)
            {
               tavernController.tavernModel.trophyOnWall = item; 
            }
            if (tavernSlotType == TavernSlotType.Pelt)
            {
                tavernController.tavernModel.peltOnWall = item;
            }
            OnItemWalled?.Invoke(item, tavernSlotType);
        }

        #endregion 



    }
}
