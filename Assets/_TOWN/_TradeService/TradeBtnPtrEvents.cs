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
            if(tradeView.tradeModel.allSelectItems.Count > 0 && tradeSelectView.IsTradeClickableMoneyChK())
            {
                isClickable = true;
                img.sprite = spriteN;
            }
            else
            {
                isClickable = false;
                img.sprite = spriteUnClickable; 
            }
        }

        public void OnItemSelectORUnSelect()
        {           
            if (tradeSelectView.IsTradeClickableMoneyChK())
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
            if (tradeSelectView.IsTradeClickableMoneyChK() && isClickable)
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
            else
            {
                img.sprite = spriteUnClickable;
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
                img.sprite = spriteUnClickable; 
            }
        }

        void Awake()
        {
            img = GetComponent<Image>();
        }

        
    }
}