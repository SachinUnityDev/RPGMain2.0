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

        public virtual void OnDayInit(DayModel dayModel)
        {
           // dayModel = CalendarService.Instance.dayEventsController.GetDayModel(dayName);
           this.dayModel = dayModel;
        }

        public virtual void OnDayApply()
        {
            
        }
        public virtual void OnDayEnd() 
        {
           
        }
    }
}