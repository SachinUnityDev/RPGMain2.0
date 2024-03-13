using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Combat;
using Town;
using UnityEngine.SceneManagement;


// apply month 
// change days 
// update UI and Specs as per 

namespace Common
{
    [Serializable]
    public class CalDate
    {
        public MonthName monthName;
        public int day;

        public CalDate(MonthName monthName, int day)
        {
            this.monthName = monthName;
            this.day = day;
        }
    }

    public class CalendarService : MonoSingletonGeneric<CalendarService>
    {
       // public event Action<DayName> OnCalendarDayStart;  // to be remove and incorported
        public event Action<int> OnStartOfCalDay;// broadcasts day in the Game 
        public event Action<int> OnStartOfNight;
        public event Action<WeekEventsName, int> OnStartOfTheWeek;
        public event Action<MonthName> OnStartOfTheMonth;
        public event Action<TimeState> OnChangeTimeState;
        public event Action<CalDate> OnStartOfCalDate;

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


        [SerializeField] List<MonthSO> allMonthSOs;
        public CalendarSO calendarSO; 
        public CalendarUIController calendarUIController;

        [Header("Calendar Factory")]
        public CalendarFactory calendarFactory; 

        [Header("Week Events Controller")]
        public WeekEventsController weekEventsController;
        public AllWeekSO allWeekSO;

        [Header("Day Events Controller")]
        public DayEventsController dayEventsController; 
        public AllDaySO allDaySO;
        public bool isNewGInitDone = false;
        
        void OnEnable()
        {
            calendarFactory = gameObject.GetComponent<CalendarFactory>();
            calendarUIController = GetComponent<CalendarUIController>();
            
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {

                calendarUIController.Init();
                calendarUIController.UpdateMonthPanel(currentMonth, startOfGameDayName, dayInYear);
                calendarUIController.UpdateWeekPanel(currentWeek);
            }
        }

        public void Init()
        {
            // define what date and time the game will start by default 
            weekEventsController = GetComponent<WeekEventsController>();
            weekEventsController.InitWeekController(allWeekSO);
            calendarUIController.Init();

            SelectWeekCycle();
            currtimeState = TimeState.Day;
            startOfGameDayName = DayName.DayOfLight;// saturday
            currentWeek = currWeekCycle.allWeekNames[0];
            dayInGame = 0;
            dayInYear = 24;
            currentMonth = MonthName.FeatherOfThePeafowl; 
            scrollMonth = currentMonth;
            currtimeState = TimeState.Day;
            dayEventsController = GetComponent<DayEventsController>();
            dayEventsController.InitDayEvent(allDaySO);
            calendarUIController.UpdateMonthPanel(currentMonth, startOfGameDayName, dayInYear);
            calendarUIController.UpdateWeekPanel(currentWeek);

            isNewGInitDone = true;
            
        }

        void SelectWeekCycle()
        {
            int netWeekCyles = allWeekSO.AllCycles.Count;
            int ran = UnityEngine.Random.Range(0, netWeekCyles); 
            currWeekCycle = allWeekSO.AllCycles[ran];
        }

        public WeekEventsName GetNextWeek(WeekEventsName weekEventsName)
        {
            int index  = currWeekCycle.allWeekNames.FindIndex(t=>t ==weekEventsName);
            if (index == -1)
            {
                Debug.Log("week Name NOT FOUND"); 
                return 0; 
            }
            int next = index + 1;
            next = (next >= currWeekCycle.allWeekNames.Count) ? 0 : next;  
            return currWeekCycle.allWeekNames[next];

        }
        public MonthSO GetMonthSO(MonthName _monthName)
        {
            MonthSO monthSO = allMonthSOs.Find(x => x.monthName == _monthName);
            return monthSO;
        }

        public void DisplayTimeChgPanel()          // no change in 
        {
            calendarUIController.UpdateDayPanel(dayInYear, startOfGameDayName);
            calendarUIController.OnPanelEnter(calendarUIController.dayPanel, PanelInScene.Day);
        }

        public void UpdateDayWeekNMonth()
        {
            dayInGame++; dayInYear++;
            currDayName = GetCurrDayName();             
            UpdateWeek();
            UpdateMonth();
        }
        
        void UpdateDayView()
        {
            calendarUIController.UpdateDayPanel(dayInYear, startOfGameDayName);
        }

        public int GetDayInRange(int day)
        {
            if (day > 7)
            {
                day = (day % 7);
                if (day == 0) return 7;  // correction for 7th day start
            }
            return day;
        }
        DayName GetCurrDayName()
        {  
            DayName currentDay = (DayName)GetDayInRange(dayInYear + ((int)startOfGameDayName - 1));
            return currentDay;
        }
        public void ScrollMonthClick(int incr)
        {
            scrollMonth = scrollMonth + incr;
            scrollMonth = (int)scrollMonth < 1 ? (MonthName)1 : scrollMonth;
            scrollMonth = (int)scrollMonth > 12 ? (MonthName)12 : scrollMonth;

            Debug.Log("Scroll Month " + (int)scrollMonth);
            if (((int)scrollMonth > 0) && ((int)scrollMonth <= 12))
                calendarUIController.UpdateMonthPanel(scrollMonth, startOfGameDayName, dayInYear);
        }

        public void UpdateWeek()
        {
            if ((int)currDayName % 7 == 1)
            {
                currentWeek = GetNextWeek(currentWeek); 
                weekCounter++;

                var noOfWeeks = Enum.GetNames(typeof(WeekEventsName)).Length;
                if ((int)currentWeek >= noOfWeeks) currentWeek = (WeekEventsName)1;

                calendarUIController.UpdateWeekPanel(currentWeek);
                currDayName = (DayName)1;
                On_StartOfTheWeek((WeekEventsName)currentWeek); 
            }

        }
        public void UpdateMonth()
        {
            if ((int)dayInYear % 30 == 1 && dayInGame != 0)
            {
                currentMonth++;
                var noOfMonths = Enum.GetNames(typeof(MonthName)).Length;
                if ((int)currentMonth >= noOfMonths) currentMonth = (MonthName)1;

                calendarUIController.UpdateMonthPanel(currentMonth, startOfGameDayName, dayInYear);
            }
            Debug.Log("Current Month" + currentMonth);
            calendarUIController.UpdateMonthPanel(currentMonth, startOfGameDayName, dayInYear);
            scrollMonth = currentMonth; // TIE IN POINT
                                         
        }
        public void On_EndDayClick()
        {
            if (currtimeState == TimeState.Night)
            {
                // start of the day
                currtimeState = TimeState.Day;               
                On_StartOfDay(dayInGame);
            }
            else if (currtimeState == TimeState.Day)
            {
                currtimeState = TimeState.Night;
                On_StartOfNight(dayInGame);
            }
            if(GameService.Instance.gameModel.gameState == GameState.InTown)
            {
                BuildingIntService.Instance.houseController.ChkNApplyUpgradeBedBuff();
            }            
        }
        
        #region DAY WEEK AND MONTH EVENT TRIGGERS
        public void On_StartOfDay(int day)
        {
            Debug.Log("END Day");  // day in Year .... 
            UpdateDayWeekNMonth(); // days updated here 
            dayEventsController.ApplyDayEvents(dayInYear);
           // Debug.Log("time state" + OnChangeTimeState.GetInvocationList().Length); 
            OnChangeTimeState?.Invoke(TimeState.Day);
            OnStartOfCalDay?.Invoke(dayInGame);  // day in Game 
            OnStartOfCalDate?.Invoke(new CalDate((MonthName)currentMonth, dayInYear));

            UpdateDayView(); 
        }
        public void On_StartOfNight(int day)
        {
            OnChangeTimeState?.Invoke(TimeState.Night);
            OnStartOfNight?.Invoke(day);
        }
        public void On_StartOfTheWeek(WeekEventsName weekName)
        {
            OnStartOfTheWeek?.Invoke(weekName, weekCounter);
        }

        public void On_StartOfTheMonth(MonthName monthName)
        {
            OnStartOfTheMonth?.Invoke(monthName);
        }

        public void MoveCalendarByDay(int day)
        {
            for (int i = 0; i < day; i++)
            {
                On_StartOfDay(dayInGame);
                On_StartOfNight(dayInGame);
            }
        }

        #endregion
    }
}
