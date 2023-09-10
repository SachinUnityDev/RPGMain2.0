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
        public event Action<int> OnStartOfCalDay;// broadcasts day inthe Game 
        public event Action<int> OnStartOfNight;
        public event Action<WeekEventsName, int> OnStartOfTheWeek;
        public event Action<MonthName> OnStartOfTheMonth;
        public event Action<TimeState> OnChangeTimeState;
        public event Action<CalDate> OnStartOfCalDate;

        [Header("CURRENT TIME STATE ")]
        public TimeState currtimeState;

        [SerializeField] Button endday; 
        public int dayInGame;
        public int dayInYear; 
        public int weekCounter;



        // does not reset with week / Month
        [Header("CURRENT DAY WEEK AND MONTH")]
        [SerializeField] DayName dayInYrName = DayName.None; 
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
            //calendarFactory = gameObject.GetComponent<CalendarFactory>();
            //calendarUIController = GetComponent<CalendarUIController>();
            
           // endday.onClick.AddListener(On_EndDayClick);
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        private void OnDisable()
        {
          //  endday.onClick.RemoveAllListeners();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                endday = FindObjectOfType<EndDayBtnEvents>(true).GetComponent<Button>();
            }
        }

        public void Init()
        {
            // define what date and time the game will start by default 
            weekEventsController = GetComponent<WeekEventsController>();
            weekEventsController.InitWeekController(allWeekSO);
            calendarUIController.UpdateMonthPanel(currentMonth, dayInYrName, dayInYear);
            calendarUIController.UpdateWeekPanel(currentWeek);
            currtimeState = TimeState.Day;
            dayInYrName = DayName.DayOfLight;// saturday
            currentWeek = WeekEventsName.WeekOfRejuvenation;
            dayInGame = 0;
            dayInYear = 24;
            currentMonth = MonthName.FeatherOfThePeafowl; 
            scrollMonth = currentMonth;
            currtimeState = TimeState.Day;
            dayEventsController = GetComponent<DayEventsController>();
            dayEventsController.InitDayEvent(allDaySO);
          
          
            isNewGInitDone = true;

        }
        public MonthSO GetMonthSO(MonthName _monthName)
        {
            MonthSO monthSO = allMonthSOs.Find(x => x.monthName == _monthName);
            return monthSO;
        }

        public void DisplayTimeChgPanel()          // no change in 
        {
            calendarUIController.UpdateDayPanel(dayInYear, dayInYrName);
            calendarUIController.OnPanelEnter(calendarUIController.dayPanel, PanelInScene.Day);
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
            if (((int)scrollMonth > 0) && ((int)scrollMonth <= 12))
                calendarUIController.UpdateMonthPanel(scrollMonth, dayInYrName, dayInYear);
        }

        public void UpdateWeek()
        {
            if ((int)currDayName % 7 == 1)
            {
                currentWeek++; weekCounter++;

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

                calendarUIController.UpdateMonthPanel(currentMonth, dayInYrName, dayInYear);
            }
            Debug.Log("Current Month" + currentMonth);
            calendarUIController.UpdateMonthPanel(currentMonth, dayInYrName, dayInYear);
            scrollMonth = currentMonth; // TIE IN POINT
                                         
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
            OnEndInHouseBuff();
        }
        void OnEndInHouseBuff()
        {
            HouseModel houseModel = BuildingIntService.Instance.houseController.houseModel; 
            if (!houseModel.isBedUpgraded) return;
            if (houseModel.restChanceOnUpgrade.GetChance())
            {
                CharController charController =
                        CharService.Instance.GetCharCtrlWithName(CharNames.Abbas);
                TempTraitController tempTraitController = charController.tempTraitController;
                tempTraitController
                        .ApplyTempTrait(CauseType.BuildingInterct, (int)BuildInteractType.Purchase
                        , charController.charModel.charID, TempTraitName.WellRested);
            }
        }

        #region DAY WEEK AND MONTH EVENT TRIGGERS
        public void On_StartOfDay(int day)
        {
            Debug.Log("END Day");  // day in Year .... 
            UpdateDay();

           // Debug.Log("time state" + OnChangeTimeState.GetInvocationList().Length); 
            OnChangeTimeState?.Invoke(TimeState.Day);
            OnStartOfCalDay?.Invoke(day);  // day in Game 
            OnStartOfCalDate?.Invoke(new CalDate((MonthName)currentMonth, dayInYear)); 
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
                On_StartOfDay(dayInYear);
                On_StartOfNight(dayInYear);
            }
        }

        #endregion
    }
}
