using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public enum WeekEventsName
    {
        None,                
        WeekOfHunters,        
        WeekOfTheBeast,
        WeekOfScholars,                
        WeekOfRejuvenation,                
    }

    [CreateAssetMenu(fileName = "WeekSO", menuName = "Calendar Service/WeekSO")]
    public class WeekSO : ScriptableObject
    {
        public List<int> orderInSeq = new List<int>();        
        public WeekEventsName weekName;
        public int weekCount; 
        public string weekNameStr;
        public string weekDesc;
        public List<string> WeekSpecs = new List<string>();
        private void Awake()
        {            
            weekNameStr = GetWeekNameStr(weekName);
        }

            public string GetWeekNameStr(WeekEventsName _weekName)
            {  
                switch (_weekName)
                {              
                    case WeekEventsName.WeekOfHunters: return "Week of Hunters";              
                    case WeekEventsName.WeekOfTheBeast: return "Week of the Beast";               
                    case WeekEventsName.WeekOfScholars: return "Week of Scholars";                   
                    case WeekEventsName.WeekOfRejuvenation: return "Week of Rejuvenation";            
                    default: return "";
                }
            }
        

    }

    



}

