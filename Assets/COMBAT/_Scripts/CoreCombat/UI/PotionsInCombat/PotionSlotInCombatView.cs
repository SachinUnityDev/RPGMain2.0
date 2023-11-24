using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
public class PotionSlotInCombatView : MonoBehaviour, IPointerClickHandler, iSlotable
{
    public int slotID { get; set; }
    public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
    [SerializeField] int itemCount = 0;
    public SlotType slotType => SlotType.ExcessInv;

    [Header("FOR DROP CONTROLS")]
    [SerializeField] GameObject draggedGO;
    [SerializeField] ItemsDragDrop itemsDragDrop;

    [Header("RIGHT CLICK CONTROLs")]
    public List<ItemActions> rightClickActions = new List<ItemActions>();
    public bool isRightClicked = false;


    private void Start()
    {
        slotID = transform.GetSiblingIndex();
        isRightClicked = false;
        InvService.Instance.commInvViewController.CloseRightClickOpts();
    }


    public void Init(Iitems item)
    {
        // Add item or make ghostly
    }

    #region I-SLOTABLE 
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
            if ((ItemsInSlot.Count + qty) <= ItemsInSlot[0].maxInvStackSize) return false;
        }
        return true;
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
        InvService.Instance.invMainModel.RemoveItemFrmExcessInv(item);  // ITEM REMOVED FROM INV MAIN MODEL HERE
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
            InvService.Instance.invMainModel.excessInvItems.Add(item); // directly added to prevent stackoverflow
            InvService.Instance.invMainModel.excessInvCount++;
        }


        RefreshImg(item);
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
    #endregion

    #region RIGHT CLICK ACTIONS ON INV RELATED
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ItemsInSlot.Count == 0) return;
        Iitems item = ItemsInSlot[0];
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                // COnsume on right click 
                }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)
                && InvService.Instance.excessInvViewController.gameObject.activeInHierarchy)
            {
                if (ItemsInSlot.Count == 0) return;

                if (item != null)
                {
                    if (InvService.Instance.invMainModel.AddItem2CommInv(item)) // remove Item from common Inv
                    {
                        RemoveItem();
                    }
                }
            }
        }

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
    }

    public void LoadSlot(Iitems item)
    {

    }

    public void CloseRightClickOpts()
    {

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
    #endregion

}
//public void OnDrop(PointerEventData eventData)
//{
//    draggedGO = eventData.pointerDrag;
//    itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
//    if (itemsDragDrop != null)
//    {
//        bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
//        if (!isDropSuccess)
//        {
//            InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
//        }
//        else
//        {
//            iSlotable islot = itemsDragDrop.iSlotable;

//            if (islot != null
//                 && (islot.slotType == SlotType.ExcessInv
//                        || islot.slotType == SlotType.CommonInv)
//                                        && islot.ItemsInSlot.Count > 0)
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