using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public abstract class WeekEventBase
    {      
            public WeekModel weekModel { get; set;  }
            public abstract WeekEventsName weekName { get; }
            public abstract void OnWeekBonusClicked();
            
            public virtual void OnWeekInit(WeekModel weekModel)
            {
                this.weekModel= weekModel;
            }
            public abstract void OnWeekApply();
            public virtual void OnWeekEnd() { }
    }
}