using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using System.ComponentModel;
using Common;

namespace Interactables
{
    public interface iSlotable
    {
        int slotID { get; set; }
        SlotType slotType { get; }
        bool isSlotFull();
        List<Iitems> ItemsInSlot { get; set; }
        void RemoveItem();// remove item from the inv Main  Model
        bool AddItem(Iitems item);// add item to the inv Main Model 
        void ClearSlot(); // only clear the display 
        void LoadSlot(Iitems item); // only add to display in view 
        void CloseRightClickOpts();
    }

    public interface IComInvActions
    {
        bool IsConsumable();
        void Consume();
        bool IsEnchantable();
        void Enchant();

        bool IsSocketable();
        void Socket();
        bool IsEquipable();
        void Equip();
        void Dispose();
        bool IsSellable();
        void Sell();
        bool IsReadable();
        void Read();

        bool IsRechargeable(); 
        void RechargeGem(); 
    }

    public interface IExcessInvActions
    {
        bool Dispose();
        bool Sell();
    }




    public class ItemSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler
                                      , iSlotable, IComInvActions
    {
        #region DECLARATIONS
        public int slotID { get; set; }
        public List<Iitems> ItemsInSlot { get; set; } = new List<Iitems>();
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
            InvService.Instance.invViewController.CloseRightClickOpts();
        }

        #region SLOT ITEM HANDLING ..ADD/REMOVE/REFRESH

        public void ClearSlot()
        {
            if (!IsEmpty())
            {
                Iitems item = ItemsInSlot[0];
                ItemsInSlot.Remove(item);
            }
            else
            {
                Transform ImgTrans = gameObject.transform.GetChild(0).GetChild(0);
                ImgTrans.gameObject.SetActive(false);
                gameObject.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;
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
        public bool AddItem(Iitems item)
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
        public void LoadSlot(Iitems item)
        {
            item.invSlotType = SlotType.CommonInv;
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
            ClearSlot();
            InvService.Instance.invMainModel.RemoveItem2CommInv(item);  // ITEM REMOVED FROM INV MAIN MODEL HERE
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

        #endregion

        #region RIGHT CLICK ACTIONS ON INV RELATED

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
            if (ItemsInSlot.Count == 0)
            {
                InvService.Instance.invViewController.CloseRightClickOpts();
                return;
            }
            
            Iitems item = ItemsInSlot[0];
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

            //bool isEquipable = InvService.Instance.IsItemEquipable(item, slotType);
            //bool isConsumable = InvService.Instance.IsItemConsumable(item, slotType);
            //bool isDisposable = InvService.Instance.IsItemDisposable(item, slotType);

            rightClickActions.Clear();
            if (IsEquipable())
            {
                if (!rightClickActions.Any(t => t == ItemActions.Equipable))
                    rightClickActions.Add(ItemActions.Equipable);

            }
            if (IsConsumable())
            {
                if (!rightClickActions.Any(t => t == ItemActions.Consumable))
                    rightClickActions.Add(ItemActions.Consumable);
            }
            if (IsReadable())
            {
                if (!rightClickActions.Any(t => t == ItemActions.Readable))
                    rightClickActions.Add(ItemActions.Readable);
            }
            if (IsEnchantable())
            {
                if (!rightClickActions.Any(t => t == ItemActions.Enchantable))
                    rightClickActions.Add(ItemActions.Enchantable);
            }

            if (true) // disposable
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


        #endregion

        #region ITEM ACTIONS 
        /// <summary>
        ///  TO BE CALLED ONLY ON BUTTON CLICK
        ///  assumed Actions will be available here i.e IConsume , I equip etc etc
        /// </summary>
        public bool IsConsumable()
        {
            IConsumable iConsumable = ItemsInSlot[0] as IConsumable;
            if (iConsumable == null)
                return false;
            return true;
        }
        public void Consume()
        {
            IConsumable iconsume = ItemsInSlot[0] as IConsumable;
            iconsume.ApplyConsumableFX();
        }
        public bool IsEnchantable()
        {
            // base it on the div and support gem argument 
            CharController charController = InvService.Instance.charSelectController;
            IDivGem gem = ItemsInSlot[0] as IDivGem;
            if (gem == null)
                return false;
            if (ItemService.Instance
                .CanEnchantGemThruScroll(charController, (GemNames)ItemsInSlot[0].itemName))
            {
                return true; 
            }
            return false; 
        }
        public void Enchant()
        {
            CharController charController = InvService.Instance.charSelectController;
            charController.itemController.EnchantTheWeaponThruScroll((GemBase)ItemsInSlot[0]);
            RemoveItem(); 
        }

        public bool IsEquipable()
        {
            IEquipAble isEquipable = ItemsInSlot[0] as IEquipAble;
            if (isEquipable == null)
                return false;
            return true;
        }
        public void Equip()
        {
            IEquipAble iEquip = ItemsInSlot[0] as IEquipAble;
           
            CharController charController = InvService.Instance.charSelectController;
            ItemData itemData = new ItemData(ItemsInSlot[0].itemType
                                                , ItemsInSlot[0].itemName);
            if (ItemsInSlot[0].itemType == ItemType.Potions)
            {
                PotionViewControllerParent parentView =  InvService.Instance.invViewController
                                            .potionActiveInvPanel.GetComponent<PotionViewControllerParent>();

                if (parentView.Equip2PotionSlot(ItemsInSlot[0]))
                {// equiped to a slot 
                    iEquip.ApplyEquipableFX();
                    RemoveItem(); 
                }
               
            }else if (ItemsInSlot[0].itemType == ItemType.GenGewgaws 
                || ItemsInSlot[0].itemType == ItemType.SagaicGewgaws 
                || ItemsInSlot[0].itemType == ItemType.PoeticGewgaws)
            {
                GewgawSlotViewControllerParent parentView = InvService.Instance.invViewController
                                            .gewgawsActiveInvPanel.GetComponent<GewgawSlotViewControllerParent>();

                if (parentView.Equip2GewgawSlot(ItemsInSlot[0]))
                {// equiped to a slot 

                    RemoveItem();
                }
            }


        }
        public void Dispose()
        {
            InvService.Instance.invMainModel.RemoveItem2CommInv(ItemsInSlot[0]);
            RemoveItem();
        }
        public bool IsSellable()
        {
            return false;
        }
        public void Sell()
        {

        }
        public bool IsSocketable()
        {
            return false;
        }
        public void Socket()
        {

        }

        public bool IsReadable()
        {
           EnchantScrollBase enchantScrollBase = ItemsInSlot[0] as EnchantScrollBase;
            if (enchantScrollBase != null)
                return true;
            else
                return false; 
        }

        public void Read()
        {
            EnchantScrollBase enchantScrollBase = ItemsInSlot[0] as EnchantScrollBase;
            ScrollNames scrollName = enchantScrollBase.scrollName;
            ItemService.Instance.OnScrollRead(scrollName);
            RemoveItem();
        }

        public bool IsRechargeable()
        {
            return false; 
        }
        public void RechargeGem()
        {

        }

        #endregion
    }
} 