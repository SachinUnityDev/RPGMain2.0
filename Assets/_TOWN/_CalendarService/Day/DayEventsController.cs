using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
   
    public class DayEventsController : MonoBehaviour  // Calendar Service extn
    {
      
        public List<DayEventsBase> allDayEventsBase = new List<DayEventsBase>();
        public List<DayModel> allDayModels = new List<DayModel>();

        //private void Start()
        //{
        //    CalendarService.Instance.OnStartOfCalDay +=ApplyDayEvents; 
        //}
        //private void OnDisable()
        //{
        //    CalendarService.Instance.OnStartOfCalDay -= ApplyDayEvents;
        //}
        public void InitDayEvent(AllDaySO allDaySO)
        {
            foreach (DaySO daySO in allDaySO.allDaySO)
            {
                DayModel dayModel = new DayModel(daySO);
                allDayModels.Add(dayModel); 
            }
            // day Events factory get all bases ....applies
            for (int i = 1; i < Enum.GetNames(typeof(DayName)).Length; i++)
            {
                DayEventsBase dayBase = 
                    GetComponent<CalendarFactory>().GetDayEvent((DayName)i);
                DayModel dayModel = GetDayModel((DayName)i); 

                dayBase.OnDayInit(dayModel);
                allDayEventsBase.Add(dayBase);
            }             
        }

        public void ApplyDayEvents(int dayInYr)
        {
            DayName currDayName = CalendarService.Instance.currDayName;
            foreach (DayModel dayModel in allDayModels)
            {
                DayEventsBase dayBase = GetDayBase(dayModel.dayName); 
                if(currDayName == dayModel.dayName) 
                         dayBase.OnDayApply();
            }
        }

        public DayEventsBase GetDayBase(DayName dayName)
        {
            int index = allDayEventsBase.FindIndex(t=>t.dayName == dayName);
            if(index != -1)
                return allDayEventsBase[index];
            else
            {
                DayEventsBase dayBase =
                    GetComponent<CalendarFactory>().GetDayEvent(dayName);
                allDayEventsBase.Add(dayBase);
                return dayBase;
            }   
        }

        public DayModel GetDayModel(DayName dayName)
        {
            int index = 
                allDayModels.FindIndex(t=>t.dayName == dayName);    
            if(index != -1)
                return allDayModels[index]; 
            else
                Debug.Log("Model not Found"+ dayName);
                return null;
        }
    }
}