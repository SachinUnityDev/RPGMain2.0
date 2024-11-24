using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Town
{
    public class MarketController : MonoBehaviour
    {
        public MarketModel marketModel;

        BuildingSO marketSO;
        [Header("TBR")]
        public MarketView marketView;
        void Start()
        {            
            CalendarService.Instance.OnChangeTimeState += UpdateBuildState;
            SceneManager.activeSceneChanged -= OnSceneLoaded;
            SceneManager.activeSceneChanged += OnSceneLoaded;   
        }
        private void OnDisable()
        {
            CalendarService.Instance.OnChangeTimeState -= UpdateBuildState;
            SceneManager.activeSceneChanged -= OnSceneLoaded;


        }
        void OnSceneLoaded(Scene oldScene, Scene newScene)
        {
            if (newScene.name == "TOWN")
            {
                marketView = FindObjectOfType<MarketView>(true);
            }
        }
        public void InitMarketController()
        {
            marketSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Marketplace);
            marketModel = new MarketModel(marketSO);
            BuildingIntService.Instance.allBuildModel.Add(marketModel);
        }
        public void InitMarketController(BuildingModel buildModel)
        {
            this.marketModel = new MarketModel(buildModel);
            BuildingIntService.Instance.allBuildModel.Add(buildModel);
        }   
        public void UpdateBuildState(TimeState timeState)
        {
            if (marketModel.buildState == BuildingState.Locked) return;
            DayName dayName = CalendarService.Instance.calendarModel.currDayName;
            

            if (dayName == DayName.DayOfWater && dayName == DayName.DayOfDark)
            {
                if(timeState == TimeState.Night)
                    marketModel.buildState = BuildingState.UnAvailable;
                else
                    marketModel.buildState = BuildingState.Available;
                return; 
            }
            if (dayName == DayName.DayOfAir)
            {
                if (timeState == TimeState.Day)
                    marketModel.buildState = BuildingState.UnAvailable;
                else
                    marketModel.buildState = BuildingState.Available;
                return;
            }
            marketModel.buildState = BuildingState.Available;
        }
        public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
        {
            marketView = FindObjectOfType<MarketView>(true);
            foreach (BuildIntTypeData buildData in marketModel.buildIntTypes)
            {
                if (buildData.BuildIntType == buildIntType)
                {
                    buildData.isUnLocked = unLock;
                    marketView.InitBuildIntBtns(marketModel as BuildingModel);
                }
            }
        }


    }
}