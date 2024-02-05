using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using Common;

namespace Interactables
{
    public class GewgawSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler, iSlotable
    {
        public int slotID { get; set; }
        public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
        public SlotType slotType => SlotType.GewgawsActiveInv;
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
            if (itemsDragDrop != null)
            {
                bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
                if (!isDropSuccess)
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                else
                {
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                    Destroy(draggedGO);
                }
            }
        }

        private void Start()
        {
            slotID = transform.GetSiblingIndex();
            isRightClicked = false;
            InvService.Instance.commInvViewController.CloseRightClickOpts();
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
            if (ItemsInSlot[0].itemName == item.itemName
                && ItemsInSlot[0].itemType == item.itemType)
                return true;
            else
                return false;
        }

        public bool isSlotFull(Iitems item, int qty)
        {
            if (IsEmpty())
                return false;
            if (HasSameItem(item))
            {
                if ((ItemsInSlot.Count + qty) <= ItemsInSlot[0].maxInvStackSize) return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            if (ItemsInSlot.Count > 0)
                return false;
            else
                return true;
        }

    

        public bool AddItem(Iitems item, bool onDrop = false)
        {
            CharController charController = InvService.Instance.charSelectController; 
            if (!(item.itemType == ItemType.GenGewgaws || item.itemType == ItemType.PoeticGewgaws ||
                item.itemType == ItemType.SagaicGewgaws || item.itemType == ItemType.RelicGewgaws))
                return false;

            if(item.itemType == ItemType.PoeticGewgaws)
            {
                PoeticGewgawSO poeticSO = ItemService.Instance.GetPoeticGewgawSO((PoeticGewgawNames)item.itemName); 
                if(!poeticSO.ChkEquipRestriction(charController))
                    return false;
            }
            if (item.itemType == ItemType.SagaicGewgaws)
            {
                SagaicGewgawSO sagaicSO = ItemService.Instance.GetSagaicGewgawSO((SagaicGewgawNames)item.itemName);
                if (!sagaicSO.ChkEquipRestriction(charController))
                    return false;
            }


            if (ItemsInSlot.Count > 0)
            {
                Iitems currItem = ItemsInSlot[0];
                RemoveItem();
                // add to common inv
                InvService.Instance.invMainModel.AddItem2CommInv(currItem);
            }

            if (IsEmpty())
            {
                AddItemOnSlot(item);
                return true;
            }
            return false;
        }
        void AddItemOnSlot(Iitems item)
        {
            if (item == null) return;
            item.invSlotType = SlotType.GewgawsActiveInv;
            ItemsInSlot.Add(item);
            InvService.Instance.invMainModel.AddItem2GewgawsActInv(item, slotID);
            IEquipAble iequip  = item as IEquipAble;
            if (iequip != null)
                iequip.ApplyEquipableFX(InvService.Instance.charSelectController); 
            RefreshImg(item);
            //if (ItemsInSlot.Count > 1)
            //    RefreshSlotTxt();
        }

        public void RemoveItem()   // controller by Item DragDrop
        {
            if (IsEmpty())
            {
                ClearSlot();
                return;
            }
            Iitems item = ItemsInSlot[0];
            ItemsInSlot.Remove(item);
            InvService.Instance.invMainModel.RemoveItemFromGewgawActInv(item);

            if (ItemsInSlot.Count >= 1)
            {
                RefreshImg(item);
            }
            else if (IsEmpty())  // After Item is removed
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
            transform.GetComponent<Image>().sprite = GetBGSprite(item);

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
        Sprite GetBGSprite(Iitems item)
        {
            Sprite sprite = InvService.Instance.InvSO.GetBGSprite(item);
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
                InvService.Instance.commInvViewController.CloseRightClickOpts();
                isRightClicked = !isRightClicked;
                return;
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InvService.Instance.invMainModel.AddItem2CommInv(ItemsInSlot[0]);
                RemoveItem();
            }
        }

        public void LoadSlot(Iitems item)
        {
            if (ItemsInSlot.Count > 1)
                return;
            item.invSlotType = SlotType.GewgawsActiveInv;
            ItemsInSlot.Add(item);
            RefreshImg(item);
        }

        public void RemoveAllItems()
        {
            
        }

        public bool SplitItem2EmptySlot(Iitems item, bool onDrop = true)
        {
            return false;
        }
    }
}

