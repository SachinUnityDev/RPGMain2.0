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

