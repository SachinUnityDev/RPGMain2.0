using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening; 



namespace Common
{
    public class CalendarUIController : MonoBehaviour
    {
        [SerializeField] GameObject TownCenterPanel;

        public GameObject famePanel;
        public GameObject monthPanel;
        public GameObject weekPanel;
        public GameObject dayPanel;
        //public GameObject currentPanel;
        //public GameObject prevPanel;
        [SerializeField] List<GameObject> allPanels = new List<GameObject>();
        public GameObject dayBGPanel;
        public GameObject nightBGPanel;

        public PanelInScene panelInScene; 
         void Start()
         {  
            //START OF THE GAME
            GetMonthStartDay(MonthName.WingOfTheLocust, DayName.DayOfLight);
            allPanels = new List<GameObject>() { famePanel, monthPanel, weekPanel, dayPanel };
            CloseAllPanel();
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


        public void ToggleDayNightUI()
        {
            OnPanelExit(dayPanel);
            if (dayBGPanel.activeSelf)
            {
                nightBGPanel.SetActive(true);
                dayBGPanel.SetActive(false);               
            }
            else
            {
                nightBGPanel.SetActive(false);
                dayBGPanel.SetActive(true);
            }
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
                //panel?.transform.DOLocalMoveY(614, 2).SetEase(Ease.OutQuint);
                //Debug.Log("Entertween");
            }            
        }
        public void OnPanelExit(GameObject panel)
        {
            panel?.transform.DOLocalMoveY(1200, 1).SetEase(Ease.OutQuint);
            panelInScene = PanelInScene.None;
           // Debug.Log("Exittween");
        }

        void CloseAllPanel()
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
            DayName currentDay = (DayName)GetDayInRange(_currentdayInYr +((int)_gameStartDay - 1));            
            
            Debug.Log("Current Day" + currentDay);
            
            dayPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text 
                       = CalendarService.Instance.GetDaySO(currentDay).dayName.ToString();
            Debug.Log(dayPanel.transform.GetChild(1).name);
            RectTransform[] allDayGO = dayPanel.transform.GetChild(1).GetComponentsInChildren<RectTransform>(true);
            
            ToggleBarPanel(allDayGO, (int)currentDay, 8);

            List<string> tipOftheDayList = CalendarService.Instance.GetDaySO(currentDay).tipOfTheDayList;
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

        public void UpdateWeekPanel(WeekName currentWeek)
        {
           // Debug.Log("WeekPanelUpdate" + weekPanel.transform.GetChild(0));

            Transform txtParent = weekPanel.transform.GetChild(0);

            Debug.Log(CalendarService.Instance.GetWeekSO(currentWeek));

            txtParent.GetChild(0).GetComponent<TextMeshProUGUI>().text = CalendarService.Instance.GetWeekSO(currentWeek).weekNameStr; 
            txtParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = CalendarService.Instance.GetWeekSO((WeekName)currentWeek).weekDesc;

        }

        public void UpdateMonthPanel(MonthName _currentMonth, DayName _gameStartDay, int _currentdayInYr)
        {

            Transform yearNameParent = monthPanel.transform.GetChild(0);
            yearNameParent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1339, Year of the Kudan Donkey";
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
        int GetMonthStartDay(MonthName _currentMonth,DayName _yearStartDay)
        {           
            int daysAdded = ((((int)_currentMonth)-1)*30);
            int monthStartDay = GetDayInRange( daysAdded+ (int)_yearStartDay);
           // Debug.Log("monthStartDay" + (DayName)monthStartDay + "int "+ monthStartDay );
            return monthStartDay;      
        }

        int GetDayInRange(int day)
        {            
            if (day > 7)
            {
                day = (day % 7);
                if (day == 0) return 7;  // correction for 7th day start
            }      
            
            return day; 
        }

    
        GameObject GetPanelInScene(PanelInScene _panelInScene)
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

