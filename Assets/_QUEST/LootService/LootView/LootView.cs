using Common;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using Town;
using System;
using DG.Tweening;

namespace Quest
{
    [Serializable]
    public class ItemBaseWithQty
    {
        public Iitems item;
        public int qty; 

        public ItemBaseWithQty(Iitems item, int qty) 
        {
            this.item = item;
            this.qty = qty;
        }

    }

    public class LootView : MonoBehaviour, IPanel
    {

        [Header("container")]
        [SerializeField] Transform containerTrans; 


        [Header("Loot All and Continue Btn")]
        [SerializeField] LootAllBtnPtrEvents lootAllBtnPtrEvents;
        [SerializeField] LootTickBtnPtrEvents lootTickPtrEvents;

        [Header("Canvas NTBR")]
        [SerializeField] Canvas canvas;

        [Header("Loot Scroll")]
        [SerializeField] int index = 1;

        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] float prevLeftClick = 0f;
        [SerializeField] float prevRightClick = 0f;


        [Header("List trackers")]
        [SerializeField] int startPos;      
        [SerializeField] int max;

        [Header("loot list and Selected list")]
        [SerializeField] List<ItemBaseWithQty> lootList = new List<ItemBaseWithQty>();
        [SerializeField] List<ItemBaseWithQty> selectedList = new List<ItemBaseWithQty>();
        void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
        }
        public void InitLootList(List<ItemDataWithQty> lootList, Transform trans)
        {
            this.lootList.Clear();this.selectedList.Clear();
            foreach (ItemDataWithQty itemQty in lootList)
            {
                Iitems item = ItemService.Instance.GetNewItem(itemQty.ItemData);
                ItemBaseWithQty itemBaseWithQty = new ItemBaseWithQty(item, itemQty.quantity);   
                this.lootList.Add(itemBaseWithQty); 
            }
            lootTickPtrEvents.InitLootTick(this);
            lootAllBtnPtrEvents.InitLootAllBtn(this);

            Load();
            max = (int)lootList.Count / 6;
            if (lootList.Count % 6 != 0)
                max++;
            FillScrollSlots();
            PosLootTable(trans);
        }

        void PosLootTable(Transform parentTrans)
        {
            //transform.SetParent(parentTrans);  
            //Vector3 pos = transform.position;

            //RectTransform rt = transform.GetComponent<RectTransform>();
            //float x  = rt.sizeDelta.x/2 + pos.x;
            //transform.DOLocalMove(new Vector3(x, pos.y, pos.z), 0.1f);
        }
        void FillScrollSlots()
        {  
            startPos = index * 6;
           
            for (int i = 0; i < containerTrans.childCount; i++)
            {
                int j = startPos + i; 
                if (j <lootList.Count)
                {
                    containerTrans.GetChild(i).GetComponent<LootSlotView>()
                    .InitSlot(lootList[j], this);
                }
                else
                {
                    containerTrans.GetChild(i).GetComponent<LootSlotView>()
                  .InitSlot(null,this);
                }
            }
        }
        public bool IsItemSelected(ItemBaseWithQty itemBaseWithQty)
        {
            return selectedList.Any(t=>t == itemBaseWithQty);            
        }
        public bool IsAllSelected()
        {
            return (lootList.Count == selectedList.Count);
        }

        public void OnLootAllSelected()
        {
            selectedList.Clear();
            selectedList.AddRange(lootList);    
            FillScrollSlots();
            UpdateTickBtnStatus();
        }
        public void OnSlotSelected(ItemBaseWithQty itemBaseWithQty)
        {
            selectedList.Add(itemBaseWithQty);
            UpdateTickBtnStatus();
        }
        public void OnSlotDeSelected(ItemBaseWithQty itemBaseWithQty)        
        { 
            selectedList.Remove(itemBaseWithQty);
            UpdateTickBtnStatus();
        }
        public void UpdateTickBtnStatus()
        {
            bool isClickable = InvService.Instance.invController.CheckIfLootCanBeAdded(selectedList);
            lootTickPtrEvents.UpdateTickBtnState(isClickable);
        }
        public void AddLoot2Inv()
        {
            foreach (ItemBaseWithQty itemQty in selectedList)
            {
                if (itemQty.item.itemType != ItemType.GenGewgaws)
                {
                    for (int i = 0; i < itemQty.qty; i++)
                    {
                       ItemService.Instance.
                        InitItemToInv(SlotType.CommonInv, itemQty.item.itemType, itemQty.item.itemName,
                                           CauseType.Loot, 1);
                    }
                }
                else
                {
                    GenGewgawQ genQ = 0; 
                    GenGewgawBase genGewgawBase = itemQty.item as GenGewgawBase;
                    if (genGewgawBase != null)
                        genQ = genGewgawBase.genGewgawQ;  

                    ItemService.Instance.
                      InitItemToInv(SlotType.CommonInv, itemQty.item.itemType, itemQty.item.itemName,
                                            CauseType.Loot, 1, genQ);
                }
            }
            lootList.Clear();
            selectedList.Clear();
            UnLoad(); 

        }

      

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            if (index <= 0)
            {
                index = max-1;
                FillScrollSlots();
            }
            else
            {
                --index;
                FillScrollSlots();
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if ((index+1) == max)
            {
                index = 0;
                FillScrollSlots();
            }
            else
            {
                ++index;
                FillScrollSlots();
            }
            prevRightClick = Time.time;
        }

        #region TO_INV_FILL
        void ClearLootFill()   
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
                child.gameObject.GetComponent<LootSlotView>().ClearSlot();
            }
        }
        #endregion


        #region INIT, LOAD, and UNLOAD
        public void Init()
        {
          
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
        #endregion
    }
}