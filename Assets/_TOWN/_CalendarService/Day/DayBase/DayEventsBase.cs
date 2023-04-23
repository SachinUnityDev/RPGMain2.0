using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public abstract class DayEventsBase
    {
        public abstract DayName dayName { get; }
        public abstract void OnDayApply();
        public virtual void OnDayEnd() { }
    }
}