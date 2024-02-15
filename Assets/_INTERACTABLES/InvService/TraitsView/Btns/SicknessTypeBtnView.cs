using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Interactables
{
    public class SicknessTypeBtnView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteOnHover;
        [SerializeField] Sprite spriteOnClick;

        [SerializeField] bool isClicked = false;
        [SerializeField] Image img;

        [SerializeField] TempTraitType tempTraitType;

        InvTraitsView invTraitsView;
        SicknessBtnView sicknessBtnView; 

        public void Init(InvTraitsView invTraitsView, SicknessBtnView sicknessBtnView)
        {
            this.invTraitsView = invTraitsView;
            this.sicknessBtnView = sicknessBtnView;
            isClicked= false;
          
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
        }
        public void OnClick()
        {
            sicknessBtnView.UnClickall();
            img.sprite = spriteOnClick;
            isClicked = true;
            CharModel charModel = InvService.Instance.charSelectController.charModel;
            invTraitsView.ShowTempTraitType(tempTraitType, charModel);
        }


        void Start()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
        }
    }
}
