using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 


// apply month 
// change days 
// update UI and Specs as per 

namespace Common
{
    public class CalendarService : MonoSingletonGeneric<CalendarService>
    {
       // public event Action<DayName> OnCalendarDayStart;  // to be remove and incorported
        public event Action<int> OnStartOfCalDay;// can broadcast day, week  and month too here
        public event Action<int> OnStartOfNight;
        public event Action<WeekEventsName> OnStartOfTheWeek;
        public event Action<MonthName> OnStartOfTheMonth;
        public event Action<TimeState> OnChangeTimeState; 

        [Header("CURRENT TIME STATE ")]
        public TimeState currtimeState;

        [SerializeField] Button endday; 
        public int dayInGame;
        public int dayInYear; 
        public int weekCounter;


        // does not reset with week / Month
        [Header("CURRENT DAY WEEK AND MONTH")]
        [SerializeField] DayName dayInYrName; 
        public DayName currDayName;
        public WeekEventsName currentWeek;
        public MonthName currentMonth;
        public MonthName scrollMonth;


        [Header("DAY,WEEK AND MONTH SOs")]
        [SerializeField] List<DaySO> allDaySOs;
        [SerializeField] List<WeekSO> allWeekSOs;
        [SerializeField] List<MonthSO> allMonthSOs;
        public CalendarSO calendarSO; 
         
       // DayNightController dayNightController;
        CalendarUIController calendarUIController;

        [Header("Calendar Factory")]
        public CalendarFactory calendarFactory; 

        [Header("Week Controller")]
        public WeekEventsController weekEventsController;
        public AllWeekSO allWeekSO; 



        void Start()
        {

            calendarFactory = gameObject.GetComponent<CalendarFactory>();   

            weekEventsController = gameObject.AddComponent<WeekEventsController>();
            weekEventsController.InitWeekController(allWeekSO); 

            calendarUIController = GetComponent<CalendarUIController>();
            dayInYrName = DayName.DayOfLight;// saturday
            dayInGame = 0;
            dayInYear = 24;
            /// currentDay = gameStartDay;   to start here .. 

            scrollMonth = currentMonth;
            currtimeState = TimeState.Day;
            endday.onClick.AddListener(On_EndDayClick);

            calendarUIController.UpdateMonthPanel(currentMonth, dayInYrName, dayInYear);
        }

        public void Init()
        {
            // define what date and time the game will start by default 
            
        }
        public MonthSO GetMonthSO(MonthName _monthName)
        {
            MonthSO monthSO = allMonthSOs.Find(x => x.monthName == _monthName);
            return monthSO;
        }
        public WeekSO GetWeekSO(WeekEventsName _weekName)
        {
            WeekSO weekSO = allWeekSOs.Find(x => x.weekName == _weekName);
            return weekSO;
        }
        public DaySO GetDaySO(DayName _dayName)
        {
            DaySO daySO = allDaySOs.Find(x => x.dayName == _dayName);
            return daySO;
        }

        public void DisplayTimeChgPanel()          // no change in 
        {
            calendarUIController.UpdateDayPanel(dayInYear, dayInYrName);
            calendarUIController.OnPanelEnter(calendarUIController.dayPanel, PanelInScene.Day);
        }
        public void On_EndDayClick()
        {          
            if (currtimeState == TimeState.Night)
            {
                // start of the day
                currtimeState = TimeState.Day;
                endday.GetComponentInChildren<TextMeshProUGUI>().text = "End the Day?";              
                On_StartOfDay(dayInGame);              
            }
            else
            {
                endday.GetComponentInChildren<TextMeshProUGUI>().text = "End the Night?";
                currtimeState = TimeState.Night;
                On_StartOfNight(dayInGame);
            }
        }
        public void UpdateDay()
        {
            currDayName++; dayInGame++; dayInYear++;
           
            calendarUIController.UpdateDayPanel(dayInYear, dayInYrName);
            UpdateWeek();
            UpdateMonth();
        }
        public void ScrollMonthClick(int incr)
        {          
            scrollMonth = scrollMonth + incr;
            scrollMonth = (int)scrollMonth < 1 ? (MonthName)1 : scrollMonth;
            scrollMonth = (int)scrollMonth > 12 ? (MonthName)12 : scrollMonth;

            Debug.Log("Scroll Month " + (int)scrollMonth);
            if (((int)scrollMonth>0) && ((int)scrollMonth <= 12))
            calendarUIController.UpdateMonthPanel(scrollMonth, dayInYrName, dayInYear);
        }
        
        public void UpdateWeek()
        { 
            if ((int)currDayName % 7 == 1)
            {
                currentWeek++; weekCounter++; 

                var noOfWeeks= Enum.GetNames(typeof(WeekEventsName)).Length;                
                if ((int)currentWeek >= noOfWeeks) currentWeek = (WeekEventsName)1;  
                
                calendarUIController.UpdateWeekPanel(currentWeek); 
                currDayName = (DayName)1;              
            }
        }
        public void UpdateMonth()
        {     
            if ((int)dayInYear % 30 == 1 && dayInGame != 0 )
            {
                currentMonth++;

                var noOfMonths = Enum.GetNames(typeof(MonthName)).Length;
                if ((int)currentMonth >= noOfMonths) currentMonth = (MonthName)1;

                calendarUIController.UpdateMonthPanel(currentMonth, dayInYrName, dayInYear );
            }
                Debug.Log("Current Month" + currentMonth); 
                calendarUIController.UpdateMonthPanel(currentMonth, dayInYrName, dayInYear );
                    scrollMonth = currentMonth; // TIE IN POINT  
        }

        #region DAY WEEK AND MONTH EVENT TRIGGERS
        public void On_StartOfDay(int day)
        {
            Debug.Log("END Day");
            UpdateDay();
            OnChangeTimeState(TimeState.Day);
            OnStartOfCalDay?.Invoke(day);
        }
        public void On_StartOfNight(int day)
        {
            OnChangeTimeState(TimeState.Night);
            OnStartOfNight?.Invoke(day);
        }
        public void On_StartOfTheWeek(WeekEventsName weekName)
        {
            OnStartOfTheWeek?.Invoke(weekName);
        }

        public void On_StartOfTheMonth(MonthName monthName)
        {
            OnStartOfTheMonth?.Invoke(monthName);
        }

        #endregion
    }
}
