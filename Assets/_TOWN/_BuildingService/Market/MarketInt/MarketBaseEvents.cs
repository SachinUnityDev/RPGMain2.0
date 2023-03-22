using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Town
{
    public class MarketBaseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler    
    {
        [Header("To be ref")]
        [SerializeField] Sprite dayN;
        [SerializeField] Sprite dayHL;
        [SerializeField] Sprite nightN;
        [SerializeField] Sprite nightHL;

        [Header("Not be ref")]
        Image btnImg;
        public MarketView marketView;
        TimeState timeState;

        void Awake()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

        public void Init(MarketView marketView)
        {
            this.marketView = marketView;
            timeState = CalendarService.Instance.currtimeState;
            SetSpriteN();
        }
        void SetSpriteN()
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = dayN;
            else
                btnImg.sprite = nightN;
        }
        void SetSpriteHL()
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = dayHL;
            else
                btnImg.sprite = nightHL;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetSpriteHL();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetSpriteN();
        }
    }
}