using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class TradeBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHovered; 
        [SerializeField] Sprite spriteClicked;
        [SerializeField] Sprite spriteUnClickable;
        [SerializeField] Image img; 

        TradeView tradeView; 
        TradeSelectView tradeSelectView;

        public bool isClickable = false; 

        public void InitTradeBtnEvents(TradeView tradeView, TradeSelectView tradeSelectView)
        {
            this.tradeView = tradeView;
            this.tradeSelectView = tradeSelectView;
        }

        public void OnItemSelectORUnSelect()
        {           
            if (tradeSelectView.IsTradeClickable())
            {
                isClickable= true;
                img.sprite = spriteN;
            }
            else
            {
                isClickable= false;
                img.sprite = spriteUnClickable;

            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (tradeSelectView.IsTradeClickable() && isClickable)
            {   
                tradeView.OnTradePressed();
                img.sprite = spriteClicked;
            }
            else
            {
                img.sprite = spriteUnClickable;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isClickable)
            {
                img.sprite = spriteHovered; 
            } 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
            {
                img.sprite = spriteN; 
            }
        }

        void Awake()
        {
            img = GetComponent<Image>();
        }

        
    }
}