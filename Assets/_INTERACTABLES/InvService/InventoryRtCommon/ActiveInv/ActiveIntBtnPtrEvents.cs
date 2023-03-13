using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Common;

namespace Interactables
{
    public class ActiveIntBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
                                                                
    {

        [SerializeField] Transform title;
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;


        Image img; 
        [SerializeField] float scaleHL;
        [Header("Right inv View: to be ref")]
        [SerializeField] InvRightViewController InvRightView; 


        public bool isClicked= false; 
        public void OnPointerEnter(PointerEventData eventData)
        {
            img.color = colorHL;
            transform.DOScale(scaleHL, 0.1f); 
            title.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClicked)
            {
                ClickState(); 
            }
            else
            {
                UnClickState();
            }
            title.gameObject.SetActive(false);
        }
        public void ClickState()
        {
 
            isClicked = true;
            img.color = colorHL;
            transform.DOScale(scaleHL, 0.1f);
        }

        public void UnClickState()
        {
            isClicked = false;
            img.color = colorN;
            transform.DOScale(1, 0.1f);
        }

        void Start()
        {
            scaleHL = 1.1f;
            title = transform.GetChild(0); 
            
            title.gameObject.SetActive(false);
            img = gameObject.GetComponent<Image>();
            UnClickState();           
        }

 
    }



}
