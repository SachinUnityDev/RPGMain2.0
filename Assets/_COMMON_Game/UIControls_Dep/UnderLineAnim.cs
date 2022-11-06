using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


namespace Common
{
  

    // deprecated 
    public class UnderLineAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Transform toScaleX;
        [SerializeField] bool isClicked =false; 
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!isClicked)
                toScaleX.DOScaleX(1, 0.3f);
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(isClicked)
              toScaleX.DOScaleX(0, 0.3f);
        }

        public void UpdateState()
        {
           
        }

        void Start()
        {
            toScaleX = gameObject.transform.GetChild(1);
            toScaleX.DOScaleX(0, 0.1f);
        }


    }
}