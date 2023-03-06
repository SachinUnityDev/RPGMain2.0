using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Common; 

namespace Town
{
    public class EndDayBtnEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TimeState timeState;

        [SerializeField] Image btnImg;
        CalendarSO calendarSO; 
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = calendarSO.endDayBtnNLit; 
            else if (timeState == TimeState.Night)
                btnImg.sprite = calendarSO.endNightBtnLit; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = calendarSO.endDayBtnN;
            else 
                btnImg.sprite = calendarSO.endNightBtnN;
        }

        void Start()
        {
            btnImg = GetComponent<Image>(); 
            calendarSO = CalendarService.Instance.calendarSO;
            timeState = CalendarService.Instance.currtimeState;
            CalendarService.Instance.OnChangeTimeState += ChgBtnBg; 
        }
        void ChgBtnBg(TimeState timeState)
        {
            this.timeState = timeState; 
            if (timeState == TimeState.Day)
                btnImg.sprite = calendarSO.endDayBtnN;
            else if (timeState == TimeState.Night)
                btnImg.sprite = calendarSO.endNightBtnN;
        }
    }




}
