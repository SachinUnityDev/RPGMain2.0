using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Common;


namespace Town
{
    public class RestInteractView : MonoBehaviour, IPanel
    {
        [SerializeField] Button endDayBtn;
        [SerializeField] Button closeBtn;

        // on press close the day event in calendar
        string buffStr = "60% chance for Well Rested";
        
      
        TimeState timeState; 

        void Awake()
        {
            endDayBtn.onClick.AddListener(OnEndDayPressed);
            closeBtn.onClick.AddListener(OnClosePressed);

        }

        public void OnEndDayPressed()
        {
            timeState = CalendarService.Instance.currtimeState;
            int day = CalendarService.Instance.dayInYear;
            CalendarService.Instance.On_EndTimeStateClick();
        
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

        void OnClosePressed()
        {
          UIControlServiceGeneral.Instance.TogglePanel(gameObject,false);
        }
        public void Init()
        {
           
        }
    }
}