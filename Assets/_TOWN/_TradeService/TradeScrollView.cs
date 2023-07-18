using Common;
using DG.Tweening;
using Interactables;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] int maxSlots;
        TradeView tradeView;
        private void Start()
        {
            InvService.Instance.OnDragResult += OnDragResult2TradeScroll;
            leftBtn.onClick.AddListener(OnLeftBtnPressed); 
            rightBtn.onClick.AddListener(OnRightBtnPressed);
        }
        public void InitSlotView(List<Iitems> allItems, TradeView tradeView)
        {
            this.tradeView= tradeView;
            ClearSlotView();
            this.allItems = allItems;
            
            foreach (Iitems item in allItems)
            {
                AddItem2InVView(item, false);
                item.invSlotType = SlotType.TradeScrollSlot; 
            }
            maxSlots = FilledSlotCount()/4;
            if (FilledSlotCount() % 4 !=0)
                maxSlots += 1;

            if (FilledSlotCount() <= 4)
            {
                rightBtn.gameObject.SetActive(false);
                leftBtn.gameObject.SetActive(false);
            }
            else
            {
                rightBtn.gameObject.SetActive(true);
                leftBtn.gameObject.SetActive(true);
            }

            foreach (Transform child in transform)
            {
                child.GetComponent<TradeScrollItemSlotController>().InitTradeScrollSlot(tradeView, this); 
            }
            index = 0;
            PopulateSlots();
        }

        public void ClearSlotView()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<iSlotable>().ClearSlot();
            }
            index = 0; 
        }

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            if (index == 0)
            {
                index = maxSlots-1;
                //if (index < 0)
                //    index = 0; 
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
            if (index == maxSlots-1)
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

        public void OnDragResult2TradeScroll(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.TradeScrollSlot)
            {
                Debug.Log(result + "Drag fail result Invoked trade scroll");
                Transform slotParent = itemsDragDrop.slotParent;
                itemsDragDrop.transform.DOMove(slotParent.position, 0.1f);

                itemsDragDrop.transform.SetParent(slotParent);
                RectTransform cloneRect = itemsDragDrop.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector3.zero;
                cloneRect.localScale = Vector3.one;

                itemsDragDrop.iSlotable.AddItem(itemsDragDrop.itemDragged);
            }
        }
        public bool AddItem2InVView(Iitems item, bool onDrop = true)  // ACTUAL ADDITION 
        {
            bool slotFound = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (iSlotable.ItemsInSlot.Count > 0)
                {
                    if (iSlotable.ItemsInSlot[0].itemName == item.itemName)
                    {
                        if (iSlotable.AddItem(item, onDrop))
                        {
                            slotFound = true;
                            return slotFound;
                        }
                    }
                }
            }
            if (!slotFound)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                    if (iSlotable.AddItem(item, onDrop))
                    {
                        slotFound = true;
                        return slotFound;
                    }
                }
            }
            return slotFound;
        }

        public int FilledSlotCount()
        {
            int count = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (!iSlotable.IsEmpty())
                {
                    count++; 
                }
            }
            return count;
        }
    }
}


//void OnClearSlot()
//{
//    // remove all 

//    for (int i = 0; i < transform.GetChild(0).childCount; i++)
//    {
//        Transform child = transform.GetChild(0).GetChild(i);  // go
//        child.gameObject.GetComponent<TradeScrollItemSlotController>().RemoveAllItems();
//    }
//    // ClearInv();  
//   // InvService.Instance.invMainModel.excessInvItems.Clear();
//}

//void ClearInv()
//{
//    for (int i = 0; i < transform.GetChild(0).childCount; i++)
//    {
//        Transform child = transform.GetChild(0).GetChild(i);  // go
//        child.gameObject.GetComponent<TradeScrollItemSlotController>().ClearSlot();
//    }
//}
//void OnSellAllPressed()
//{
//    // iitem get SO or directly get price
//    //for (int i = 0; i < transform.GetChild(0).childCount; i++)
//    //{
//    //    Transform child = transform.GetChild(0).GetChild(i);  // go
//    //    iSlotable iSlot = child.gameObject.GetComponent<iSlotable>();
//    //    if (!child.gameObject.GetComponent<ExcessItemSlotController>().IsEmpty())
//    //    {
//    //        int count = iSlot.ItemsInSlot.Count;
//    //        Iitems item = iSlot.ItemsInSlot[0];
//    //        if (count > 0)
//    //        {
//    //            CostData costData =
//    //            ItemService.Instance.GetCostData(item.itemType, item.itemName);
//    //            // add to play Eco and dispose item
//    //            int silver = (costData.cost.silver / 3) * count;
//    //            int bronze = (costData.cost.bronze / 3) * count;
//    //            Currency itemSaleVal = new Currency(silver, bronze).RationaliseCurrency();

//    //            EcoServices.Instance.AddMoney2PlayerInv(itemSaleVal);
//    //            iSlot.RemoveAllItems();
//    //        }
//    //    }
//    //}
//}