using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Combat;

namespace Common
{
    #region Abstract_Definitions
    public abstract class MonthEventBase : MonoBehaviour
    {
        public virtual MonthName monthName { get; set; }
        public virtual string MonthShortName { get; set; }
        public abstract void ApplyMonthSpecs(CharController charController); 
        
    }

    public abstract class  WeekEventBase :MonoBehaviour
    {
        public virtual WeekEventsName weekName { get;  set; }
        public virtual void OnWeekEnter(CharController charController) { }
        public virtual void OnWeekTick(CharController characterController) { }
        public virtual void OnWeekExit(CharController charController) { }
        
    }
    public abstract class DayEventBase 
    {
        public abstract DayName dayName { get; set; }
        public abstract string dayStr { get; set; }
        public abstract void ApplyDaySpecs(CharController charController); 
     
    }

    #endregion
   
    
    public class CalendarFactory : MonoBehaviour
    {
        public Dictionary<WeekEventsName, Type> AllWeekEvents = new Dictionary<WeekEventsName, Type>();
        [SerializeField] int weekEvents =0 ; 
        void Start()
        {
            InitWeekEvents(); 
        }

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

        public void AddWeekEvent(WeekEventsName _weekName, GameObject go)
        {
            if (AllWeekEvents.ContainsKey(_weekName))
            {
                Type weekEvent = AllWeekEvents[_weekName];
                WeekEventBase  weekInst = Activator.CreateInstance(weekEvent) as WeekEventBase;
                Debug.Log("Event Added " + weekInst.weekName); 
                go.AddComponent(weekInst.GetType());
            }
        }
    }


}

