using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Interactables
{
    public class PermaTraitBtnView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteOnHover;
        [SerializeField] Sprite spriteOnClick;

        [SerializeField] bool isClicked = false;
        [SerializeField] Image img;

        InvTraitsView invTraitsView;


        public void Init(InvTraitsView invTraitsView)
        {
            this.invTraitsView = invTraitsView;
            OnClick(); 
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
                OnClick();          
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!isClicked)
                img.sprite = spriteOnHover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isClicked)
                img.sprite = spriteN;

        }

        public void OnUnClick()
        {
            img.sprite = spriteN;
            isClicked= false;
            
        }
        public void OnClick()
        {
            img.sprite = spriteOnClick;
            isClicked= true;
            CharModel charModel = InvService.Instance.charSelectController.charModel;
            invTraitsView.ShowPermaTrait(charModel); 
        }


        void Start()
        {
            img= GetComponent<Image>();
            img.sprite = spriteN;
        }

        
    }
}