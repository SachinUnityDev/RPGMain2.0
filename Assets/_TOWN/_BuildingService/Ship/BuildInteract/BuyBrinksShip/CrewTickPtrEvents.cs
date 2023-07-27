using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class CrewTickPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteNA;
        Image img;
        public bool isClickable = true;
        CrewDrinkView crewDrinksView;
        Currency availMoney = null;
        int silverNeeded = 0;

        public void InitTickPtrEvents(CrewDrinkView crewDrinksView,
                                            Currency availMoney, int silverNeeded)
        {
            img = GetComponent<Image>();
            this.crewDrinksView = crewDrinksView;
            this.availMoney = availMoney.DeepClone();
            this.silverNeeded = silverNeeded;
            SetTickState();
        }

        public void ChgTickState(bool isClickable)
        {
            this.isClickable= isClickable;  
            SetTickState();
        }
        void SetTickState()
        {           
            if (silverNeeded * 12 < availMoney.BronzifyCurrency()
                && crewDrinksView.CanOfferDrink())
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
            if (isClickable)
            {
                crewDrinksView.OnTickBtnPressed();
            }
            isClickable = false;
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