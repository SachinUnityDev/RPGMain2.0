using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Combat
{
    public class OnFleeBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [Header("TBR")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        Image img; 
        private void Start()
        {
            img = GetComponent<Image>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            img.sprite = spriteHL;
            
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN; 
        }
    }
}