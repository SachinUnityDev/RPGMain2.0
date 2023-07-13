using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class TradeScrollView : MonoBehaviour
    {

        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;

        [SerializeField] float prevLeftClick = 0f;
        [SerializeField] float prevRightClick = 0f;

        [SerializeField] int index;
        List<Iitems> allItems;
        private void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed); 
            rightBtn.onClick.AddListener(OnRightBtnPressed);
        }
        public void InitSlotView(List<Iitems> allItems)
        {
            // fill items in the slots
            this.allItems = allItems; 

        }

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            if (index == 0)
            {
                index = (transform.childCount/4) - 1;
                PopulateSlots();
            }
            else
            {
                --index; PopulateSlots();
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if (index == (transform.childCount/4) - 1)
            {
                index = 0;
                PopulateSlots();
            }
            else
            {
                ++index; PopulateSlots();
            }
            prevRightClick = Time.time;
        }

        void PopulateSlots()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int minVal = index * 4;
                int maxVal = index * 4 + 4; 
                if(i< maxVal && i>= minVal)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}