using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Town
{
    public class TempleBaseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("To be ref")]
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;     

        [Header("Not be ref")]
        Image btnImg;
        public TempleView templeView;
        TimeState timeState;

        void Awake()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

        public void Init(TempleView templeView)
        {
            this.templeView = templeView;
            timeState = CalendarService.Instance.currtimeState;
            SetSpriteN();
        }
        void SetSpriteN()
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = btnN;   
        }
        void SetSpriteHL()
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = btnHL;    
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