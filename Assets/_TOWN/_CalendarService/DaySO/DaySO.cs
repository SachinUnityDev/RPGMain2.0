using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{    
    [CreateAssetMenu(fileName = "DaySO", menuName = "Calendar Service/DaySO")]
    public class DaySO : ScriptableObject
    {
        public int orderInSeq; 
        public DayName dayName;
        public DayName2 dayName2; 
        public string dayNameStr;
        public string dayDescription;
        [TextArea(10,15)]
        public List<string> tipOfTheDayList = new List<string>();
      
        private void Awake()
        {
            orderInSeq = (int)dayName;
            dayName2 = (DayName2)orderInSeq; 
            dayNameStr = GetDayNameStr(dayName);           
        }

        public string GetDayNameStr(DayName _dayName)
        {
            switch (_dayName)
            {
                case DayName.DayOfFire: return "Day of Fire";
                case DayName.DayOfEarth: return "Day of Earth";
                case DayName.DayOfWater: return "Day of Water";
                case DayName.DayOfAir: return "Day of Air";
                case DayName.DayOfDark: return "Day of Dark";
                case DayName.DayOfLight: return "Day of Light";
                case DayName.DayOfSpirit: return "Day of Spirit";
                default: return null;
            }
        }

    }

    public enum DayName
    {
        None,
        DayOfFire,
        DayOfEarth,
        DayOfWater,
        DayOfAir,
        DayOfDark,
        DayOfLight,
        DayOfSpirit,
    }

    public enum DayName2
    {
        None,
        İzidu,
        Kidu,
        Müdu,
        Andu,
        Kanadu,
        Nurudu,
        Aladu,
    }

}

