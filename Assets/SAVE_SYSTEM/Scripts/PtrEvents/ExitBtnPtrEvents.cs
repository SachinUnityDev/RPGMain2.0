using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Common
{


    public class ExitBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        TextMeshProUGUI btntext;
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL; 
        public void OnPointerClick(PointerEventData eventData)
        {
            // Auto Save and Close 

            // application close

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            btntext.color = colorHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            btntext.color = colorN;
        }

        void Start()
        {
            btntext = GetComponentInChildren<TextMeshProUGUI>();
            btntext.color = colorN;
        }


    }


}

