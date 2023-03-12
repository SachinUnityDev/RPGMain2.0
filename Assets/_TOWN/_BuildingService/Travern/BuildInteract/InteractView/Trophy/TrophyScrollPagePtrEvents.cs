using Common;
using Interactables;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace Town
{


    public class TrophyScrollPagePtrEvents : MonoBehaviour 
    {
        [Header("Select Container")]
        [SerializeField] Transform selectContainer;
        
        [Header("Page scroll related")]        
        List<Iitems> allItems = new List<Iitems>();
        List<Iitems> allSelect = new List<Iitems>();
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;

        [SerializeField] float prevLeftClick = 0f;
        [SerializeField] float prevRightClick = 0f;

        [Header("return btn")]
        [SerializeField] Button returnBtn; 

        [Header("Global var")]
        [SerializeField] int index;
        [SerializeField] int maxIndex;
        TrophyView trophyView;
   
        void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            returnBtn.onClick.AddListener(OnReturnBtnPressed);  
        }
        public void InitScrollPage(TrophyView trophyView, TavernSlotType tavernSlotType)
        {
            this.trophyView= trophyView;    
            allItems.Clear();   
            allItems.AddRange(InvService.Instance.invMainModel.GetItemsFrmCommonInv(ItemType.TradeGoods));
            allItems.AddRange(InvService.Instance.invMainModel.GetItemsFrmStashInv(ItemType.TradeGoods));
          
            foreach (Iitems item in allItems) 
            {
            
                ITrophyable iTrophy = item as ITrophyable;
                if(iTrophy.tavernSlotType == tavernSlotType)
                {
                    allSelect.Add(item);    
                }
            }
            if(allItems.Count > 0)            
            FillItemsinSlots();
        }

        void FillItemsinSlots()
        {
            if(allSelect.Count% 3 == 0)
                maxIndex = (allSelect.Count/3)-1; // 0 factor in list    
            else
                maxIndex = (allSelect.Count/3) ;
            int startIndex = index * 3;
            int endIndex = startIndex + 3;           
            int j = 0; 
            for (int i = startIndex; i < endIndex; i++)
            {
                TrophyScrollSlotController slotController
                        = selectContainer.GetChild(j).GetComponent<TrophyScrollSlotController>();
                slotController.ClearSlot();

                if (i < allSelect.Count)
                    slotController.LoadSlot(allSelect[startIndex]);                
                j++; 
            }

        }
        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            if (index == 0)
            {
                index = maxIndex; // to account for the 3 slots
                FillItemsinSlots();
            }
            else
            {
                --index; FillItemsinSlots();
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if (index == maxIndex)  // to account for the 3 slots 
            {
                index = 0;
                FillItemsinSlots();
            }
            else
            {
                ++index; FillItemsinSlots();
            }
            prevRightClick = Time.time;
        }

        void OnReturnBtnPressed()
        {
            trophyView.DisplaySelectPage();
        }

        public void OnSlotClicked(Iitems item)
        {
            // subscribe to onslotselect
            BuildingIntService.Instance.On_TrophyableTavern(item); 
        }

    }
}