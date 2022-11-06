using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; 
namespace Common
{
    public class AlphaChgOnHoverEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] float initAlpha;
        [SerializeField] float finalAlpha;

        [Header("TO BE REF....")]
        [SerializeField] Image img; 

        private void Start()
        {           
            initAlpha = initAlpha / 255;
            finalAlpha = finalAlpha / 255;
            img.DOFade(initAlpha, 0.1f);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            img.DOFade(finalAlpha, 0.1f); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.DOFade(initAlpha, 0.1f);

        }



    }
}


