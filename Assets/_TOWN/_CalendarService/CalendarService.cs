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

    public class CalendarService : MonoSingletonGeneric<CalendarService>, ISaveable
    {
       // public event Action<DayName> OnCalendarDayStart;  // to be remove and incorported
        public event Action<int> OnStartOfCalDay;// broadcasts day in the Game 
        public event Action<int> OnStartOfNight;
        public event Action<WeekEventsName, int> OnStartOfTheWeek;
        public event Action<MonthName> OnStartOfTheMonth;
        public event Action<TimeState> OnChangeTimeState;
        public event Action<CalDate> OnStartOfCalDate; // broadcast day in yr

        //[Header("CURRENT TIME STATE ")]
        //public TimeState currtimeState;

        //public int dayInGame;
        //public int dayInYear; 
        //public int weekCounter;
        //public WeekCycles currWeekCycle;

        //// does not reset with week / Month
        //[Header("CURRENT DAY WEEK AND MONTH")]
        //public DayName startOfGameDayName = DayName.None; 
        //public DayName currDayName = DayName.None;
        //public WeekEventsName currentWeek = WeekEventsName.None;
        //public MonthName currentMonth = MonthName.None;
        //public MonthName scrollMonth = MonthName.None;

        public CalendarModel calendarModel; 
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

        public ServicePath servicePath => ServicePath.CalendarService;

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
                calendarUIController.UpdateMonthPanel(calendarModel.currentMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
                calendarUIController.UpdateWeekPanel(calendarModel.currentWeek);
            }
        }

        public void Init()
        {
            calendarModel = new CalendarModel();
            // define what date and time the game will start by default 
            weekEventsController = GetComponent<WeekEventsController>();
            weekEventsController.InitWeekController(allWeekSO);
            calendarUIController.Init();

            SelectWeekCycle();
            // Calendar Model Init
            calendarModel.currtimeState = TimeState.Day;
            calendarModel.startOfGameDayName = DayName.DayOfLight;// saturday
            calendarModel.currentWeek = calendarModel.currWeekCycle.allWeekNames[0];
            calendarModel.dayInGame = 0;
            calendarModel.dayInYear = 24;
            calendarModel.currentMonth = MonthName.FeatherOfThePeafowl;
            calendarModel.scrollMonth = calendarModel.currentMonth;
            calendarModel.currtimeState = TimeState.Day;
            dayEventsController = GetComponent<DayEventsController>();
            dayEventsController.InitDayEvent(allDaySO);
            calendarUIController.UpdateMonthPanel(calendarModel.currentMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
            calendarUIController.UpdateWeekPanel(calendarModel.currentWeek);

            isNewGInitDone = true;
            
        }

        void SelectWeekCycle()
        {
            int netWeekCyles = allWeekSO.AllCycles.Count;
            int ran = UnityEngine.Random.Range(0, netWeekCyles);
            calendarModel.currWeekCycle = allWeekSO.AllCycles[ran];
        }

        public WeekEventsName GetNextWeek(WeekEventsName weekEventsName)
        {
            int index  = calendarModel.currWeekCycle.allWeekNames.FindIndex(t=>t ==weekEventsName);
            if (index == -1)
            {
                Debug.Log("week Name NOT FOUND"); 
                return 0; 
            }
            int next = index + 1;
            next = (next >= calendarModel.currWeekCycle.allWeekNames.Count) ? 0 : next;  
            return calendarModel.currWeekCycle.allWeekNames[next];

        }
        public MonthSO GetMonthSO(MonthName _monthName)
        {
            MonthSO monthSO = allMonthSOs.Find(x => x.monthName == _monthName);
            return monthSO;
        }

        public void DisplayTimeChgPanel()          // no change in 
        {
            calendarUIController.UpdateDayPanel(calendarModel.dayInYear, calendarModel.startOfGameDayName);
            calendarUIController.OnPanelEnter(calendarUIController.dayPanel, PanelInScene.Day);
        }

        public void UpdateDayWeekNMonth()
        {
            calendarModel.dayInGame++; calendarModel.dayInYear++;
            calendarModel.currDayName = GetCurrDayName();             
            UpdateWeek();
            UpdateMonth();
        }
        
        void UpdateDayView()
        {
            calendarUIController.UpdateDayPanel(calendarModel.dayInYear, calendarModel.startOfGameDayName);
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
            DayName currentDay = (DayName)GetDayInRange(calendarModel.dayInYear + ((int)calendarModel.startOfGameDayName - 1));
            return currentDay;
        }
        public void ScrollMonthClick(int incr)
        {
            calendarModel.scrollMonth = calendarModel.scrollMonth + incr;
            calendarModel.scrollMonth = (int)calendarModel.scrollMonth < 1 ? (MonthName)1 : calendarModel.scrollMonth;
            calendarModel.scrollMonth = (int)calendarModel.scrollMonth > 12 ? (MonthName)12 : calendarModel.scrollMonth;

            Debug.Log("Scroll Month " + (int)calendarModel.scrollMonth);
            if (((int)calendarModel.scrollMonth > 0) && ((int)calendarModel.scrollMonth <= 12))
                calendarUIController.UpdateMonthPanel(calendarModel.scrollMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
        }

        public void UpdateWeek()
        {
            if ((int)calendarModel.currDayName % 7 == 1)
            {
                calendarModel.currentWeek = GetNextWeek(calendarModel.currentWeek);
                calendarModel.weekCounter++;

                var noOfWeeks = Enum.GetNames(typeof(WeekEventsName)).Length;
                if ((int)calendarModel.currentWeek >= noOfWeeks) calendarModel.currentWeek = (WeekEventsName)1;

                calendarUIController.UpdateWeekPanel(calendarModel.currentWeek);
                calendarModel.currDayName = (DayName)1;
                On_StartOfTheWeek((WeekEventsName)calendarModel.currentWeek); 
            }

        }
        public void UpdateMonth()
        {
            if ((int)calendarModel.dayInYear % 30 == 1 && calendarModel.dayInGame != 0)
            {
                calendarModel.currentMonth++;
                var noOfMonths = Enum.GetNames(typeof(MonthName)).Length;
                if ((int)calendarModel.currentMonth >= noOfMonths) calendarModel.currentMonth = (MonthName)1;

                calendarUIController.UpdateMonthPanel(calendarModel.currentMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
            }
            Debug.Log("Current Month" + calendarModel.currentMonth);
            calendarUIController.UpdateMonthPanel(calendarModel.currentMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
            calendarModel.scrollMonth = calendarModel.currentMonth; // TIE IN POINT
                                         
        }
        public void On_EndDayClick(BuildingNames FrmbuildName)
        {
            if (calendarModel.currtimeState == TimeState.Night)
            {
                // start of the day
                calendarModel.currtimeState = TimeState.Day;               
                On_StartOfDay(calendarModel.dayInGame);
            }
            else if (calendarModel.currtimeState == TimeState.Day)
            {
                calendarModel.currtimeState = TimeState.Night;
                On_StartOfNight(calendarModel.dayInGame);
            }
            //if(GameService.Instance.gameModel.gameState == GameState.InTown)
            //{
            //    if(FrmbuildName == BuildingNames.House)
            //         BuildingIntService.Instance.houseController.ChkNApplyUpgradeBedBuff();
            //    else if (FrmbuildName == BuildingNames.Tavern)
            //    {
            //        if (80f.GetChance())
            //        {
            //            CharController charController = CharService.Instance.GetAbbasController(CharNames.Abbas);
            //            TempTraitController tempTraitController = charController.tempTraitController;
            //            tempTraitController.ApplyTempTrait(CauseType.BuildingInterct, (int)BuildInteractType.Purchase
            //                                            , charController.charModel.charID, TempTraitName.WellRested);
            //        }
            //    }
            //}            
        }
        
        #region DAY WEEK AND MONTH EVENT TRIGGERS
        public void On_StartOfDay(int day)
        {
            Debug.Log("END Day");  // day in Year .... 
            UpdateDayWeekNMonth(); // days updated here 
            dayEventsController.ApplyDayEvents(calendarModel.dayInYear);
           // Debug.Log("time state" + OnChangeTimeState.GetInvocationList().Length); 
            OnChangeTimeState?.Invoke(TimeState.Day);
            OnStartOfCalDay?.Invoke(calendarModel.dayInGame);  // day in Game 
            OnStartOfCalDate?.Invoke(new CalDate((MonthName)calendarModel.currentMonth, calendarModel.dayInYear));

            UpdateDayView(); 
        }
        public void On_StartOfNight(int day)
        {
            OnChangeTimeState?.Invoke(TimeState.Night);
            OnStartOfNight?.Invoke(day);
        }
        public void On_StartOfTheWeek(WeekEventsName weekName)
        {
            OnStartOfTheWeek?.Invoke(weekName, calendarModel.weekCounter);
        }

        public void On_StartOfTheMonth(MonthName monthName)
        {
            OnStartOfTheMonth?.Invoke(monthName);
        }

        public void MoveCalendarByDay(int day)
        {
            for (int i = 0; i < day; i++)
            {
                On_StartOfDay(calendarModel.dayInGame);
                On_StartOfNight(calendarModel.dayInGame);
            }
        }

        public void SaveState()
        {


        }

        public void LoadState()
        {

        }

        public void ClearState()
        {

        }

        #endregion
    }
}
