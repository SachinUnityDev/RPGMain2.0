using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public enum WeekEventsName

    {
        None,
        WeekOfTroubadors,
        WeekOfHunters,
        SolarFestival,
        WeekOfTheBeast,
        WeekOfFamine,
        WeekOfPirates,
        WeekOfScholars,
        WeekOfThieves,
        WeekOfTraders,
        WeekOfRejuvenation,
        WeekOfMages,
        WeekOfWarriors,

    }

    [CreateAssetMenu(fileName = "WeekSO", menuName = "Calendar Service/WeekSO")]
    public class WeekSO : ScriptableObject
    {
        public int orderInSeq;
        public WeekEventsName weekName;
        public int weekCount; 
        public string weekNameStr;
        public string weekDesc;
        public List<string> WeekSpecs = new List<string>();
        private void Awake()
        {
            orderInSeq = (int)weekName;
            weekNameStr = GetWeekNameStr(weekName);

        }

            public string GetWeekNameStr(WeekEventsName _weekName)
            {  
                switch (_weekName)
                {
                case WeekEventsName.WeekOfTroubadors: return "Week of Troubadours";
                case WeekEventsName.WeekOfHunters: return "Week of Hunters";
                case WeekEventsName.SolarFestival: return "Solar Festival";
                case WeekEventsName.WeekOfTheBeast: return "Week of the Beast";
                case WeekEventsName.WeekOfFamine: return "Week of Famine";
                case WeekEventsName.WeekOfPirates: return "Week of Pirates";
                case WeekEventsName.WeekOfScholars: return "Week of Scholars";
                case WeekEventsName.WeekOfThieves: return "Week of Thieves";
                case WeekEventsName.WeekOfTraders: return "Week of Traders";
                case WeekEventsName.WeekOfRejuvenation: return "Week of Rejuvenation";
                case WeekEventsName.WeekOfMages: return "Week of Mages";
                case WeekEventsName.WeekOfWarriors: return "Week of Warriors";
                default: return "";

                }
            }
        

    }

    



}

