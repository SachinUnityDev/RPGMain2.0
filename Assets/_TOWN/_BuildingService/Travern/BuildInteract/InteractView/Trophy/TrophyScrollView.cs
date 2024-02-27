using Common;
using Interactables;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

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

        [Header("Fame Yield transform")]
        [SerializeField] Transform fameYieldTrans;
        [SerializeField] Transform buffTxtTrans;

        [Header("Global var")]
        [SerializeField] int index;
        [SerializeField] int maxIndex;
        TrophyView trophyView;
        [SerializeField] TavernSlotType tavernSlotType;
        [SerializeField] string trophyStr = ""; 

        void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            returnBtn.onClick.AddListener(OnReturnBtnPressed);  
        }
        public void InitScrollPage(TrophyView trophyView, TavernSlotType tavernSlotType
                                                                        , List<Iitems> slotItems)
        {
            HideFameNBuff(); 
            this.slotItems.Clear();
            index = 0; 
            this.trophyView= trophyView;
            this.tavernSlotType = tavernSlotType;
            foreach (Iitems item in slotItems)
            {
                if(!this.slotItems.Any(t=>t.itemName ==item.itemName && t.itemType == item.itemType))
                {
                    this.slotItems.Add(item);
                }
            }
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

                slotController.InitSlotView(trophyView, this);
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


        public void ShowFameNBuff(Iitems item)
        {
            int fameYield = 0; 
            ITrophyable itrophy = item as ITrophyable;
            TGBase tgBase = item as TGBase;
            fameYieldTrans.gameObject.SetActive(true);
            buffTxtTrans.gameObject.SetActive(true);
            if (item != null)
            {
                fameYield += itrophy.fameYield;
                trophyStr = tgBase.allDisplayStr[0];
            }
            else if (item == null)
            {
                trophyStr = "";
                fameYield = 0;
            }
            buffTxtTrans.GetComponentInChildren<TextMeshProUGUI>().text = trophyStr.ToString();
            fameYieldTrans.GetChild(1).GetComponent<TextMeshProUGUI>().text = fameYield.ToString();
        }
        public void HideFameNBuff()
        {
            fameYieldTrans.gameObject.SetActive(false);
            buffTxtTrans.gameObject.SetActive(false);
        }
    }
}
