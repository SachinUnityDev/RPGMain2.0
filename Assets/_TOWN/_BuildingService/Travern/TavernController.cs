using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
using System;

namespace Town
{
    public class TavernController : MonoBehaviour
    {

        public TavernModel tavernModel;
        BuildingSO tavernSO;

        [Header("TBR")]
        public TavernView tavernView;
    
        void Start()
        {            
            CalendarService.Instance.OnChangeTimeState += (TimeState timeState)=> UpdateBuildState(); 
        }
        public void InitTavernController()
        {
            tavernSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Tavern);
            tavernModel = new TavernModel(tavernSO);
            BuildingIntService.Instance.allBuildModel.Add(tavernModel);
        }
        public void OnTrophySocketed(TGNames trophyName)
        {

        }
        public void OnPeltSocketed(TGNames peltName)
        {

        }
        public void UpdateBuildState()
        {
            DayName dayName = CalendarService.Instance.currDayName;
            TimeState timeState = CalendarService.Instance.currtimeState;
            if (tavernModel.buildState == BuildingState.Locked) return; 
            if(dayName== DayName.DayOfAir && timeState== TimeState.Night)            
                tavernModel.buildState = BuildingState.UnAvailable;            
            else
                tavernModel.buildState = BuildingState.Available;
        }
    }

    [Serializable]
    public class DayNTimeData
    {
        public DayName dayName;
        public TimeState timeState;
        public DayNTimeData(DayName dayName, TimeState timeState)
        { 
            this.dayName = dayName;
            this.timeState = timeState;
        }
    }


}


