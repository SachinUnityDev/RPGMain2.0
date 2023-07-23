using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class TrophySelectSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler,iSlotable
    {

        #region DECLARATIONS
        public int slotID { get; set; }
        public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
        [SerializeField] int itemCount = 0;

        [SerializeField] 
        public SlotType slotType => SlotType.TrophySelectSlot;

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
                    iSlotable islot = itemsDragDrop.iSlotable;

                    if (islot != null
                         && (islot.slotType == SlotType.CommonInv ||
                                    islot.slotType == SlotType.ExcessInv)
                                            && islot.ItemsInSlot.Count > 0)
                    {
                        int count = islot.ItemsInSlot.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (AddItem(islot.ItemsInSlot[0])) // size of list changes with every item removal 
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

        #endregion

        private void Start()
        {
            slotID = transform.GetSiblingIndex();
            isRightClicked = false;
            InvService.Instance.commInvViewController.CloseRightClickOpts();
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
                if ((ItemsInSlot.Count + qty) <= ItemsInSlot[0].maxInvStackSize) return false;
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
        public bool AddItem(Iitems item, bool onDrop = true)
        {

            if (IsEmpty())
            {
                AddItemOnSlot(item, onDrop);
                return true;
            }
            else
            {
                // add item to the trophyable item in common inv
                InvService.Instance.invMainModel.AddItem2CommORStash(ItemsInSlot[0]);
                RemoveItem();
                AddItemOnSlot(item, onDrop);
                return true;
            }
        }
        void AddItemOnSlot(Iitems item, bool onDrop)
        {
            if (item != null)
            {
                ITrophyable trophyable = item as ITrophyable;
                Iitems currentItem;
                if (trophyable.tavernSlotType == TavernSlotType.Trophy)
                    currentItem = BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall; 
                if (trophyable.tavernSlotType == TavernSlotType.Pelt)
                    BuildingIntService.Instance.tavernController.tavernModel.peltOnWall = item;
                
                ItemsInSlot.Add(item);
                itemCount++;
                if (onDrop)
                {
                    
                    if (trophyable.tavernSlotType == TavernSlotType.Trophy)
                        BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall = item;
                    if (trophyable.tavernSlotType == TavernSlotType.Pelt)
                        BuildingIntService.Instance.tavernController.tavernModel.peltOnWall = item;
                }
            }
            RefreshImg(item);
            // if (ItemsInSlot.Count > 1 || onDrop)
            RefreshSlotTxt();
        }
        public void LoadSlot(Iitems item)
        {
            item.invSlotType = SlotType.CommonInv;
            ItemsInSlot.Add(item);
            RefreshImg(item);
            if (ItemsInSlot.Count > 1)
                RefreshSlotTxt();
        }
        public void RemoveItem()   // not to be used here 
        {
            ClearSlot();
            ItemService.Instance.itemCardGO.SetActive(false);
            //if (IsEmpty())
            //{
            //    ClearSlot();
            //    return;
            //}
            //Iitems item = ItemsInSlot[0];  // remove item from the slot
            //InvService.Instance.invMainModel.AddItem2CommORStash(item);
            //ITrophyable trophyable = item as ITrophyable;
            //if (trophyable.tavernSlotType == TavernSlotType.Trophy)
            //    BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall = null;
            //if (trophyable.tavernSlotType == TavernSlotType.Pelt)
            //    BuildingIntService.Instance.tavernController.tavernModel.peltOnWall = null;

            //ItemsInSlot.Remove(item);
            //itemCount--;
            //if (ItemsInSlot.Count >= 1)
            //{
            //    RefreshImg(item);
            //}
            //else if (IsEmpty())  // After Item is removed
            //{
            //    ClearSlot();
            //}
            //RefreshSlotTxt();
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

        public void OnPointerClick(PointerEventData eventData)
        {
          
        }

        public void CloseRightClickOpts()
        {
          
        }
        #endregion
    }
}