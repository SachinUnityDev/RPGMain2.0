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
        [SerializeField] TextMeshProUGUI netDays;
        
        
        [Header("global var")]
        [SerializeField] AlcoholNames alcoholName;
        [SerializeField] int net_Days = 0;
        [SerializeField] int days_Remaining = 0; 
        [SerializeField] AlcoholSO alcoholSO;

        [SerializeField] int startDay; 
        private void Awake()
        {
            WIPImg = transform.GetChild(0).GetComponent<Image>();
            fillImg = transform.GetChild(1).GetComponent<Image>();
            daysRemaining = transform.GetChild(2).GetComponent<TextMeshProUGUI>();  
            netDays = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        }
        private void Start()
        {
            InitBrewWIP();
            CalendarService.Instance.OnStartOfDay += OnDayChange;

        }
        public void InitBrewWIP()
        {
            WIPImg.gameObject.SetActive(false);
            fillImg.gameObject.SetActive(false);
            daysRemaining.gameObject.SetActive(false);
            netDays.gameObject.SetActive(false);
        }

        public bool IsSlotFilled()
        {
            return isFilled; 
        }

        public void StartBrewWIP(BrewSlotView brewSlotView)
        {
            alcoholName = brewSlotView.alcoholName; 
            AlcoholSO alchoholSO = ItemService.Instance.GetAlcoholSO(alcoholName);
            WIPImg.sprite = alchoholSO.iconSprite;
            
            fillImg.fillAmount = 0;
            net_Days = Random.Range(alcoholSO.minTime, alchoholSO.maxTime+1);
            days_Remaining = net_Days; 

            daysRemaining.text = days_Remaining.ToString();
            netDays.text = netDays.ToString();
            startDay = CalendarService.Instance.dayInGame; 


            WIPImg.gameObject.SetActive(true);
            fillImg.gameObject.SetActive(true);
            daysRemaining.gameObject.SetActive(true);
            netDays.gameObject.SetActive(true);
        }

        private void OnDayChange(int day)
        {       
            days_Remaining--;
            daysRemaining.text = days_Remaining.ToString();
            fillImg.fillAmount = days_Remaining / net_Days;
        }
    }
}