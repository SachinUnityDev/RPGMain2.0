using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common;

namespace Intro
{
    public class ButtonTextHL : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Color HLColor;
        [SerializeField] Color startColor;
        
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponentInParent<IPanel>().UnLoad();
    }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = HLColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = startColor;

        }

        void Start()
        {
            startColor = gameObject.GetComponent<TextMeshProUGUI>().color;
        }

        private void OnEnable()
        {
            TextMeshProUGUI txt = gameObject.GetComponent<TextMeshProUGUI>();
              
            txt.DOFade(0.2f, 1.8f).SetLoops(-1, LoopType.Yoyo);

        }

     

        private void OnDisable()
        {
            
        }

        private void Update()
        {
            

        }
    }



}
