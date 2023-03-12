using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Interactables;
using TMPro;

namespace Town
{
    public class ProvisionOptionsPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Color colorHL;
        [SerializeField] Color colorN;

        [SerializeField] bool isClicked;

        [SerializeField] ProvisionView provisionView;

        public PotionNames potionName;
      

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked) return; 
            isClicked = true;
            OnSelected(); 

        }
        public void OnSelected()
        {
            transform.DOScale(1.15f, 0.1f);           
            transform.GetComponent<TextMeshProUGUI>().DOColor(colorHL, 0.1f); 
            provisionView.OnSelect(potionName, transform.GetSiblingIndex());
           
        }
        public void OnDeSelect(bool isClick = false)  
        {
            transform.DOScale(1f, 0.1f);
            transform.GetComponent<TextMeshProUGUI>().DOColor(colorN, 0.1f);
            if (isClick)
                isClicked = false;  
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isClicked) return;
            transform.DOScale(1.15f, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClicked) return;            
            transform.DOScale(1f, 0.1f);

        }
    }
}