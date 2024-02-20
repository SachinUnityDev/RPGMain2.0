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
        public WeekEventsName weekName;
        public int weekCount; 
        public string weekNameStr;
        [TextArea(5,10)]
        public string weekDesc;
        [TextArea(5, 10)]
        public List<string> WeekSpecs = new List<string>();
        public Sprite weekIcon; 
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

