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

    public class ProvisionSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler, iSlotable
    {
        public int slotID { get; set; }
        public List<InvData> ItemsInSlot { get; set; } = new List<InvData>();
        public SlotType slotType => SlotType.ProvActiveInv;

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

            if (item.itemType != ItemType.Potions || ItemsInSlot.Count > 0)
            {
                return false;
            }

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
            ItemsInSlot.Add(invData);
            // COUNTER = ItemsInSlot.Count;
          

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
            InvData invData = ItemsInSlot[0];
            ItemsInSlot.Remove(invData);
            if (ItemsInSlot.Count >= 1)
            {
                RefreshImg(invData.item);
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
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InvService.Instance.invMainModel.AddItem2CommInv(ItemsInSlot[0]);
                RemoveItem();
            }
        }
    }
}
