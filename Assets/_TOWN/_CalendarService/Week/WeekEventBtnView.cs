using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Town;
using DG.Tweening;

namespace Common
{

    public class WeekEventBtnView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI onHoverTxt;
        [SerializeField] Color colorHover;
        [SerializeField] Color colorClicked;
        [SerializeField] Color colorDisabled;
        [SerializeField] Color colorN;

        [SerializeField] WeekModel weekModel;
        [SerializeField] WeekEventBase weekBase;

        [SerializeField] Image img; 
        public void Init(WeekModel weekModel, WeekSO weekSO)
        {
            this.weekModel= weekModel;
            transform.GetComponent<Image>().sprite = weekSO.weekIcon;
            img = GetComponent<Image>();
            img.sprite = weekSO.weekIcon;
            if (!weekModel.isDayBonusReceived)            
                img.DOColor(colorN, 0.1f);
            else            
                img.DOColor(colorDisabled, 0.1f);
            
            onHoverTxt.gameObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!weekModel.isDayBonusReceived)
            { 
                weekBase = CalendarService.Instance.weekEventsController.GetWeekEventBase(weekModel.weekName);
                if (weekBase != null)
                    weekBase.OnWeekBonusClicked();
                else
                    Debug.Log("Week base not FOUND!"); 

                weekModel.isDayBonusReceived = true;
                img.DOColor(colorClicked, 0.1f); 
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!weekModel.isDayBonusReceived)
            {
                img.DOColor(colorHover, 0.1f);
                onHoverTxt.gameObject.SetActive(true);
            }
            else
            {
                img.DOColor(colorDisabled, 0.1f);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!weekModel.isDayBonusReceived)
            {
                img.DOColor(colorN, 0.1f);
            }
            else
            {
                img.DOColor(colorDisabled, 0.1f);
            }
            onHoverTxt.gameObject.SetActive(false);
        }
    }
}