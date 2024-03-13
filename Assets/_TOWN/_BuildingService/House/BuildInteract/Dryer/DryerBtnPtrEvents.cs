using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Interactables;

namespace Town
{
    public class DryerBtnPtrEvents : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header(" TBR")]
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnNA;
        [SerializeField] TextMeshProUGUI onHoverTxt;

        [Header("Global Var")]
        [SerializeField] Image btnImg;
        public bool isClickable;

        DryerView dryerView;
        DryerSlotView dryerSlotView;
        Iitems itemSelect; 
        void Awake()
        {
            btnImg = GetComponent<Image>();
        }
        public void InitDryerBtnPtrEvents(DryerView dryerView, DryerSlotView dryerSlotView, Iitems item)
        {
            this.dryerView = dryerView;
            this.dryerSlotView = dryerSlotView;
            itemSelect= item;
            UpdateState();
        }
        void UpdateState()
        {
            if(itemSelect == null || BuildingIntService.Instance.houseController.houseModel.slotSeq >= dryerView.MAX_SLOT_SIZE)
            {
                isClickable= false;
                btnImg.sprite = btnNA;
            }
            else
            {
                isClickable= true;
                btnImg.sprite = btnN;
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClickable)
            {
                dryerView.OnDryerPressed();
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
            }
            onHoverTxt.gameObject.SetActive(false);
        }
    }
}