using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI; 


namespace Town
{
    public class RestInteractView : MonoBehaviour, IPanel
    {
        [SerializeField] Button endDayBtn;


        // on press close the day event in calendar
        string buffStr = "60% chance for Well Rested";
        
      
        TimeState timeState; 

        void Awake()
        {
            endDayBtn.onClick.AddListener(OnEndDayPressed);
        }

        public void OnEndDayPressed()
        {
            timeState = CalendarService.Instance.currtimeState;
            int day = CalendarService.Instance.dayInYear; 
            if(timeState == TimeState.Day)
            {
                CalendarService.Instance.On_StartOfNight(day); 
            }
            else
            {
                CalendarService.Instance.On_StartOfDay(day);
            }
            FillHouseView(); 
        }
        void FillHouseView()
        {
            BuildingIntService.Instance.houseController.houseView.FillHouseBG();             
        }

        void ApplyBuff()
        {
            // apply well rested trait to ABBAS 

        }

        public void Load()
        {
            
        }

        public void UnLoad()
        {
           
        }

        public void Init()
        {
           
        }
    }
}