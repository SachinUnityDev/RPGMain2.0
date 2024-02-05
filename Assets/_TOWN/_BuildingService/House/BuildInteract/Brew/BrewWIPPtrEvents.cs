using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class BrewWIPPtrEvents : MonoBehaviour
    {
        [Header("WIP")]
        public bool isFilled = false; 

        [Header("Transform Not to be ref")]
        [SerializeField] Image WIPImg;
        [SerializeField] Image fillImg;
        [SerializeField] TextMeshProUGUI daysRemaining;
        [SerializeField] TextMeshProUGUI netDaysTxt;
        
        
        [Header("global var")]
        [SerializeField] AlcoholNames alcoholName;
        [SerializeField] int net_Days = 0;
        [SerializeField] int days_Remaining = 0; 
        [SerializeField] AlcoholSO alcoholSO;
        [SerializeField] BrewSlotView brewSlotView; 

        [SerializeField] int startDay; 
        void Awake()
        {
            WIPImg = transform.GetChild(0).GetComponent<Image>();
            fillImg = transform.GetChild(1).GetComponent<Image>();
            daysRemaining = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            netDaysTxt = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        }
        private void Start()
        {
            // InitBrewWIP();
     
            CalendarService.Instance.OnStartOfCalDay += OnDayChange;
        }
        public void InitBrewWIP()
        {
            if (isFilled) return;
             EmptySlot();
        }

        public bool IsSlotFilled()
        {
            return isFilled; 
        }

        public void StartBrewWIP(BrewSlotView brewSlotView)
        {
            this.brewSlotView = brewSlotView; 
            alcoholName = brewSlotView.alcoholName; 
            alcoholSO = ItemService.Instance.allItemSO.GetAlcoholSO(alcoholName);

            WIPImg.gameObject.SetActive(true);
            fillImg.gameObject.SetActive(true);
            daysRemaining.gameObject.SetActive(true);
            netDaysTxt.gameObject.SetActive(true);

            WIPImg.sprite = alcoholSO.iconSprite;
            
            fillImg.fillAmount = 0;      
            net_Days = UnityEngine.Random.Range(alcoholSO.minTime, alcoholSO.maxTime+1);
            days_Remaining = net_Days; 

            daysRemaining.text = days_Remaining.ToString();
            netDaysTxt.text = net_Days.ToString() +" days";
            startDay = CalendarService.Instance.dayInGame; 
            isFilled = true;
        }

        private void OnDayChange(int day)
        {
            if (!isFilled) return;
            days_Remaining--;
            if(days_Remaining > 0 )
            {
                daysRemaining.text = days_Remaining.ToString();
                fillImg.fillAmount =(1 - ((float)days_Remaining / net_Days));
            }
            else
            {
                EmptySlot();
               
                brewSlotView.readySlotContainer
                        .GetChild(0).GetComponent<BrewReadySlotPtrEvents>().Add2Slot(alcoholSO);               
            }
        }

        void EmptySlot()
        {
            isFilled = false;
            alcoholName = AlcoholNames.None; 
            net_Days = 0;
            days_Remaining= 0;
            
         
            WIPImg.gameObject.SetActive(false);
            fillImg.gameObject.SetActive(false);
            daysRemaining.gameObject.SetActive(false);
            netDaysTxt.gameObject.SetActive(false);
        }

    }
}