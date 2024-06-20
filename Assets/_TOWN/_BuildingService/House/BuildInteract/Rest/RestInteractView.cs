using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using TMPro;

namespace Town
{
    public class RestInteractView : MonoBehaviour, IPanel
    {
        [SerializeField] Button endDayBtn;
        [SerializeField] Button closeBtn;
        [SerializeField] TextMeshProUGUI buffTxt;

        [SerializeField] Image hourGlass;
        // on press close the day event in calendar
        [Header("End day buff")]
        string buffstrOnUpgrade = "60% chance for Well Rested upon resting";
        string buffStrBase = "No Chance for buff upon resting";
        [SerializeField] HousePurchaseOptsData houseData;


        TimeState timeState; 

        void Awake()
        {
            endDayBtn.onClick.AddListener(OnEndDayPressed);
            closeBtn.onClick.AddListener(OnClosePressed);
      
        }
        public void OnEndDayPressed()
        {
            timeState = CalendarService.Instance.calendarModel.currtimeState;
            int day = CalendarService.Instance.calendarModel.dayInYear;
            CalendarService.Instance.On_EndDayClick(BuildingNames.House);                
            FillHouseView();    
            
        }
      
        void FillHouseView()
        {
            BuildingIntService.Instance.houseController.houseView.FillBuildBG();             
            FillPanelBg();
        }
        void FillPanelBg()
        {
            CalendarSO calSO = CalendarService.Instance.calendarSO;
            if (CalendarService.Instance.calendarModel.currtimeState == TimeState.Day)
            {// BG
                transform.GetChild(0).GetComponent<Image>().sprite = calSO.restPanelDay;
                // hour Glass
                hourGlass.sprite = calSO.hourGlassDay;
                endDayBtn.GetComponent<Image>().sprite = calSO.endDayBtnN;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = calSO.restPanelNight; ;
                hourGlass.sprite = calSO.hourGlassNight;
                endDayBtn.GetComponent<Image>().sprite = calSO.endNightBtnN;
            }

        }
        //void FillPanelBg()
        //{
        //    CalendarSO
        //    if (CalendarService.Instance.currtimeState == TimeState.Day)
        //    {// BG
        //        transform.GetChild(0).GetComponent<Image>().sprite = CalendarService.Instance.calendarSO.restPanelDay;
        //        // hour Glass
        //        hourGlass.sprite =
        //            CalendarService.Instance.calendarSO.hourGlassDay;

        //    }
        //    else
        //    {
        //        transform.GetChild(0).GetComponent<Image>().sprite =
        //            CalendarService.Instance.calendarSO.restPanelNight; ;
        //        hourGlass.sprite =
        //            CalendarService.Instance.calendarSO.hourGlassNight;
        //    }
        //}
        void FillTheBuffStr()
        {
           houseData = BuildingIntService.Instance.houseController
                                .houseModel.GetHouseOptsInteractData(HousePurchaseOpts.UpgradeBed);

            if (houseData.isUpgraded)
            {
                buffTxt.text = buffstrOnUpgrade;
            }
            else
            {
                buffTxt.text = buffStrBase;
            }
        }  

        public void Load()
        {
            FillHouseView();
            FillTheBuffStr();
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
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