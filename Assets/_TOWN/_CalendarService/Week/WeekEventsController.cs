using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class WeekEventsController : MonoBehaviour
    {
        public List<WeekModel> allWeekModels = new List<WeekModel>(); 
        public List<WeekEventBase> allWeekBases= new List<WeekEventBase>();

        void Start()
        {

        }


        public void InitWeekController(AllWeekSO allWeekSO)
        {
            foreach (WeekSO model in allWeekSO.allWeekSO)
            {
                WeekModel weekModel = new WeekModel(model);
                allWeekModels.Add(weekModel);   
            }
        }
        public WeekModel GetWeekModels(WeekEventsName weekName)
        {
            int index = allWeekModels.FindIndex(t=>t.weekName==weekName);
            if(index != -1)
                return allWeekModels[index];    
            Debug.Log("Week Model not found" + weekName);
            return null; 
        }

        public WeekEventBase GetWeekEventBase(WeekEventsName weekName)
        {
            int index = allWeekBases.FindIndex(t => t.weekName == weekName);
            if (index != -1)
                return allWeekBases[index];
            Debug.Log("Week base not found" + weekName);
            return null;
        }

        public void InitAllWeekEvents()
        {
           // WeekEventBase weekBase = CalendarService.Instance.calendarFactory.Get
        }

    }
}