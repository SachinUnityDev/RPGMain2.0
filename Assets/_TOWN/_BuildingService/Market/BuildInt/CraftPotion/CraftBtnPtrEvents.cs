using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class CraftBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler

    {
        [Header(" TBR")]
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnNA;
        [SerializeField] TextMeshProUGUI onHoverTxt; 

        [Header("Global Var")]
        [SerializeField] Image btnImg;
        public bool isClickable;

        CraftView craftView; 
        void Awake()
        {
            btnImg = GetComponent<Image>();
        }
        public void InitCraftBtnPtrEvents(CraftView craftView)
        {
            this.craftView = craftView;

        }

        public void SetState(bool isClickable)
        {
            this.isClickable = isClickable;
            // change sprite 
            if(isClickable)            
                btnImg.sprite = btnN;
            else            
                btnImg.sprite = btnNA;
            onHoverTxt.gameObject.SetActive(false);
        }
        public void OnPointerClick(PointerEventData eventData)
        {           
            if (isClickable)
            {
                craftView.OnCraftPressed(); 
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isClickable)
            {
                btnImg.sprite = btnHL;
                onHoverTxt.gameObject.SetActive(true);
            }                
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
            {
                btnImg.sprite = btnN;
                onHoverTxt.gameObject.SetActive(false);
            }                
        }
    }
}