using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class CenterBGClick2Close : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]float timeEnabled = 0f;
        [SerializeField] float timeDelay = 0.5f; 
        public void Init()
        {
            Image img = GetComponent<Image>();
            img.enabled = true;
            timeEnabled = Time.time; 
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if((Time.time -timeEnabled) >timeDelay)
            CalendarService.Instance.calendarUIController.CloseAllPanel();
        }
    }
}