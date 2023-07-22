using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;
using TMPro; 

namespace Town
{
    public class RestViewTavern : MonoBehaviour, IPanel
    {
        
        [SerializeField] Button endDayBtn;
        [SerializeField] Button exitBtn;
        [SerializeField] TextMeshProUGUI buffTxt;

        [SerializeField] Image hourGlass;
        // on press close the day event in calendar
        string buffstrOnUpgrade = "60% chance for Well Rested upon resting";
        string buffStrBase = "No Chance for buff upon resting";
        TimeState timeState;

        void Awake()
        {
            endDayBtn.onClick.AddListener(OnEndDayPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);
        }
        public void OnEndDayPressed()
        {
            timeState = CalendarService.Instance.currtimeState;
            int day = CalendarService.Instance.dayInYear;
            CalendarService.Instance.On_EndDayClick();
            FillTavernView();
            BuildingIntService.Instance.tavernController.OnEndDayInTavern();

        }



        void FillTavernView()
        {
            BuildingIntService.Instance.tavernController.tavernView.FillBuildBG();   
            FillPanelBg();
        }
        void FillPanelBg()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Day)
            {// BG
                transform.GetChild(0).GetComponent<Image>().sprite =
                         CalendarService.Instance.calendarSO.restPanelDay;
                // hour Glass
                hourGlass.sprite =
                    CalendarService.Instance.calendarSO.hourGlassDay;

            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite =
                    CalendarService.Instance.calendarSO.restPanelNight; ;
                hourGlass.sprite =
                    CalendarService.Instance.calendarSO.hourGlassNight;
            }

        }
        void FillTheBuffStr()
        {
            HousePurchaseOptsData houseData =
             BuildingIntService.Instance.houseController
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
            FillTavernView();
            FillTheBuffStr();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        void OnExitBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
        public void Init()
        {

        }
    }
}