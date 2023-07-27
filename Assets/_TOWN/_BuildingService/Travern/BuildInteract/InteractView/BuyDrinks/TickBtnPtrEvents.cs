using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{
    public class TickBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteNA;
        Image img; 
       public bool isClickable = true;
        EveryonePagePtrEvents everyonePagePtrEvents;
        Currency availMoney;
        int silverNeeded = 0;
        
        public void InitTickPtrEvents(EveryonePagePtrEvents everyonePagePtrEvents, 
                                            Currency availMoney, int silverNeeded)
        {
            img = GetComponent<Image>();
            this.everyonePagePtrEvents = everyonePagePtrEvents;
            this.availMoney= availMoney;
            this.silverNeeded= silverNeeded;
            SetTickState(); 
        }
        public void ChgTickState(bool isClickable)
        {
            this.isClickable = isClickable;
            SetTickState();
        }
        void SetTickState()
        {
            if(silverNeeded*12 < availMoney.BronzifyCurrency()
                && everyonePagePtrEvents.CanOfferDrink())
            {
                isClickable = true;
                img.sprite = spriteN;
            }
            else
            {
                isClickable = false;
                img.sprite = spriteNA;                
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isClickable)
            {
                everyonePagePtrEvents.OnTickBtnPressed(); 
            }
            isClickable= false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isClickable)
            {
                img.sprite = spriteHL;
            }
            else
            {
                img.sprite = spriteNA;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
            {
                img.sprite = spriteN;
            }
            else
            {
                img.sprite = spriteNA;
            }
        }
    }
}