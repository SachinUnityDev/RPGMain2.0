using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


namespace Common
{
    public class PopUpNColorChg : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        
        [Header("Pop up Anim related")]
        RectTransform rectTranform;
        float animTime = 0.15f;
        float scaleValue = 1.25f;
        
        [Header("Color Change related")]
        [SerializeField] TextMeshProUGUI textContent;
        [SerializeField] Color colorNormal; 
        [SerializeField] Color colorHL;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(scaleValue, animTime);
            textContent.color = colorHL;              
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1, animTime);
            textContent.color = colorNormal;
        }

        void Start()
        {           
            textContent = gameObject.GetComponentInChildren<TextMeshProUGUI>();           
            rectTranform.DOScale(1, animTime);
            textContent.color = colorNormal; 
        }

    }



}

