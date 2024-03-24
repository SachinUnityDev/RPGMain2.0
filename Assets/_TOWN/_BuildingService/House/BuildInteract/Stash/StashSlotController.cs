using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 


namespace Town
{
    public class StashSlotController : MonoBehaviour, IDropHandler, iSlotable, IPointerClickHandler
    {
        #region DECLARATIONS
        public int slotID { get; set; }
        public SlotState slotState { get; set; }
        public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
        [SerializeField] int itemCount = 0;
        public SlotType slotType => SlotType.StashInv;

        [Header("FOR DROP CONTROLS")]
        [SerializeField] GameObject draggedGO;
        [SerializeField] ItemsDragDrop itemsDragDrop;

        [Header("RIGHT CLICK CONTROLs")]
        public List<ItemActions> rightClickActions = new List<ItemActions>();
        public bool isRightClicked = false;
        //public void OnDrop(PointerEventData eventData)
        //{
        //    draggedGO = eventData.pointerDrag;
        //    itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
        //    if (itemsDragDrop != null)
        //    {
        //        bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
        //        if (!isDropSuccess)
        //            InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
        //        else
        //        {
        //            iSlotable islot = itemsDragDrop.iSlotable;
        //            if (islot != null
        //                 && (islot.slotType == SlotType.StashInv)                                    
        //                                    && islot.ItemsInSlot.Count > 0)
        //            {
        //                int count = islot.ItemsInSlot.Count;
        //                for (int i = 0; i < count; i++)
        //                {
        //                    if (AddItem(islot.ItemsInSlot[0])) // size of list changes with every item removal 
        //                    {
        //                        islot.RemoveItem();
        //                    }
        //                    else
        //                    {
        //                        break; // as soon as you cannot add a item just break 
        //                    }
        //                }
        //            }
        //            InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
        //            Destroy(draggedGO);
        //        }
        //    }
        //}
        public void OnDrop(PointerEventData eventData)
        {
            draggedGO = eventData.pointerDrag;
            itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
            Iitems itemDragged = itemsDragDrop?.itemDragged;
            if (itemsDragDrop != null)
            {
                iSlotable prevSlot = itemsDragDrop.iSlotable;
                int c = prevSlot.ItemsInSlot.Count;
                bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
                if (!isDropSuccess)
                {
                    if (!IsEmpty() && !HasSameItem(itemDragged))// two reasons for drag failure 
                    {// try swap
                        List<Iitems> allItemsInDraggedItemSlot = new List<Iitems>();
                        allItemsInDraggedItemSlot.AddRange(prevSlot.ItemsInSlot);
                        List<Iitems> allItemsInThisSlot = new List<Iitems>();
                        allItemsInThisSlot.AddRange(ItemsInSlot);
                        prevSlot.RemoveAllItems();
                        RemoveAllItems();
                        isDropSuccess = AddItem(itemsDragDrop.itemDragged);
                        for (int i = 0; i < allItemsInThisSlot.Count; i++)
                        {
                            if (prevSlot.AddItem(allItemsInThisSlot[0])) // ADD item in this slot to Dragged Item inv
                            {
                                isDropSuccess = true;
                            }
                            else
                            {
                                break; // as soon as you cannot add a item just break 
                            }
                        }
                        for (int i = 0; i < allItemsInDraggedItemSlot.Count; i++)
                        {
                            if (AddItem(allItemsInDraggedItemSlot[0])) // size of list changes with every item removal 
                            {
                                Debug.Log("SWAP");
                                isDropSuccess = true;
                            }
                            else
                            {
                                break; // as soon as you cannot add a item just break 
                            }
                        }
                    }
                }
                else
                {
                    if ((prevSlot.slotType == SlotType.CommonInv || prevSlot.slotType == SlotType.ExcessInv
                        || prevSlot.slotType == SlotType.StashInv))
                    {
                        int islotCount = prevSlot.ItemsInSlot.Count;
                        if (IsEmpty() || HasSameItem(itemDragged)) // simply ADD
                        {
                            for (int i = 0; i < islotCount; i++)
                            {
                                if (AddItem(itemDragged)) // size of list changes with every item removal 
                                {
                                    prevSlot.RemoveItem();
                                }
                                else
                                {
                                    break; // as soon as you cannot add a item just break 
                                }
                            }
                        }
                    }
                }
                InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                Destroy(draggedGO);
            }
        }
        #endregion

        private void Awake()
        {
            slotID = transform.GetSiblingIndex();
            isRightClicked = false;
            InvService.Instance.invRightViewController.CloseRightClickOpts();
        }

        #region SLOT ITEM HANDLING ..ADD/REMOVE/REFRESH

        public void ClearSlot()
        {
            ItemsInSlot.Clear();
            itemCount = 0;
            if (IsEmpty())
            {
                Transform ImgTrans = gameObject.transform.GetChild(0).GetChild(0);
                ImgTrans.gameObject.SetActive(false);
                gameObject.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;
                RefreshSlotTxt();
            }
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
                if ((ItemsInSlot.Count+qty) <= ItemsInSlot[0].maxInvStackSize) return false;
            }            
            return true;
        }
        public void RemoveAllItems()
        {
            int count = ItemsInSlot.Count;
            for (int i = 0; i < count; i++)
            {
                RemoveItem();
            }
        }
        public bool IsEmpty()
        {
            if (ItemsInSlot.Count > 0)
                return false;
            else
                return true;
        }
        public bool SplitItem2EmptySlot(Iitems item, bool onDrop = true)
        {
            if (IsEmpty())
            {
                AddItemOnSlot(item, onDrop);
                return true;
            }
            return false;
        }
        //public bool CanAddItem(ItemData itemData)
        //{
        //    if (IsEmpty())
        //        return true; 
        //    // get Item SO 
        //   // ItemService.Instance.getItem

        //    return false; 
        //}
        public bool AddItem(Iitems item, bool onDrop = true)
        {
            if (IsEmpty())
            {
                AddItemOnSlot(item, onDrop);
                return true;
            }
            else
            {
                if (HasSameItem(item))  // SAME ITEM IN SLOT 
                {
                    if (ItemsInSlot.Count < item.maxInvStackSize)  // SLOT STACK SIZE 
                    {
                        AddItemOnSlot(item, onDrop);
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
        void AddItemOnSlot(Iitems item, bool onDrop)
        {
            ItemsInSlot.Add(item);
            itemCount++;
            if (onDrop)
            {
                InvService.Instance.invMainModel.stashInvItems.Add(item); // directly added to prevent stackoverflow
                InvService.Instance.invMainModel.stashInvCount++;
            }
            RefreshImg(item);
            // if (ItemsInSlot.Count > 1 || onDrop)
            RefreshSlotTxt();
        }
        public void LoadSlot(Iitems item)
        {
            item.invSlotType = SlotType.StashInv;
            ItemsInSlot.Add(item);
            RefreshImg(item);
            if (ItemsInSlot.Count > 1)
                RefreshSlotTxt();
        }
        public void RemoveItem()   // controller by Item DragDrop
        {
            ItemService.Instance.itemCardGO.SetActive(false);
            if (IsEmpty())
            {
                ClearSlot();
                return;
            }
            Iitems item = ItemsInSlot[0];
            InvService.Instance.invMainModel.RemoveItemFrmStashInv(item);  // ITEM REMOVED FROM INV MAIN MODEL HERE
            ItemsInSlot.Remove(item);
            itemCount--;
            if (ItemsInSlot.Count >= 1)
            {
                RefreshImg(item);
            }
            else if (IsEmpty())  // After Item is removed
            {
                ClearSlot();
            }
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
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ItemsInSlot.Count == 0) return;
            Iitems item = ItemsInSlot[0];
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    bool slotfound = false;
                    if (ItemsInSlot.Count <= 1) return;
                    Transform parentTrans = transform.parent;
                    for (int i = 0; i < parentTrans.childCount; i++)
                    {
                        Transform child = parentTrans.GetChild(i);
                        iSlotable iSlotable = child.GetComponent<iSlotable>();
                        if (iSlotable.SplitItem2EmptySlot(item))
                        {
                            slotfound = true;
                            break;
                        }
                    }
                    if (slotfound)
                    {
                        RemoveItem();
                    }
                }
            }
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)
                    && InvService.Instance.stashInvViewController.gameObject.activeInHierarchy)
                {                 
                    if (item != null)
                    {
                        if (InvService.Instance.invMainModel.AddItem2CommInv(item))
                        {
                            RemoveItem();
                        }
                    }
                }
            }
        }

     
        #endregion
    }
}