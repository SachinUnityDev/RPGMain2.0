using Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class ShipBasePtrEvents : BuildBaseEvents
    {
        //[Header("To be ref")]
        //[SerializeField] Sprite dayN;
        //[SerializeField] Sprite dayHL;
        //[SerializeField] Sprite nightN;
        //[SerializeField] Sprite nightHL;

        //[Header("Not be ref")]
        //Image btnImg;
        //public ShipView shipView;
        //TimeState timeState;

        //void Awake()
        //{
        //    btnImg = GetComponent<Image>();
        //    btnImg.alphaHitTestMinimumThreshold = 0.1f;
        //}

        //public void Init(ShipView shipView)
        //{
        //    this.shipView = shipView;
        //    timeState = CalendarService.Instance.currtimeState;
        //    SetSpriteN();
        //}
        //void SetSpriteN()
        //{
        //    if (timeState == TimeState.Day)
        //        btnImg.sprite = dayN;
        //    else
        //        btnImg.sprite = nightN;
        //}
        //void SetSpriteHL()
        //{
        //    if (timeState == TimeState.Day)
        //        btnImg.sprite = dayHL;
        //    else
        //        btnImg.sprite = nightHL;
        //}

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    SetSpriteHL();
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    SetSpriteN();
        //}
    }
}