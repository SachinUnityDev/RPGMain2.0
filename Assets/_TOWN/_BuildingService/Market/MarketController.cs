using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            CalendarService.Instance.OnChangeTimeState += (TimeState timeState) => UpdateBuildState();
        }

        public void InitMarketController()
        {
            marketSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Marketplace);
            marketModel = new MarketModel(marketSO);
            BuildingIntService.Instance.allBuildModel.Add(marketModel);
        }

        public void UpdateBuildState()
        {
            if (marketModel.buildState == BuildingState.Locked) return;
            DayName dayName = CalendarService.Instance.currDayName;
            TimeState timeState = CalendarService.Instance.currtimeState;

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


    }
}