using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Town
{
    public class JobView : MonoBehaviour, IPanel
    {
        JobModel jobModel;

        [Header("RANK TBR")]
        [SerializeField] Transform RankIconContainer;
        [SerializeField] TextMeshProUGUI rankDesc;

        [Header("Exp")]
        [SerializeField] JobExpBarPtrEvents jobExpPtrEvents; 


        [Header("Last Attend")]
        [SerializeField] TextMeshProUGUI lastAttendedDate;
        [SerializeField] TextMeshProUGUI warningTxt;

        [Header("Attend to Job btn")]
        [SerializeField] AttendJobBtn attendJobBtn; 

        private void Start()
        {
            
        }
        public void InitJobView(JobModel jobModel)
        {
            this.jobModel= jobModel;

            Load();
            attendJobBtn.Init(this); 
            FillJobView(); 
        }

        void FillJobView()
        {
            FillRank(); 
            FillDate();
            FillExpbar();
        }
        void FillExpbar()
        {
            jobExpPtrEvents.InitJobExp(jobModel); 
        }

        void FillRank()
        {
            JobRank jobRank = jobModel.currJobRank;

            for (int i = 0; i < RankIconContainer.childCount; i++)
            {
                if (i <= (int)jobRank)
                {
                    RankIconContainer.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    RankIconContainer.GetChild(i).gameObject.SetActive(false);
                }
            }
            rankDesc.text = jobRank.ToString(); 
        }
        void FillDate()
        {
            MonthName monthName = CalendarService.Instance.calendarModel.currentMonth; 
            MonthSO monthSO = CalendarService.Instance.GetMonthSO(monthName);
            string monthNameShort = monthSO.GetMonthNameShort(monthName);
            int todayInYr = CalendarService.Instance.calendarModel.dayInYear;
            int diff = (todayInYr) - jobModel.dayInYrPlayed; 
            if (diff == 1)
            {
                lastAttendedDate.text = "Yesterday"; 
            }
            if (diff > 1 && diff <=2)
            {
                lastAttendedDate.text = $"{diff} days before";
            }
            else
            {
                int monthStartday = CalendarService.Instance.calendarUIController.
                                         GetMonthStartDay(monthName, CalendarService.Instance.calendarModel.startOfGameDayName);
                int dayInMonth = todayInYr- monthStartday;
                lastAttendedDate.text = $"{dayInMonth} of" +monthNameShort;
            }
            
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(gameObject, true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
           
        }
    }
}