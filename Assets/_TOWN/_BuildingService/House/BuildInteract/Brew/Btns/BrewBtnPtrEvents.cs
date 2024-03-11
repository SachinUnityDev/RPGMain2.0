using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class BrewBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image img;
        [Header("Color")]
        [SerializeField] Color colorN;
        [SerializeField] Color colorNA;

        [SerializeField] bool isClickable = false; 
        BrewSlotView brewSlotView; 
        void Start()
        {
            InvService.Instance.OnItemRemovedFrmComm += OnItemRemoved; 
        }

        void OnItemRemoved(Iitems item)
        {
            ChgStatus(); 
        }

        public void Init(BrewSlotView brewSlotView)
        {
            this.brewSlotView = brewSlotView;
            ChgStatus(); 
        }
        void ChgStatus()
        {
            if (brewSlotView.AreIngredSufficient())
            {
                isClickable= true;
                img.color = colorN;
            }
            else
            {
                isClickable= false;
                img.color = colorNA;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isClickable) 
            {
              bool isClickable = brewSlotView.OnBrewBtnPressed();   
                ChgStatus();
            }
        }
    }
}