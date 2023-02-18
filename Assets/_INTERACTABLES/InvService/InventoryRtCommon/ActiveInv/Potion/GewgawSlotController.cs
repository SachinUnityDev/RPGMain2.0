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
            if (ItemsInSlot[0].itemName == item.itemName
                && ItemsInSlot[0].itemType == item.itemType)
                return true;
            else
                return false;
        }

        public bool isSlotFull()
        {
            if (ItemsInSlot.Count <= ItemsInSlot[0].maxInvStackSize) return false;
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
            CharNames charName = InvService.Instance.charSelect;         

            if (item.itemType != ItemType.GenGewgaws)
                return false;

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
            item.invSlotType = SlotType.GewgawsActiveInv;
            ItemsInSlot.Add(item);
            RefreshImg(item);
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
            Iitems item = ItemsInSlot[0];
            ItemsInSlot.Remove(item);
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
                InvService.Instance.invViewController.CloseRightClickOpts();
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

