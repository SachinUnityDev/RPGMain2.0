using Common;
using DG.Tweening;
using Interactables;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class TradeSelectView : MonoBehaviour
    {
        List<Iitems> allItems;
        [SerializeField] TradeModel tradeModel;
        [SerializeField] NPCNames npcName;

        public TradeView tradeView;

        [Header("Inv Money")]
        [SerializeField] TextMeshProUGUI moneyInInvTxt; 
        [SerializeField] DisplayCurrency moneyInInv;

        [Header("Transact Money")]
        [SerializeField] TextMeshProUGUI transactMoneyTxt;
        [SerializeField] DisplayCurrency transactMoney; // Demari to pay Denari to Get

        [Header("Global var")]
        public Currency netVal = new Currency(0,0) ; 
        public Currency invMoney= new Currency(0,0) ;

        private void Start()
        {
            InvService.Instance.OnDragResult += OnDragResult2TradeSelect;         
        }

        #region ITEM SELECT
        public void InitSelectView(NPCNames npcName, TradeView tradeView)
        {
            this.tradeView = tradeView;
            ClearSlotView();
            this.npcName= npcName;         
            tradeModel = TradeService.Instance.tradeController.GetTradeModel(npcName); 
            ClearSlotView();
            foreach (Transform child in transform)
            {
                child.GetComponent<TradeSelectItemSlotController>().InitSelectSlot(this); 
            }
            InitInvMoney();
        } 
        public void Add2EmptyOrFirstSelectSlot(List<Iitems> items)
        {
            bool slotFound = false;
            foreach (Transform child in transform)
            {   
                TradeSelectItemSlotController selectSlot =
                                    child.GetComponent<TradeSelectItemSlotController>();
                if (selectSlot.IsEmpty())
                {
                    foreach(Iitems item in items)
                    {
                       slotFound =  selectSlot.AddItem(item, true);                       
                    }
                    if (slotFound) break;   
                }  
            }
            if(!slotFound)
            {
                TradeSelectItemSlotController slotController =
                    transform.GetChild(0).GetComponent<TradeSelectItemSlotController>();
                slotController.SwapItem2TradeScroll(items[0], true);
                for (int i = 0; i < items.Count-1; i++)
                {
                    slotController.AddItem(items[0]); 
                }
            }
           
        }
        public void AddItem2SelectLs(Iitems item)
        {
            item.invSlotType = SlotType.TradeSelectSlot;
            tradeModel.allSelectItems.Add(item);
            OnItemSelected();  
        }
        public void RemoveItemFrmSelectLs(Iitems item)
        {
            item.invSlotType = SlotType.TradeScrollSlot;
            OnItemDeSelected(item);
            tradeModel.allSelectItems.Remove(item);
            
        }
        public void ClearSlotView()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<iSlotable>().ClearSlot();
            }
            InitTransactCurrViews();
        }
        public void OnDragResult2TradeSelect(bool result, ItemsDragDrop itemsDragDrop)
        {
            // Handle drag fail
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.TradeSelectSlot)
            {
                Debug.Log(result + "Drag fail result Invoked trade Select");
                Transform slotParent = itemsDragDrop.slotParent;
                itemsDragDrop.transform.DOMove(slotParent.position, 0.1f);

                itemsDragDrop.transform.SetParent(slotParent);
                RectTransform cloneRect = itemsDragDrop.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector3.zero;
                cloneRect.localScale = Vector3.one;

                itemsDragDrop.iSlotable.AddItem(itemsDragDrop.itemDragged);
            }
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

        #endregion

        public void InitTransactCurrViews()
        {
            transactMoney.Display(new Currency(0, 0));
            if(tradeView.isBuyBtnPressed)
                transactMoneyTxt.text = "Denari to get";
            else
                transactMoneyTxt.text = "Denari to Pay";
        }

        public void OnItemDeSelected(Iitems item)
        {
            if(item != null)
            {
                if(item.currency  != null)
                {
                    netVal.SubMoney(item.currency);
                    Debug.Log("Currency send" + netVal.bronze + netVal.silver);
                    transactMoney.Display(netVal); 
                }    
                item.currency = null; 
            }     
            if(tradeModel.allSelectItems.Count == 0)
            {
                transactMoney.Display(new Currency(0, 0)); 
            }
            tradeView.tradeBtnPtrEvents.OnItemSelectORUnSelect();
        }

        public void OnItemSelected()
        {
            if(tradeView.isBuyBtnPressed)
            {
                Add2BuyValue();
               
            }
            else
            {
                Add2SellValue();

            }
            tradeView.tradeBtnPtrEvents.OnItemSelectORUnSelect(); 
        }
       void Add2SellValue()
        {
            int val =0; 
            foreach (Iitems item in tradeModel.allSelectItems)
            {
                CostData costData =
                        ItemService.Instance.allItemSO.GetCostData(item.itemType, item.itemName);                
                int bronzifiedCurr = costData.baseCost.BronzifyCurrency() / 2; // 1/2 sell factor
                item.currency = new Currency(0, bronzifiedCurr); 
                val += bronzifiedCurr;
            }   
            netVal = new Currency(0, val);
            Debug.Log(" add 2 sellCurrency " + netVal);
            transactMoney.Display(netVal);
            transactMoneyTxt.text = "Denari to Receive";
        }
       void Add2BuyValue()
        {
            netVal = new Currency(0,0);
            Currency currency = null;
            if (tradeModel.allSelectItems.Count == 0) return;
            foreach (Iitems item in tradeModel.allSelectItems)
            {
                currency = tradeModel.GetCurrPrice(new ItemData(item.itemType, item.itemName));
                item.currency = currency.DeepClone();
                netVal.AddMoney(currency);
            }
            transactMoney.Display(netVal);
            transactMoneyTxt.text = "Denari to Pay";
        }


       public void InitInvMoney()
        {
            invMoney = EcoService.Instance.GetMoneyAmtInPlayerInv();
            moneyInInv.Display(invMoney);
        }
       public bool IsTradeClickableMoneyChK()
        {
            int invBronzify = invMoney.BronzifyCurrency();
            int netvalBronzify = netVal.BronzifyCurrency();
            if (invBronzify >= netvalBronzify)
                return true; 
            else return false;  
        }

       public void  OnTradePressed()
       {
            
            if(tradeView.isBuyBtnPressed)
            {
                tradeModel.allSelectItems.ForEach(t => InvService.Instance.invMainModel.AddItem2CommORStash(t));
                tradeModel.allSelectItems.Clear();
                netVal = new Currency(0, 0);
                tradeModel.OnSoldFrmStock();
              
            }
            else
            {
                foreach (Iitems item in tradeModel.allSelectItems)
                {
                    InvService.Instance.invMainModel.RemoveItemFrmCommInv(item);
                }
            }
            ClearSlotView();
        } 


    }
}

//public bool AddItem2InVView(Iitems item, bool onDrop = true)  // ACTUAL ADDITION 
//{
//    bool slotFound = false;
//    for (int i = 0; i < transform.childCount; i++)
//    {
//        Transform child = transform.GetChild(i);
//        iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
//        if (iSlotable.ItemsInSlot.Count > 0)
//        {
//            if (iSlotable.ItemsInSlot[0].itemName == item.itemName)
//            {
//                if (iSlotable.AddItem(item, onDrop))
//                {
//                    slotFound = true;
//                    return slotFound;
//                }
//            }
//        }
//    }
//    if (!slotFound)
//    {
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            Transform child = transform.GetChild(i);
//            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
//            if (iSlotable.AddItem(item, onDrop))
//            {
//                slotFound = true;
//                return slotFound;
//            }
//        }
//    }
//    return slotFound;
//}