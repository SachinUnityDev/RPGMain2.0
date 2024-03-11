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
    public interface iSlotable
    {
        int slotID { get; set; }
        SlotType slotType { get; }
        bool isSlotFull(Iitems item, int qty);
        bool IsEmpty();
        List<Iitems> ItemsInSlot { get; set; }
        void RemoveItem();// remove item from the inv Main  Model
        void RemoveAllItems(); 
        bool AddItem(Iitems item, bool onDrop =true);// add item to the inv Main Model 
        bool SplitItem2EmptySlot(Iitems item, bool onDrop = true);
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
        [SerializeField] int itemCount = 0;
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
            Iitems itemDragged = itemsDragDrop?.itemDragged;
            iSlotable islot = itemsDragDrop.iSlotable;
            if (islot.slotType == SlotType.ProvActiveInv) return;
            if (itemsDragDrop != null)
            {
                iSlotable prevSlot = itemsDragDrop.iSlotable;
                int c = prevSlot.ItemsInSlot.Count;
                bool isDropSuccess = AddItem(itemsDragDrop.itemDragged);
                if (!isDropSuccess)
                {
                    if(!IsEmpty() && !HasSameItem(itemDragged))// two reasons for drag failure 
                    {// try swap
                        List<Iitems> allItemsInDraggedItemSlot = new List<Iitems>(); 
                        allItemsInDraggedItemSlot.AddRange( prevSlot.ItemsInSlot);
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
                    if ((prevSlot.slotType == SlotType.CommonInv ||prevSlot.slotType == SlotType.ExcessInv))
                    {
                        int islotCount = prevSlot.ItemsInSlot.Count;
                        if(IsEmpty() || HasSameItem(itemDragged)) // simply ADD
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

        private void Start()
        {
            slotID = transform.GetSiblingIndex();
          
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
                if (HasSameItem(item))  // SAME ITEM IN SLOT 
                {
                    if (ItemsInSlot.Count < item.maxInvStackSize)  // SLOT STACK SIZE 
                    {
                        AddItemOnSlot(item, onDrop);
                        return true;
                    }
                    else
                    {
                        //  Debug.Log("Slot full");
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
            item.invSlotType = SlotType.CommonInv;
            ItemsInSlot.Add(item);
            item.slotID = slotID; 
            itemCount++;
            if (onDrop)
            {
                InvService.Instance.invMainModel.commonInvItems.Add(item); // directly added to prevent stackoverflow
                InvService.Instance.invMainModel.commonInvCount++;
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
        public void RemoveItem()   // controller by Item DragDrop
        {
            ItemService.Instance.itemCardGO.SetActive(false);
            if (IsEmpty())
            {
                ClearSlot();
                return;
            }
            Iitems item = ItemsInSlot[0];
            InvService.Instance.invMainModel.RemoveItemFrmCommInv(item);  // ITEM REMOVED FROM INV MAIN MODEL HERE
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
        #endregion

        #region RIGHT CLICK ACTIONS ON INV RELATED

        public void CloseRightClickOpts()
        {
            if (isRightClicked)
            {
                InvService.Instance.invRightViewController.CloseRightClickOpts();
                isRightClicked= false;
                return;
            }
        }
        public void ShowRightClickOpts()
        {
            isRightClicked = true;
            InvService.Instance.invRightViewController.ShowRightClickList(this);
        }

        void PopulateRightClickList()
        {
            if (ItemsInSlot.Count == 0)
            {
                CloseRightClickOpts();
                return;
            }
            
            Iitems item = ItemsInSlot[0];
            //if (isRightClicked)
            //{
            //    CloseRightClickOpts();                 
            //}
            //else
            //{
            //    InvService.Instance.commInvViewController.OpenRightClickOpts();               
            //}
           // isRightClicked = !isRightClicked; 

            rightClickActions.Clear();
            if (IsEquipable())
            {
                if (!rightClickActions.Any(t => t == ItemActions.Equipable))
                    rightClickActions.Add(ItemActions.Equipable);

            }
            if (IsSocketable())
            {
                if (!rightClickActions.Any(t => t == ItemActions.Socketable))
                    rightClickActions.Add(ItemActions.Socketable);

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
            ShowRightClickOpts(); 

        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if(isRightClicked)
                {
                    CloseRightClickOpts();
                }
                else
                {
                    PopulateRightClickList();
                    ItemService.Instance.itemCardGO.SetActive(false);
                }
            }

            if (eventData.button == PointerEventData.InputButton.Left)// transfer item to Excess Inv
            {
                if(Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)
                    && InvService.Instance.excessInvViewController.gameObject.activeInHierarchy)
                {
                    if (ItemsInSlot.Count == 0) return;
                    Iitems item = ItemsInSlot[0];
                    if (item != null)
                    {
                        if (InvService.Instance.invMainModel.AddItem2ExcessInv(item))
                        {
                            RemoveItem();
                        }
                    }
                }
            }

            if (eventData.button == PointerEventData.InputButton.Left) // Split Item in two slots 
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))                                        
                {
                    bool slotfound = false; 
                    if (ItemsInSlot.Count <= 1) return;
                    Iitems item = ItemsInSlot[0];
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

        #endregion

        #region ITEM ACTIONS 
        /// <summary>
        ///  TO BE CALLED ONLY ON BUTTON CLICK
        ///  assumed Actions will be available here i.e IConsume , I equip etc etc
        /// </summary>
        public bool IsConsumable()
        {
            if (GameService.Instance.gameModel.gameState == GameState.InTown)
                return false; 
                IConsumable iConsumable = ItemsInSlot[0] as IConsumable;
            if (iConsumable == null)
                return false;
            return true;
        }
        public void Consume()
        {
            IConsumable iconsume = ItemsInSlot[0] as IConsumable;
            iconsume.ApplyConsumableFX();
            CharController charController = InvService.Instance.charSelectController;
            ItemService.Instance.On_ItemConsumed(charController, ItemsInSlot[0]); 
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
            ItemService.Instance.On_ItemEnchanted(charController, ItemsInSlot[0]);  
            RemoveItem(); 
        }
        public bool IsEquipable()
        {
            IEquipAble isEquipable = ItemsInSlot[0] as IEquipAble;
            if (isEquipable == null)
                return false;
            return true;
        }
        public void Equip() // on right click opts
        {
            IEquipAble iEquip = ItemsInSlot[0] as IEquipAble;
           
            CharController charController = InvService.Instance.charSelectController;
            ItemData itemData = new ItemData(ItemsInSlot[0].itemType
                                                , ItemsInSlot[0].itemName);
            if (ItemsInSlot[0].itemType == ItemType.Potions)
            {
                PotionViewControllerParent parentView =  InvService.Instance.invRightViewController
                                            .potionActiveInvPanel.GetComponent<PotionViewControllerParent>();

                if (parentView.Equip2PotionSlot(ItemsInSlot[0]))
                {// equiped to a slot 
                    iEquip.ApplyEquipableFX(InvService.Instance.charSelectController);
                    RemoveItem(); 
                }
               
            }else if (ItemsInSlot[0].itemType == ItemType.GenGewgaws 
                || ItemsInSlot[0].itemType == ItemType.SagaicGewgaws 
                || ItemsInSlot[0].itemType == ItemType.PoeticGewgaws)
            {
                GewgawSlotViewControllerParent parentView = InvService.Instance.invRightViewController
                                            .gewgawsActiveInvPanel.GetComponent<GewgawSlotViewControllerParent>();

                if (parentView.Equip2GewgawSlot(ItemsInSlot[0]))
                {// equiped to a slot 
                    iEquip.ApplyEquipableFX(InvService.Instance.charSelectController);
                    RemoveItem();
                }                
            }
        }
        public void Dispose()
        {
            InvService.Instance.invMainModel.RemoveItemFrmCommInv(ItemsInSlot[0]);
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
            // item Service item  itemModel is socket socket available
            CharController charController = InvService.Instance.charSelectController;
            ItemController itemController = charController.itemController;
            ItemModel itemModel = itemController.itemModel;
            IDivGem div = ItemsInSlot[0] as IDivGem;
            ISupportGem support = ItemsInSlot[0] as ISupportGem;

            if(div != null)
            {
                return itemModel.CanSocketDivGem(ItemsInSlot[0]);          
            }
            else if(support != null)
            {
               if(itemModel.CanSocketSupportGem(ItemsInSlot[0]))
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            else
            {
                return false; 
            }            
        }
        public void Socket()
        {
            CharController charController = InvService.Instance.charSelectController;
            ItemController itemController = charController.itemController;
            ItemModel itemModel = itemController.itemModel;

            IDivGem idivGem = ItemsInSlot[0] as IDivGem;
            ISupportGem isupport = ItemsInSlot[0] as ISupportGem;
            if(idivGem != null)
            {
                itemModel.SocketItem2Armor(ItemsInSlot[0], GemType.Divine);                
            }
            else if(isupport != null)
            {
                itemModel.SocketItem2Armor(ItemsInSlot[0], GemType.Support);                
            }
            RemoveItem();
        }
        public bool IsReadable()
        {
           EnchantScrollBase enchantScrollBase = ItemsInSlot[0] as EnchantScrollBase;
           LoreBookBase loreBookBase = ItemsInSlot[0] as LoreBookBase;
            if (enchantScrollBase != null)            
                return true;                        
            else if (loreBookBase != null)
                return true;
            else
                return false;             
        }
        public void Read()
        {
            EnchantScrollBase enchantScrollBase = ItemsInSlot[0] as EnchantScrollBase;
            LoreBookBase loreBookBase = ItemsInSlot[0] as LoreBookBase;
            if (enchantScrollBase != null)
            {                
                enchantScrollBase.ApplyScrollReadFX();
                CharController charController = InvService.Instance.charSelectController;
                ItemService.Instance.On_ItemRead(charController, ItemsInSlot[0]);
                RemoveItem();
            }
            else if(loreBookBase != null)
            {
                loreBookBase.ApplyBookReadFx();
                CharController charController = InvService.Instance.charSelectController;
                ItemService.Instance.On_ItemRead(charController, ItemsInSlot[0]);
                RemoveItem();
            }
            
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