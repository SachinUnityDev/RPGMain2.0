using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace Common
{
    public class GuitarController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        Image btnImg;
        // [SerializeField] GameObject namePlank;

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        // talk .. trade ...each interaction is unique ....


        public void OnPointerEnter(PointerEventData eventData)
        {
            //   namePlank.GetComponent<RectTransform>().DOScale(1, 0.25f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //  namePlank.GetComponent<RectTransform>().DOScale(0, 0.25f);

        }

        void Start()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
            //  namePlank.GetComponent<RectTransform>().DOScale(0, 0.25f);
        }
    }
}
