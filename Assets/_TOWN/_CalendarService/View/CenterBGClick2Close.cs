using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class CenterBGClick2Close : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            CalendarService.Instance.calendarUIController.CloseAllPanel();
        }
    }
}