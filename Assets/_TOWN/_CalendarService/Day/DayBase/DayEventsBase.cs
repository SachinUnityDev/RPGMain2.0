using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public abstract class DayEventsBase
    {
        public abstract DayName dayName { get; }        
        public DayModel dayModel;
        public virtual void OnDayApply()
        {
            dayModel = CalendarService.Instance.dayEventsController.GetDayModel(dayName);          
        }
        public virtual void OnDayEnd() 
        {
           
        }
    }
}