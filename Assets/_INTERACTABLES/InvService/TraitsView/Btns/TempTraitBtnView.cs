using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] TextMeshProUGUI text;

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
            if (invTraitsView.sicknessBtnView == null) return;
            isClicked = false;            
            invTraitsView.sicknessBtnView.HideBtns();
            text.gameObject.SetActive(false);
        }
        public void OnClick()
        {            
            img.sprite = spriteOnClick;
            if (invTraitsView.sicknessBtnView == null) return;
            isClicked = true;
            if(invTraitsView.sicknessBtnView)
            invTraitsView.sicknessBtnView.ShowBtns(); 
            text.gameObject.SetActive(true);
        }


        void OnEnable()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
        }

 
    }
}
