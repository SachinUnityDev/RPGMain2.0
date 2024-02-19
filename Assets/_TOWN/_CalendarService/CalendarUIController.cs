using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using Town;
using UnityEngine.UI;
using Combat;
using UnityEngine.SceneManagement;
using DG.Tweening.Core;

namespace Common
{
    public class CalendarUIController : MonoBehaviour
    {
        [SerializeField] GameObject townCenterPanel;

        public GameObject famePanel;
        public GameObject monthPanel;
        public GameObject weekPanel;
        public GameObject dayPanel;     
        [SerializeField] List<GameObject> allPanels = new List<GameObject>();
        public GameObject dayBGPanel;
        //  public GameObject nightBGPanel;

        [SerializeField] Button showMonthBtn;
        [SerializeField] Button showWeekBtn;

        public PanelInScene panelInScene; 
         void Awake()
         {
            //START OF THE GAME
           //if() 
           // GetMonthStartDay(MonthName.WingOfTheLocust, DayName.DayOfLight);
           
        }
       
        public void Init()
        {
            dayBGPanel = FindObjectOfType<TownViewController>(false).gameObject;
            famePanel = FindObjectOfType<FameViewController>(true).gameObject;
            monthPanel = FindObjectOfType<HelpMonth>(true).gameObject;// script attached to month
            dayPanel = FindObjectOfType<HelpDay>(true).gameObject;// script attached to Day 
            weekPanel = FindObjectOfType<WeekView>(true).gameObject;
            townCenterPanel = FindObjectOfType<TownCenterView>(true).gameObject;


            //showMonthBtn = monthPanel.GetComponentInChildren<Button>();
            //showWeekBtn = weekPanel.GetComponentInChildren<Button>();


            townCenterPanel.SetActive(true);
            allPanels.Clear();
            allPanels.AddRange(new List<GameObject>(){ famePanel, monthPanel, weekPanel, dayPanel });
            CloseAllPanel();
            showMonthBtn.onClick.RemoveAllListeners();// prevent double subscriptions
            showWeekBtn.onClick.RemoveAllListeners();   
            showMonthBtn.onClick.AddListener(OnShowMonthBtnPressed);
            showWeekBtn.onClick.AddListener(OnShowWeekBtnPressed);

        }
        private void OnEnable()
        {           
            SceneManager.sceneLoaded += OnSceneLoaded;   
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //if (scene.name == "TOWN")
            //{
            //    Init();
            //}
        }

        void OnShowWeekBtnPressed()
        {
            UpdateWeekPanel(CalendarService.Instance.currentWeek);
            OnPanelEnter(weekPanel, PanelInScene.Week);
        }
        void OnShowMonthBtnPressed()
        {   
            OnPanelEnter(monthPanel, PanelInScene.Month);
        }
        public void OnTownBannerClicked()
        {         
            OnPanelEnter(monthPanel, PanelInScene.Month); 
        }
        public void OnMonthNWeekToggleClicked()
        {
            if (panelInScene == PanelInScene.Month)
                OnPanelEnter(weekPanel, PanelInScene.Week);
            else OnPanelEnter(monthPanel, PanelInScene.Month); 
        }

        public void OnFameBtnClick()
        {          
            OnPanelEnter(famePanel, PanelInScene.Fame);
        }
        public void OnPanelEnter(GameObject panel, PanelInScene _panelInScene)
        {
            if (panelInScene == _panelInScene) 
            {  
                OnPanelExit(panel);
            }
            else
            {
                OnPanelExit(GetPanelInScene(panelInScene));
                panelInScene = _panelInScene;
                panel?.GetComponent<RectTransform>().DOLocalMove(new Vector3(0,0,0), 0.4f);
                if(panel != null)
                {
                    HelpPanelSetUp(panel, panelInScene);
                    panel.SetActive(true);                
                    Image Img = panel?.transform.parent.parent.GetComponent<Image>();
                    Img.enabled = true;
                }
            }            
        }
        void HelpPanelSetUp(GameObject panel, PanelInScene panelInScene)
        {
            HelpName helpName = HelpName.None;
            switch (panelInScene)
            {
                
                case PanelInScene.None:
                    helpName = HelpName.None; 
                    break;
                case PanelInScene.Day:
                    helpName = HelpName.EndDay;
                    break;
                case PanelInScene.Week:
                    helpName = HelpName.WeeklyEvents;
                    break;
                case PanelInScene.Month:
                    helpName = HelpName.CalendarPanel;
                    break;
                case PanelInScene.Fame:
                    helpName = HelpName.Fame;
                    break;
                default:
                    break;
            }
            UIControlServiceGeneral.Instance.helpName = helpName;

        }
        public void OnPanelExit(GameObject panel)
        {   
            if(panel != null)
            {
                Image Img = panel?.transform.parent.parent.GetComponent<Image>();
                Img.enabled = false;
                panel.SetActive(false);
            }
            panel?.transform.DOLocalMoveY(1200, 1).SetEase(Ease.OutQuint);
            panelInScene = PanelInScene.None;        
        }

        public void CloseAllPanel()
        {
            foreach (GameObject panel in allPanels)
            {
                OnPanelExit(panel);
            }
        }

    
        public void UpdateDayPanel(int _currentdayInYr, DayName _gameStartDay)
        {
            Debug.Log(_gameStartDay + "in num " + (int)_gameStartDay);
            Debug.Log("current Day in year" + _currentdayInYr);
            DayName currentDay = CalendarService.Instance.currDayName; 
                //(DayName)GetDayInRange(_currentdayInYr +((int)_gameStartDay - 1));            
                                            
            Debug.Log("Current Day" + currentDay);
            
            dayPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text 
                       = CalendarService.Instance.allDaySO.GetDaySO(currentDay).dayNameStr;
            Debug.Log(dayPanel.transform.GetChild(1).name);
            RectTransform[] allDayGO = dayPanel.transform.GetChild(1).GetComponentsInChildren<RectTransform>(true);
            
            ToggleBarPanel(allDayGO, (int)currentDay, 8);

            List<string> tipOftheDayList = CalendarService.Instance.allDaySO.GetDaySO(currentDay).tipOfTheDayList; 
            int len = tipOftheDayList.Count;
            if (CalendarService.Instance.currtimeState == TimeState.Day)
            {
                dayPanel.transform.GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Tip of the Day";                
            } else
            {
                dayPanel.transform.GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Tip of the Night";

            }
            dayPanel.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text
                                            = tipOftheDayList[UnityEngine.Random.Range(0, len)];

            FillSpecs(currentDay);
        }
        public void FillSpecs(DayName currDayName)
        {
            DayModel dayModel = CalendarService.Instance.dayEventsController.GetDayModel(currDayName);
            if (dayModel == null) return;
            dayPanel.transform.GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text
                = dayModel.daySpecs.ToString();
        }
        public void ToggleBarPanel(RectTransform[] allGO, int visible1, int visible2 =-1)
        {
            for(int i =1; i < allGO.Length; i++)  // 1 to jump the parent 
            {
                if (i == visible1 || i == visible2)                     
                {                   
                    allGO[i].gameObject.SetActive(true);                   
                }
                else
                {
                    allGO[i].gameObject.SetActive(false);
                }
            }
        }
        public void UpdateWeekPanel(WeekEventsName currentWeek)
        {
            Transform txtParent = weekPanel.transform.GetChild(0);
            WeekModel weekModel = CalendarService.Instance.weekEventsController.GetWeekModels(currentWeek);

            // week panel get week btn
            weekPanel.GetComponent<WeekView>().InitWeeek(this, weekModel);
        }

        public void UpdateMonthPanel(MonthName _currentMonth, DayName _gameStartDay, int _currentdayInYr)
        {
            Transform yearNameParent = monthPanel.transform.GetChild(0);
            yearNameParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "1339, Year of the Khudan Donkey";
            Transform monthNameParent = monthPanel.transform.GetChild(1).GetChild(0); 
            if(CalendarService.Instance?.GetMonthSO((MonthName)_currentMonth) != null)
                monthNameParent.GetComponent<TextMeshProUGUI>().text = CalendarService.Instance?.GetMonthSO((MonthName)_currentMonth).monthNameStr;
             
            Transform daysPanel = monthPanel.transform.GetChild(2);

            int childCount = daysPanel.childCount;
            int monthStartday = GetMonthStartDay(_currentMonth, _gameStartDay); 

            for(int i = 0; i<childCount; i++)
            {                
                DayBtnController dayBtnController=  daysPanel.GetChild(i).gameObject.GetComponent<DayBtnController>();
                SetUpDayBtn(dayBtnController,monthStartday, _currentdayInYr,_currentMonth, i);
            }
        }

        void SetUpDayBtn(DayBtnController _dayBtnController, int _monthStartday ,int _currentdayInYr, MonthName _currentMonth, int pos)
        {
            int today = pos +2 - _monthStartday; 
           // Debug.Log("day no " + (pos +2 -_monthStartday) +"pos " +pos +"monthStartday " +_monthStartday); 
            if (today <1)     
            {
               _dayBtnController.SetState(DayBtnState.Blankday, 0); 
            } 
            else if (today > 30)
            {
                _dayBtnController.SetState(DayBtnState.Blankday, 0);

            }
            else 
            {
                if (((int)(_currentMonth-1) * 30 +today) == _currentdayInYr)
                {
                   
                    _dayBtnController.SetState(DayBtnState.Currentday, today);
                }
                if (((int)(_currentMonth-1) * 30 + today) < _currentdayInYr)
                {
                   
                    _dayBtnController.SetState(DayBtnState.Passedday, today);
                }                
                if (((int)(_currentMonth-1) * 30 + today) > _currentdayInYr)
                {
                   
                    _dayBtnController.SetState(DayBtnState.Upcomingday, today);
                }
            }          
        }
        public int GetMonthStartDay(MonthName _currentMonth,DayName _yearStartDay)
        {           
            int daysAdded = ((((int)_currentMonth)-1)*30);
            int monthStartDay = CalendarService.Instance.GetDayInRange( daysAdded+ (int)_yearStartDay);
           // Debug.Log("monthStartDay" + (DayName)monthStartDay + "int "+ monthStartDay );
            return monthStartDay;      
        }

       public GameObject GetPanelInScene(PanelInScene _panelInScene)
        {
            switch (_panelInScene)
            {
                case PanelInScene.Day: return dayPanel;
                case PanelInScene.Week: return weekPanel;
                case PanelInScene.Month: return monthPanel;
                case PanelInScene.Fame: return famePanel;
                case PanelInScene.None: return null;

                default: return null; 
            }
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                OnPanelExit(GetPanelInScene(panelInScene));
            }
        }

    }
    public enum PanelInScene
    {
        None,
        Day,
        Week, 
        Month, 
        Fame, 
    }
}

