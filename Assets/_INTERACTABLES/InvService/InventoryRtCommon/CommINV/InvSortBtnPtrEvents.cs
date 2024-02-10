using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common
{


    public class InvSortBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler  
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        Image img;
        [SerializeField] bool isClicked;
        [SerializeField] int index; 

        InvSortingView invSortingView;
        [SerializeField] ItemGrp itemGrp; 
        void Start()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
            isClicked = false;
            index = transform.GetSiblingIndex();
        }

        public void InitSortBtns(InvSortingView invSortView, ItemGrp itemGrp)
        {
            this.invSortingView = invSortView;
            this.itemGrp = itemGrp;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            isClicked = !isClicked;
            invSortingView.OnItemGrpSelected(itemGrp, isClicked);
            if (isClicked)
                OnClick();
        }
        void OnClick()
        {
            isClicked = true;
            img.sprite = spriteHL;
        }
        public void UnClick()
        {
            isClicked= false;
            img.sprite = spriteN; 
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!isClicked) 
            img.sprite = spriteN;
        }
    }
}