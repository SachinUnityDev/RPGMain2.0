using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Town
{
    public class FameBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] float prevClickTime = 0f;  


        public void OnPointerClick(PointerEventData eventData)
        {
            if(Time.time - prevClickTime > 0.5f)
            {
                CalendarService.Instance.calendarUIController.OnFameBtnClick();
                prevClickTime = Time.time;
            }            
        }
    }
}