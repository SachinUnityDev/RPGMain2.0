using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public abstract class WeekEventBase
    {      
            public WeekModel weekModel { get; set;  }

            public string resultStr = "";            
            public abstract WeekEventsName weekName { get; }            
            protected List<int> buffIDs  = new List<int>(); 
           protected  List<EventCostMultData> allEventCostMultData { get; set; }= new List<EventCostMultData>();
            public virtual void OnWeekInit(WeekModel weekModel)
            {
                this.weekModel= weekModel;
            }
            public abstract void OnWeekApply();
            public virtual void OnWeekEnd() 
            { 
                allEventCostMultData.Clear();
                
            }
            public abstract void OnWeekBonusClicked();

    }
}