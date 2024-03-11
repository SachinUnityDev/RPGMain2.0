using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class DrinkBtnPtrEvents : MonoBehaviour
    {
        [SerializeField] Image img;
        [Header("Color")]
        [SerializeField] Color colorN;
        [SerializeField] Color colorNA;

        [SerializeField] bool isClickable = false;
        BrewSlotView brewSlotView;
        void Start()
        {
           
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
                isClickable = true;
                img.color = colorN;
            }
            else
            {
                isClickable = false;
                img.color = colorNA;
            }
        }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (isClickable)
        //    {
        //        bool isClickable = brewSlotView.OnBrewBtnPressed();
        //        ChgStatus();
        //    }
        //}
    }
}