using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI; 
using Common; 

namespace Town
{
    public class BuildView : MonoBehaviour
    {
        [Header("Building CLICKS")]

        [Header("HOUSE")]
        [SerializeField] Button houseDayBtn;
        [SerializeField] Button houseNightBtn;

        [Header("TEMPLE")]
        [SerializeField] Button templeDayBtn;
        [SerializeField] Button templeNightBtn;

        [Header("MARKET PLACE")]
        [SerializeField] Button marketDayBtn;
        [SerializeField] Button marketNightBtn;


        [Header("Building Panels")]
        [SerializeField] Transform housePanel;
        [SerializeField] Transform marketPanel;
        [SerializeField] Transform templePanel;
        
        // use Ipanel to open and close 

        void Start()
        {
            templeDayBtn.onClick.AddListener(OnTempleBtnPressed);
            templeNightBtn.onClick.AddListener(OnTempleBtnPressed);

            marketDayBtn.onClick.AddListener(OnMarketBtnPressed);
            marketNightBtn.onClick.AddListener(OnMarketBtnPressed);

            houseDayBtn.onClick.AddListener(OnHouseBtnPressed);
            houseNightBtn.onClick.AddListener(OnHouseBtnPressed);

        }

        void OnTempleBtnPressed()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Day)
                UIControlServiceGeneral.Instance.TogglePanel(templePanel.gameObject, true);
            //locked at night  
        }
     
        void OnMarketBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanel(marketPanel.gameObject, true);
            if (CalendarService.Instance.currtimeState == TimeState.Day)
            {
                marketPanel.GetChild(0).gameObject.SetActive(true);
                marketPanel.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                marketPanel.GetChild(0).gameObject.SetActive(false);
                marketPanel.GetChild(1).gameObject.SetActive(true);
            }
        }
     
        void OnHouseBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanel(housePanel.gameObject, true);
            if (CalendarService.Instance.currtimeState == TimeState.Day)
            {
                housePanel.GetChild(0).gameObject.SetActive(true);
                housePanel.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                housePanel.GetChild(0).gameObject.SetActive(false);
                housePanel.GetChild(1).gameObject.SetActive(true);
            }
        }
     

    }
}