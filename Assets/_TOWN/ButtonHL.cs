using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening; 

namespace Town
{
    public class ButtonHL : MonoBehaviour, IPointerEnterHandler
    {
        /// <summary>
        /// 
        /// </summary>
        Image btnImg;

        [Header("Display Plank")]
        [SerializeField] Transform plankTransform;

        void Start()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
            plankTransform.DOScale(0, 0.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Sequence popUpSeq = DOTween.Sequence();

            popUpSeq
                .Append(plankTransform.DOScale(1, 0.1f))
                .AppendInterval(1.5f)
                .Append(plankTransform.DOScale(0, 0.1f))
                ;
            popUpSeq.Play();

        }
    }


}
