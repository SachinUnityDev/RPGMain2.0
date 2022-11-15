using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

namespace Interactables
{
    public interface iSlotable
    {
        int slotID { get; set; }
        SlotType slotType { get; }
        bool isSlotFull();
        List<InvData> ItemsInSlot { get; set;  }
        void RemoveItem();
        bool AddItem(Iitems item);
        void ClearSlot(); 
        void CloseRightClickOpts(); 
    }

    public class ItemSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler, iSlotable
    {
        public  int slotID { get; set; }
        public List<InvData> ItemsInSlot { get; set; } = new List<InvData>();
        public SlotType slotType => SlotType.CommonInv;

        [Header("FOR DROP CONTROLS")]
        [SerializeField] GameObject draggedGO;
        [SerializeField] ItemsDragDrop itemsDragDrop;

        [Header("RIGHT CLICK CONTROLs")]
        public List<ItemActions> rightClickActions = new List<ItemActions>();
        public bool isRightClicked = false;
        public void OnDrop(PointerEventData eventData)
        {
            draggedGO = eventData.pointerDrag;
            itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
            if(itemsDragDrop != null)
            {
                bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
                if (!isDropSuccess)
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                else
                {
                    iSlotable islot = itemsDragDrop.iSlotable;
                    
                    if (islot != null 
                         && (islot.slotType == SlotType.CommonInv ||
                                    islot.slotType == SlotType.ExcessInv)
                                            && islot.ItemsInSlot.Count > 0)
                            {                 
                                int count = islot.ItemsInSlot.Count; 
                                for (int i = 0; i < count ; i++)
                                {
                                    if (AddItem(islot.ItemsInSlot[0].item)) // size of list changes with every item removal 
                                    {
                                        islot.RemoveItem();
                                    }
                                    else
                                    {
                                        break; // as soon as you cannot add a item just break 
                                    }
                                }
                            }
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                    Destroy(draggedGO);
                }
            }
        }
       
        private void Start()
        {
            slotID = transform.GetSiblingIndex();
            isRightClicked = false;
            InvService.Instance.invViewController.CloseRightClickOpts();
        }

        public void ClearSlot()
        {
            ItemsInSlot.Clear();         
            Transform ImgTrans = gameObject.transform.GetChild(0).GetChild(0);
            ImgTrans.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;             
        }

        public bool HasSameItem(Iitems item)
        {
            if (ItemsInSlot[0].item.itemName == item.itemName 
                && ItemsInSlot[0].item.itemType == item.itemType)
                return true;
            else
                return false;
        }

        public bool isSlotFull()
        {          
            if (ItemsInSlot.Count <= ItemsInSlot[0].item.maxInvStackSize) return false; 
            return true; 
        }

        public bool IsEmpty()
        {
            if (ItemsInSlot.Count > 0)
                return false;
            else
                return true;
        }

        public bool AddItem(Iitems item)
        {
            CharNames charName = InvService.Instance.charSelect;                
            InvData invData = new InvData(charName, item); 
            if (IsEmpty())
            {
                AddItemOnSlot(invData);               
                return true;
            }
            else
            {
                if (HasSameItem(item))  // SAME ITEM IN SLOT 
                {
                    if (ItemsInSlot.Count < item.maxInvStackSize)  // SLOT STACK SIZE 
                    {
                        AddItemOnSlot(invData);                       
                        return true;
                    }
                    else
                    {
                        Debug.Log("Slot full");
                        return false;
                    }
                }
                else   // DIFF ITEM IN SLOT 
                {
                    return false;
                }
            }
        }
        void AddItemOnSlot(InvData invData)
        {
            invData.item.invType = SlotType.CommonInv;
            ItemsInSlot.Add(invData);           
            InvService.Instance.invMainModel.commonInvItems.Add(invData);

            RefreshImg(invData.item);
            if (ItemsInSlot.Count > 1)
                RefreshSlotTxt();
        }

        public void RemoveItem()   // controller by Item DragDrop
        {
            if (IsEmpty())
            {
                ClearSlot();
                return;
            }
            InvData invData =  ItemsInSlot[0];
            ItemsInSlot.Remove(invData);
            if (ItemsInSlot.Count >= 1)
            {
                RefreshImg(invData.item);
            }
            else if(IsEmpty())  // After Item is removed
            {
                ClearSlot();
            }
           // COUNTER = ItemsInSlot.Count;
            RefreshSlotTxt();
        }
        
        void RefreshImg(Iitems item)
        {
            for (int i = 0; i < gameObject.transform.GetChild(0).childCount - 1; i++)
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
            }
            Transform ImgTrans = gameObject.transform.GetChild(0).GetChild(0);
            ImgTrans.GetComponent<Image>().sprite = GetSprite(item);
            ImgTrans.gameObject.SetActive(true);
            // clear Extra GO
          
        }
        void RefreshSlotTxt()
        {
            Transform txttrans = gameObject.transform.GetChild(1);

            if (ItemsInSlot.Count > 1)
            {
                txttrans.gameObject.SetActive(true);
                txttrans.GetComponentInChildren<TextMeshProUGUI>().text = ItemsInSlot.Count.ToString();
            }
            else
            {
                txttrans.gameObject.SetActive(false);
            }

        }
        Sprite GetSprite(Iitems item)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite(item.itemName, item.itemType);
            if (sprite != null)
                return sprite;
            else            
                Debug.Log("SPRITE NOT FOUND");
                return null;            
        }

        public void CloseRightClickOpts()
        {
            if (isRightClicked)
            {
                InvService.Instance.invViewController.CloseRightClickOpts();
                isRightClicked = !isRightClicked;
                return;
            }
        }

        void PopulateRightClickList()
        {
            Iitems item = ItemsInSlot[0].item;
            if (isRightClicked)
            {
                InvService.Instance.invViewController.CloseRightClickOpts();
                isRightClicked = !isRightClicked;
                return; 
            }
            else
            {
                InvService.Instance.invViewController.OpenRightClickOpts();
                isRightClicked = !isRightClicked;
            }
            
            // get frame from and Buttons frame from InvSO
            // populate button name from the Item Actions..Inv SO strings
            bool isEquipable = InvService.Instance.IsItemEquipable(item);
            bool isConsumable = InvService.Instance.IsItemConsumable(item);
            bool isDisposable = InvService.Instance.IsItemDispoable(item);
            // bool isSellable = InvService.Instance.IsItemDispoable(item);
            // bool isPurchaseable = InvService.Instance.IsItemDispoable(item);

            rightClickActions.Clear();
            if (isEquipable)
            {
                if(!rightClickActions.Any(t=>t == ItemActions.Equipable))
                        rightClickActions.Add(ItemActions.Equipable);

            }
            if (isConsumable)
            {
                if (!rightClickActions.Any(t => t == ItemActions.Consumable))
                    rightClickActions.Add(ItemActions.Consumable);
            }
            if (isDisposable)
            {
                if (!rightClickActions.Any(t => t == ItemActions.Disposable))
                    rightClickActions.Add(ItemActions.Disposable);
            }

            InvService.Instance.invViewController.ShowRightClickList(this);  

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                PopulateRightClickList();
            }
        }

    }
}

