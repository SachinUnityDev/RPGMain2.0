using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public enum WeekName

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
        public WeekName weekName;
        public string weekNameStr;
        public string weekDesc;
        public List<string> WeekSpecs = new List<string>();
        private void Awake()
        {
            orderInSeq = (int)weekName;
            weekNameStr = GetWeekNameStr(weekName);

        }

            public string GetWeekNameStr(WeekName _weekName)
            {  
                switch (_weekName)
                {
                case WeekName.WeekOfTroubadors: return "Week of Troubadours";
                case WeekName.WeekOfHunters: return "Week of Hunters";
                case WeekName.SolarFestival: return "Solar Festival";
                case WeekName.WeekOfTheBeast: return "Week of the Beast";
                case WeekName.WeekOfFamine: return "Week of Famine";
                case WeekName.WeekOfPirates: return "Week of Pirates";
                case WeekName.WeekOfScholars: return "Week of Scholars";
                case WeekName.WeekOfThieves: return "Week of Thieves";
                case WeekName.WeekOfTraders: return "Week of Traders";
                case WeekName.WeekOfRejuvenation: return "Week of Rejuvenation";
                case WeekName.WeekOfMages: return "Week of Mages";
                case WeekName.WeekOfWarriors: return "Week of Warriors";
                default: return "";

                }
            }
        

    }

    



}

