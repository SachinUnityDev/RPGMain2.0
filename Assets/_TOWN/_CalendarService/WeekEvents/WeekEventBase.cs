using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public abstract class WeekEventBase
    {      
            public abstract WeekEventsName weekName { get; }
            public abstract void OnWeekApply();
            public virtual void OnWeekEnd() { }
    }
}