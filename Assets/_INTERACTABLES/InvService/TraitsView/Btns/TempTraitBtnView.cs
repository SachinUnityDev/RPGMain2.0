using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Interactables
{
    public class TempTraitBtnView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
            OnUnClick(); 
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
                OnClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isClicked)
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
            isClicked = false;
            invTraitsView.sicknessBtnView.HideBtns();

        }
        public void OnClick()
        {
            img.sprite = spriteOnClick;
            isClicked = true;
            invTraitsView.sicknessBtnView.ShowBtns(); 

        }


        void Start()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;

        }

        private void OnDisable()
        {


        }
    }
}
