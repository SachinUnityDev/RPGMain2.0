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

    public class ProvisionSlotController : MonoBehaviour, IPointerClickHandler, iSlotable
    {
        public int slotID { get; set; }
        public SlotState slotState { get; set; }
        public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
        public SlotType slotType => SlotType.ProvActiveInv;

        // no dragf and drop available here
        //[Header("FOR DROP CONTROLS")]
        //[SerializeField] GameObject draggedGO;
        //[SerializeField] ItemsDragDrop itemsDragDrop;

        [Header("RIGHT CLICK CONTROLs")]
        public List<ItemActions> rightClickActions = new List<ItemActions>();
        public bool isRightClicked = false;
        //public void OnDrop(PointerEventData eventData)
        //{
        //    draggedGO = eventData.pointerDrag;
        //    itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
        //    iSlotable islot = itemsDragDrop.iSlotable; 
        //    // cannot drag and drop an item on provision slot
        //    if (islot.slotType == SlotType.ProvActiveInv || islot.slotType == SlotType.TrophySelectSlot
        //             || islot.slotType == SlotType.TrophyScrollSlot)
        //        return; 
           
        //    if (itemsDragDrop != null)
        //    {
        //        InvService.Instance.On_DragResult(false, itemsDragDrop);
        //        //Destroy(draggedGO);
        //    }
        //}

        private void Start()
        {
            slotID = transform.GetSiblingIndex();
            isRightClicked = false;
            InvService.Instance.invRightViewController.CloseRightClickOpts();
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

        public bool AddItem(Iitems item, bool add2Model = false)
        {
            if (item.itemType != ItemType.Potions )
                //|| ItemsInSlot.Count > 0)            
            return false;
            
            AddItemOnSlot(item);
            return true;            
        }
        void AddItemOnSlot(Iitems item)
        {
            item.invSlotType = SlotType.ProvActiveInv;
            ItemsInSlot.Add(item);
            item.slotID = transform.GetSiblingIndex();

            // InvService.Instance.invMainModel.EquipItem2PotionProvSlot(item);
            //IEquipAble iequip = item as IEquipAble;
            //if (iequip != null)
            //    iequip.ApplyEquipableFX(InvService.Instance.charSelectController);
            RefreshImg(item);
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

#region POPULATE Slot Methods 
        void RefreshImg(Iitems item)
        {
            for (int i = 0; i < gameObject.transform.GetChild(0).childCount - 1; i++)
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
            }
            Transform ImgTrans = transform.GetChild(0).GetChild(0);
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

#endregion
        public void CloseRightClickOpts()
        {
            if (isRightClicked)
            {
                InvService.Instance.invRightViewController.CloseRightClickOpts();
                isRightClicked = !isRightClicked;
                return;
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (ItemsInSlot.Count > 0)
                {
                    InvService.Instance.invMainModel.AddItem2CommInv(ItemsInSlot[0]);
                    RemoveItem();
                }
            }
        }

        public void LoadSlot(Iitems item) // added only to View
        {
            if (item == null) return;
            item.invSlotType = SlotType.ProvActiveInv;
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
