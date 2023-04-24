using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Combat;
using Town; 

namespace Common
{    
    public class CalendarFactory : MonoBehaviour
    {
        public Dictionary<WeekEventsName, Type> AllWeekEvents = new Dictionary<WeekEventsName, Type>();
        public Dictionary<DayName, Type> AllDayEvents = new Dictionary<DayName, Type>();   
        
        [SerializeField] int weekEvents = 0;
        [SerializeField] int dayEvents = 0; 

        void Awake()
        {
            InitWeekEvents();
            InitDayEvents();
        }


        #region WEEK EVENTS
        public void InitWeekEvents()
        {
            
            if (AllWeekEvents.Count > 0) return;
            
            var getWeekEvents = Assembly.GetAssembly(typeof(WeekEventBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(WeekEventBase)));

            foreach (var weekEvent in getWeekEvents)
            {
               
                var p = Activator.CreateInstance(weekEvent) as WeekEventBase;
                AllWeekEvents.Add(p.weekName, weekEvent);
                //Debug.Log("week events added" + getWeekEvents);
            }
            weekEvents= AllWeekEvents.Count;

        }

        public WeekEventBase GetWeekEvents(WeekEventsName _weekName)
        {
            if (AllWeekEvents.ContainsKey(_weekName))
            {
                Type weekEvent = AllWeekEvents[_weekName];
                WeekEventBase  weekbase = Activator.CreateInstance(weekEvent) as WeekEventBase;
                return weekbase;
            }
            Debug.Log("week events base" + _weekName);
            return null;
        }
        #endregion
        #region
        public void InitDayEvents()
        {

            if (AllDayEvents.Count > 0) return;
            var getDayEvents = Assembly.GetAssembly(typeof(DayEventsBase)).GetTypes()
                                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(DayEventsBase)));

            foreach (var dayEvent in getDayEvents)
            {
                var p = Activator.CreateInstance(dayEvent) as DayEventsBase;
                AllDayEvents.Add(p.dayName, dayEvent);                
            }
            dayEvents = AllDayEvents.Count; 
        }

        public DayEventsBase GetDayEvent(DayName dayName)
        {
            Debug.Log("Day Name" + dayName);
            if (AllDayEvents.ContainsKey(dayName))
            {
                Type dayEvents = AllDayEvents[dayName];
                DayEventsBase dayBase = Activator.CreateInstance(dayEvents) as DayEventsBase;
                return dayBase;               
            }
            Debug.Log("Day event Not found" + dayName);
            return null; 
        }

        #endregion
    }


}

