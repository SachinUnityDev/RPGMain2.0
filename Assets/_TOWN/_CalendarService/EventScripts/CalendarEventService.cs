using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CalendarEventService : MonoSingletonGeneric<CalendarEventService>
    {
        public event Action<int> OnStartOfTheDay;
        public event Action OnStartOfTheNight;

        public event Action<MonthName> OnStartOfTheMonth;
        public event Action<WeekName> OnStartOfTheWeek;
        public event Action<DayName, WeekName, MonthName> OnStartOfTheGame; 



        /// <summary>
        /// This class to be used for monthly weekly and daily events
        /// </summary>
        void Start()
        {

        }

        public void On_StartOfTheDay(int currentDay)
        {
            OnStartOfTheDay?.Invoke(currentDay);
        }
        public void On_StartOftheNight()
        {
            OnStartOfTheNight?.Invoke();
        }

    }


}

