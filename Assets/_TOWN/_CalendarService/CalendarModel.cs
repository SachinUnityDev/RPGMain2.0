using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [System.Serializable]
    public class CalendarModel 
    {
        [Header("CURRENT TIME STATE ")]
        public TimeState currtimeState;

        public int dayInGame;
        public int dayInYear;
        public int weekCounter;
        public WeekCycles currWeekCycle;

        // does not reset with week / Month
        [Header("CURRENT DAY WEEK AND MONTH")]
        public DayName startOfGameDayName = DayName.None;
        public DayName currDayName = DayName.None;
        public WeekEventsName currentWeek = WeekEventsName.None;
        public MonthName currentMonth = MonthName.None;
        public MonthName scrollMonth = MonthName.None;

        public CalendarModel()
        {
            dayInGame = 0;
            dayInYear = 0;
            weekCounter = 0;
            currWeekCycle = new WeekCycles();
            currtimeState = TimeState.None;
            startOfGameDayName = DayName.None;
            currDayName = DayName.None;
            currentWeek = WeekEventsName.None;
            currentMonth = MonthName.None;
            scrollMonth = MonthName.None;
        }

    }
}

