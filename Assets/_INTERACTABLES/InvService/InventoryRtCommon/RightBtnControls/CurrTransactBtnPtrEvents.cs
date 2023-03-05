using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Town
{
    public class CurrTransactBtnPtrEvents : MonoBehaviour, IPointerEnterHandler
                                            , IPointerExitHandler, IPointerClickHandler
    {
        public bool isClicked;
        [SerializeField] Image bgImg; 

        private void Start()
        {
            isClicked = false; 
            bgImg = GetComponent<Image>();
            bgImg.DOFade(0, 0.1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {        
            isClicked = !isClicked;
            if(isClicked) 
                OnClicked();
            else
                OnUnClick();    
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            bgImg.DOFade(1, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isClicked)
                bgImg.DOFade(0, 0.1f);
        }
        public void OnClicked()
        {
            isClicked = true;
            bgImg.DOFade(1, 0.1f);
        }
        public void OnUnClick()
        {
            isClicked = false;
            bgImg.DOFade(0, 0.1f);
        }

    }
}