using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

namespace Common
{
    public class NewGameModeBtns : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
      
        [SerializeField] float animTime = 0.8f;
        [SerializeField] Transform descTransform;
        [SerializeField] TextMeshProUGUI txt;

        private void Start()
        {
            descTransform = gameObject.transform.GetChild(1);
            txt = descTransform.GetComponentInChildren<TextMeshProUGUI>();            
            txt.enabled = false;
            txt.DOFade(0.0f, 0.4f);
        }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    Debug.Log("Campaign button has been clicked");
        //}

        public void OnPointerEnter(PointerEventData eventData)
        {         
                txt.enabled = true; 
                txt.DOFade(1.0f, 0.8f);         
        }
        public void OnPointerExit(PointerEventData eventData)
        {           
                txt.DOFade(0f, 0.1f).OnComplete(()=>txt.enabled = false);
        }
    }




}
