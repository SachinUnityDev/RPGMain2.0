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

        [Header("Day Btns")]
        [SerializeField] Sprite endDayNormal;
        [SerializeField] Sprite endDayHL;

        [Header("Night Btns")]
        [SerializeField] Sprite endNightNormal;
        [SerializeField] Sprite endNightHL;

        [SerializeField] TimeState timeState;

        [SerializeField] Image btnImg; 
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = endDayHL;
            else if (timeState == TimeState.Night)
                btnImg.sprite = endNightHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = endDayNormal;
            else if (timeState == TimeState.Night)
                btnImg.sprite = endNightNormal;
        }

        void Start()
        {
            btnImg = GetComponent<Image>(); 
        }


    }




}
