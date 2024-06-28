using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Combat;
using Town;
using UnityEngine.SceneManagement;
using Quest;
using System.IO;


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
        public void InitOnLoad(CalendarModel calendarModel)
        {
            if(this.calendarModel == null)
                this.calendarModel = new CalendarModel();
            this.calendarModel = calendarModel.DeepClone();  
        }
        public void Init()
        {
            weekEventsController = GetComponent<WeekEventsController>();
            dayEventsController = GetComponent<DayEventsController>();

            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    calendarModel = new CalendarModel();
                    // define what date and time the game will start by default 

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

                    dayEventsController.InitDayEvent(allDaySO);
                    // common
                    calendarUIController.UpdateMonthPanel(calendarModel.currentMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
                    calendarUIController.UpdateWeekPanel(calendarModel.currentWeek);
                }
                else
                {
                    LoadState();                  
                    calendarUIController.UpdateMonthPanel(calendarModel.currentMonth, calendarModel.startOfGameDayName, calendarModel.dayInYear);
                    calendarUIController.UpdateWeekPanel(calendarModel.currentWeek);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
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

    #region SAVE_LOAD   
        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            ClearState();
            string calJSON = JsonUtility.ToJson(calendarModel);            
            string fileName = path + "calendarModel" + ".txt";
            File.WriteAllText(fileName, calJSON);

            SaveState_WeekE();
            SaveState_DayE();
        }

        void SaveState_WeekE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/WeekE/";
            // Check if the folder does not exist and create it if necessary
            if (!Directory.Exists(path))
                CreateAFolder(path);

            ClearState_WeekE();
            foreach (WeekModel weekModel in weekEventsController.allWeekModels)
            {
                string weekEModelJson = JsonUtility.ToJson(weekModel);
                Debug.Log(weekEModelJson);
                string fileName = path + weekModel.weekName.ToString() + ".txt";
                File.WriteAllText(fileName, weekEModelJson);
            }   
            //Note: Current week Event save in cal model
        }
        void ClearState_WeekE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/WeekE/";
            DeleteAllFilesInDirectory(path);
        }
        void LoadState_WeekE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string weekEPath = path + "/WeekE/";

            if (SaveService.Instance.DirectoryExists(weekEPath))
            {
                string[] fileNames = Directory.GetFiles(weekEPath);
                List<WeekModel> allWeekEModels = new List<WeekModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;

                    string contents = File.ReadAllText(fileName);
                    WeekModel weekModel = JsonUtility.FromJson<WeekModel>(contents);

                    allWeekEModels.Add(weekModel);
                }
                weekEventsController.InitOnLoad(allWeekEModels);
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        void SaveState_DayE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/DayE/";
            // Check if the folder does not exist and create it if necessary
            if (!Directory.Exists(path))
                CreateAFolder(path);

            ClearState_DayE();
            foreach (DayModel dayModel in dayEventsController.allDayModels)
            {
                string dayModelJSON = JsonUtility.ToJson(dayModel);                
                string fileName = path + dayModel.dayName.ToString() + ".txt";
                File.WriteAllText(fileName, dayModelJSON);
            }
            //Note: Current day Event save in cal model
        }
        void ClearState_DayE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/DayE/";
            DeleteAllFilesInDirectory(path);
        }
        void LoadState_DayE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string dayEPath = path + "/DayE/";

            if (SaveService.Instance.DirectoryExists(dayEPath))
            {
                string[] fileNames = Directory.GetFiles(dayEPath);
                List<DayModel> allDayModels = new List<DayModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    DayModel dayModel = JsonUtility.FromJson<DayModel>(contents);

                    allDayModels.Add(dayModel);
                }
                dayEventsController.InitOnLoad(allDayModels);                
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;            
                    string contents = File.ReadAllText(fileName);
                    CalendarModel calModel = JsonUtility.FromJson<CalendarModel>(contents);
                    InitOnLoad(calModel);            
                }
                LoadState_WeekE();
                LoadState_DayE();
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void ClearState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            
            string[] fileNames = Directory.GetFiles(path);

            foreach (string fileName in fileNames)
            {
                if ((fileName.Contains(".meta")) || (fileName.Contains(".txt")) )                
                    File.Delete(fileName);
            }       
        }
        #endregion
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveState();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadState();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                ClearState();
            }
        }

#endregion
    }
}
