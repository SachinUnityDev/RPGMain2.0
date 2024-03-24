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
            CalendarService.Instance.On_EndDayClick(BuildingNames.Tavern);
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
            CalendarSO calSO = CalendarService.Instance.calendarSO; 
            if (CalendarService.Instance.currtimeState == TimeState.Day)
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