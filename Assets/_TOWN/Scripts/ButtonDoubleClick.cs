using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 


namespace Common
{
    public class ButtonDoubleClick : MonoBehaviour, IPointerClickHandler
    {
        float clicked = 0;
        float clicktime = 0;
        float clickdelay = 0.5f;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(eventData.clickCount);
            if (eventData.clickCount <= 1)
            {
                CalendarService.Instance.On_ChangeTimeState();

            }
            if (eventData.clickCount > 1)
            {
                CalendarService.Instance.On_EndTimeStateClick();
            }
        }

        //public void OnPointerDown(PointerEventData data)
        //{
        //    Debug.Log(data.clickCount);
        //   if(data.clickCount <= 1)
        //    {
        //      CalendarService.Instance.OnDayChangeClick();

        //    }
        //   if (data.clickCount > 1)
        //    {

        //        CalendarService.Instance.OnDayChangeDoubleClick();

        //    }


        //    //clicked++;
        //    //if (clicked == 1)
        //    //{
        //    //    clicktime = Time.time;
        //    //    Debug.Log("Single Click"); 
        //    //   CalendarService.Instance.OnDayChangeClick();

        //    //}

        //    //if (clicked > 1 && Time.time - clicktime < clickdelay)
        //    //{
        //    //    clicked = 0;
        //    //    clicktime = 0;
        //    //    Debug.Log("Double Click");
        //    //    CalendarService.Instance.OnDayChangeDoubleClick();

        //    //}
        //    //else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;

        //}
    }
}
