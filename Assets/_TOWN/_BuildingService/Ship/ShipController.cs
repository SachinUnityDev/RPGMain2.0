using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Town
{
    public class ShipController : MonoBehaviour
    {
        public ShipModel shipModel;
        [Header("to be ref")]
        public ShipView shipView;


        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDay +=UpdateBuildState;
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                shipView = FindObjectOfType<ShipView>(true);
            }
        }
        public void InitShipController()
        {
            BuildingSO shipSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Ship);
            shipModel = new ShipModel(shipSO);
            BuildingIntService.Instance.allBuildModel.Add(shipModel);
        }
        public void InitShipController(BuildingModel buildModel)
        {
            this.shipModel = new ShipModel(buildModel);
            BuildingIntService.Instance.allBuildModel.Add(buildModel);
        }
        public void OnShipUnLock()
        {
            shipModel.buildState = BuildingState.Available;
            shipModel.unLockedOnDay = CalendarService.Instance.calendarModel.dayInGame;
            shipModel.lastAvailDay = shipModel.unLockedOnDay;
        }

        public void UpdateBuildState(int dayInGame)
        {
            if (shipModel.buildState == BuildingState.Locked) return;
            // weekly name to be done .....
           // int dayInGame = CalendarService.Instance.dayInGame;           
            
            if(shipModel.buildState == BuildingState.Available)
            {
                int diff =  dayInGame - shipModel.lastAvailDay;
                if (diff > 15) // avail for 15 days 
                {
                    shipModel.buildState = BuildingState.UnAvailable;
                    shipModel.lastUnAvailDay = dayInGame;
                }
            }
            else if(shipModel.buildState == BuildingState.UnAvailable)
            {
                int diff = dayInGame - shipModel.lastUnAvailDay;
                if (diff > 10) // avail for 10 days 
                {
                    shipModel.buildState = BuildingState.Available;
                    shipModel.lastAvailDay = dayInGame;
                }
            }
        
        }
        public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
        {
            shipView = FindObjectOfType<ShipView>(true);
            foreach (BuildIntTypeData buildData in shipModel.buildIntTypes)
            {
                if (buildData.BuildIntType == buildIntType)
                {
                    buildData.isUnLocked = unLock;
                    shipView.InitBuildIntBtns(shipModel as BuildingModel);
                }
            }
        }

    }
}