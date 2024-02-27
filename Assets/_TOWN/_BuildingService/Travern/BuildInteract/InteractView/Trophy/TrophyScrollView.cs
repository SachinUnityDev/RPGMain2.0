using Common;
using Interactables;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Town
{


    public class TrophyScrollView : MonoBehaviour 
    {
        [Header("Select Container")]
        [SerializeField] Transform selectContainer;
        
        [Header("Page scroll related")]        
        List<Iitems> allItems = new List<Iitems>();
        List<Iitems> slotItems = new List<Iitems>();
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
        [SerializeField] TavernSlotType tavernSlotType; 
   
        void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            returnBtn.onClick.AddListener(OnReturnBtnPressed);  
        }
        public void InitScrollPage(TrophyView trophyView, TavernSlotType tavernSlotType
                                                                        , List<Iitems> slotItems)
        {
            this.slotItems.Clear();
            index = 0; 
            this.trophyView= trophyView;
            this.tavernSlotType = tavernSlotType;
            this.slotItems.AddRange(slotItems.Distinct().ToList());
            FillItemsinSlots();
        }
        void FillItemsinSlots()
        {
            if (slotItems.Count == 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    TrophyScrollSlotController slotController
                      = selectContainer.GetChild(k).GetComponent<TrophyScrollSlotController>();
                    slotController.ClearSlot();
                }
                return; 
            }

            if(slotItems.Count% 3 == 0)
                maxIndex = (slotItems.Count/3)-1; // 0 factor in list    
            else
                maxIndex = (slotItems.Count/3) ;
            if(maxIndex < 1) // range 0..n
            {
                leftBtn.gameObject.SetActive(false);
                rightBtn.gameObject.SetActive(false);
            }
            else
            {
                leftBtn.gameObject.SetActive(true);
                rightBtn.gameObject.SetActive(true);
            }

            int startIndex = index * 3;
            int endIndex = startIndex + 3;    
            
            int j = 0; 
            for (int i = startIndex; i < endIndex; i++)
            {
                TrophyScrollSlotController slotController
                        = selectContainer.GetChild(j).GetComponent<TrophyScrollSlotController>();

                slotController.InitSlotView(trophyView);
                if (i < slotItems.Count)
                    slotController.AddItem(slotItems[i]);                
                else
                    slotController.ClearSlot();
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

   
    }
}

//allItems.Clear();   
//allItems.AddRange(InvService.Instance.invMainModel.GetItemsFrmCommonInv(ItemType.TradeGoods));
//allItems.AddRange(InvService.Instance.invMainModel.GetItemsFrmStashInv(ItemType.TradeGoods));

//foreach (Iitems item in allItems) 
//{

//    ITrophyable iTrophy = item as ITrophyable;
//    if(iTrophy.tavernSlotType == tavernSlotType)
//    {
//        allSelect.Add(item);    
//    }
//}
//if(allItems.Count > 0)       