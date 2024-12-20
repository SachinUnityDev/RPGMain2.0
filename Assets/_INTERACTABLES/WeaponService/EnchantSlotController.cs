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

    public class EnchantSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler, iSlotable
    {
        public int slotID { get; set; }
        public SlotState slotState { get; set; }
        public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
        public SlotType slotType => SlotType.EnchantSlot;

        [Header("FOR DROP CONTROLS")]
        [SerializeField] GameObject draggedGO;
        [SerializeField] ItemsDragDrop itemsDragDrop;

        [Header("RIGHT CLICK CONTROLs")]
        public List<ItemActions> rightClickActions = new List<ItemActions>();
        public bool isRightClicked = false;
        public void OnDrop(PointerEventData eventData)
        {
            //draggedGO = eventData.pointerDrag;
            //itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
            //if (itemsDragDrop != null)
            //{
            //    bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
            //    if (!isDropSuccess)
            //        InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
            //    else
            //    {
            //        InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
            //        Destroy(draggedGO);
            //    }
            //}
        }

        private void Start()
        {
            slotID = transform.GetSiblingIndex();
            isRightClicked = false;
            InvService.Instance.invRightViewController.CloseRightClickOpts();
        }

        public void ClearSlot()
        {
            ItemsInSlot.Clear();
            Transform ImgTrans = transform.GetChild(0); // check this 
            ImgTrans.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().sprite = WeaponService.Instance.allWeaponSO.emptySlot;
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

        public bool AddItem(Iitems item, bool add2Model = false)
        {         
            if (IsEmpty())
            {
                AddItemOnSlot(item);
                return true;
            }
            else
            {
                if (HasSameItem(item))  // SAME ITEM IN SLOT 
                {
                    if (ItemsInSlot.Count < item.maxInvStackSize)  // SLOT STACK SIZE 
                    {
                        AddItemOnSlot(item);
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
        void AddItemOnSlot(Iitems item)
        {
            item.invSlotType = SlotType.CommonInv;
            ItemsInSlot.Add(item);
            InvService.Instance.invMainModel.commonInvItems.Add(item);

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
                InvService.Instance.invRightViewController.CloseRightClickOpts();
                isRightClicked = !isRightClicked;
                return;
            }
        }

        void PopulateRightClickList()
        {
            ///FOLLOWING CODE FOR INV SLOT NOT APPLICABLE HERE

            //Iitems item = ItemsInSlot[0];
            //if (isRightClicked)
            //{
            //    InvService.Instance.invViewController.CloseRightClickOpts();
            //    isRightClicked = !isRightClicked;
            //    return;
            //}
            //else
            //{
            //    InvService.Instance.invViewController.OpenRightClickOpts();
            //    isRightClicked = !isRightClicked;
            //}

            //// get frame from and Buttons frame from InvSO
            //// populate button name from the Item Actions..Inv SO strings
            //bool isEquipable = InvService.Instance.IsItemEquipable(item);
            //bool isConsumable = InvService.Instance.IsItemConsumable(item);
            //bool isDisposable = InvService.Instance.IsItemDispoable(item);
            //// bool isSellable = InvService.Instance.IsItemDispoable(item);
            //// bool isPurchaseable = InvService.Instance.IsItemDispoable(item);

            //rightClickActions.Clear();
            //if (isEquipable)
            //{
            //    if (!rightClickActions.Any(t => t == ItemActions.Equipable))
            //        rightClickActions.Add(ItemActions.Equipable);

            //}
            //if (isConsumable)
            //{
            //    if (!rightClickActions.Any(t => t == ItemActions.Consumable))
            //        rightClickActions.Add(ItemActions.Consumable);
            //}
            //if (isDisposable)
            //{
            //    if (!rightClickActions.Any(t => t == ItemActions.Disposable))
            //        rightClickActions.Add(ItemActions.Disposable);
            //}

            ////InvService.Instance.invViewController.ShowRightClickList(this);

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                PopulateRightClickList();
            }
        }

        public void LoadSlot(Iitems item)
        {
           
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

