using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class DrinkBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
    {
        [SerializeField] Image img;
        [Header("Color")]
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;
        [SerializeField] Color colorNA;

        [SerializeField] bool isClickable = false;
        BrewSlotView brewSlotView;
        BrewReadySlotPtrEvents brewReadySlotPtrEvents; 
        void OnEnable()
        {
           img= GetComponent<Image>();
        }
        public void Init(BrewSlotView brewSlotView, BrewReadySlotPtrEvents brewReadySlotPtrEvents)
        {
            this.brewSlotView = brewSlotView;
            this.brewReadySlotPtrEvents = brewReadySlotPtrEvents;
            SetState();
        }
        void SetState()
        {  
            if (brewReadySlotPtrEvents.readyQuantity > 0)
            {
                isClickable = true;
                img.color = colorN;
            }
            else
            {
                isClickable = false;
                img.color = colorNA;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isClickable)
            {
                brewSlotView.OnDrinkBtnPressed(); 
                SetState();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isClickable)
                img.color = colorHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
                img.color = colorN;
        }

        
    }
}