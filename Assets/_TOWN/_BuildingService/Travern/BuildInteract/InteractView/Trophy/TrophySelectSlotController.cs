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
    public class TrophySelectSlotController : MonoBehaviour,  IPointerClickHandler, iSlotable
    {
        

        #region DECLARATIONS
        public int slotID { get; set; }
        public SlotState slotState { get; set; }
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

        #endregion

        private void Start()
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
            //if (IsEmpty())
            //{
            //    AddItemOnSlot(item, onDrop);
            //    return true;
            //}
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
                RemoveItem();
                AddItemOnSlot(item, onDrop);
                //return true;
                //
               // Debug.LogError("SLOT not empty" + (TGNames)ItemsInSlot[0].itemName);
                //AddItemOnSlot(item, onDrop);
                return false;
            }
        }
        void AddItemOnSlot(Iitems item, bool onDrop)
        {
            if (item != null)
            {   
                ItemsInSlot.Add(item);
                itemCount++;
                if (onDrop)
                {
                    BuildingIntService.Instance.tavernController.WallItem(item); 
                }
            }
            RefreshImg(item);
            // if (ItemsInSlot.Count > 1 || onDrop)
            RefreshSlotTxt();
        }
        public void LoadSlot(Iitems item)
        {
            item.invSlotType = slotType;
            ItemsInSlot.Add(item);
            RefreshImg(item);
            if (ItemsInSlot.Count > 1)
                RefreshSlotTxt();
        }
        public void RemoveItem()   // not to be used here 
        {
            BuildingIntService.Instance.tavernController.RemoveWalledItem(ItemsInSlot[0]);
            ClearSlot();
            if (ItemService.Instance.itemCardGO)
                ItemService.Instance.itemCardGO.SetActive(false);

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
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                RemoveItem(); 
            }
        }
        public void CloseRightClickOpts()
        {
          
        }
        #endregion
    }
}