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
        [Header("to be ref")]
        public MarketView marketView;
        void Start()
        {
            marketSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Marketplace);
            marketModel = new MarketModel(marketSO);
            CalendarService.Instance.OnChangeTimeState += (TimeState timeStart) => UpdateBuildState();
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