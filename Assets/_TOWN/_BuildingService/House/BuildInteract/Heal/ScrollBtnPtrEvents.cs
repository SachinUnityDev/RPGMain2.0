using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Town
{


    public class ScrollBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHover;
        [SerializeField] Sprite btnClick;

        [SerializeField] Image btnImg; 


        public void OnPointerClick(PointerEventData eventData)
        {
            btnImg.sprite = btnClick;   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            btnImg.sprite = btnHover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            btnImg.sprite = btnN;
        }

        private void Awake()
        {
            btnImg = GetComponent<Image>();
            btnImg.sprite = btnN; 
        }


    }
}