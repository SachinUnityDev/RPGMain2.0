using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Interactables
{
    public class GewgawSlotViewControllerParent : MonoBehaviour
    {
        // to be attached to the parent active inv Controller 

        // rules for the gewgaw fill up

        // you cannot have two slot type(back, neck etc ..) among the three active slots 
        // you cannot have more that more one SAGAIC in active inv 
        // Can have more than poetic, lyric, folkloric, epic in active inv
        // one slot can have item 

        [Header("Canvas Not to be ref")]
        [SerializeField] Canvas canvas;


        [SerializeField] int slotNum;

        [Header("GEWGAW SLOT RULE RELATED")]
        public Iitems item1;
        public Iitems item2; 
        public Iitems item3;

        public List<SlotType> allSlotTypes;
    
        // get and set items
        //  public List<Iitems> allPotionActiveInvList = new List<Iitems>();

        public Transform rightClickOpts;

        [Header("Slot results")]
        [SerializeField] bool slot1result;
        [SerializeField] bool slot2result;
        [SerializeField] bool slot3result;

        void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            InvService.Instance.OnDragResult += OnDragResult2GewgawActiveInv;
            InvService.Instance.OnCharSelectInvPanel += LoadActiveInvSlots;
            slotNum = transform.GetSiblingIndex();
            for (int i = 0; i < 3; i++)
            {
                allSlotTypes.Add(SlotType.None); 
            }
        }

        //bool SlotRuleCheck()
        //{

        //    return false; 
        //}
        //GewgawSlotType GetSlotType(Iitems item)
        //{
        //    if (item.itemType == ItemType.GenGewgaws)
        //    {
        //        GenGewgawSO gengewgawSO =
        //            ItemService.Instance.allItemSO.GetGenGewgawSO((GenGewgawNames)item.itemName);
        //        GewgawSlotType slotType = gengewgawSO.gewgawSlotType;
        //        return slotType;
        //    }
        //    if(item.itemType == ItemType.SagaicGewgaws)
        //    {
        //        SagaicGewgawSO sagaicSO = 
        //            ItemService.Instance.allItemSO.GetSagaicGewgawSO((SagaicGewgawNames)item.itemName);
        //        GewgawSlotType slotType = sagaicSO.gewgawSlotType;
        //        return slotType;
        //    }
        //    if (item.itemType == ItemType.PoeticGewgaws)
        //    {
        //        PoeticGewgawSO poeticSO =
        //            ItemService.Instance.allItemSO.GetPoeticGewgawSO((PoeticGewgawNames)(item.itemName));                    
        //        GewgawSlotType slotType = poeticSO.gewgawSlotType;
        //        return slotType;
        //    }
        //    return 0; 
        //}

        void LoadActiveInvSlots(CharModel charModel)
        {
            ClearInv();
            CharController charController = InvService.Instance.charSelectController;
            if (charController == null) return;
            ActiveInvData activeInvData = InvService.Instance.invMainModel
                                            .GetActiveInvData(charController.charModel.charID);
            if (activeInvData == null) return;
            for (int i = 0; i < activeInvData.gewgawActivInv.Count; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.GetComponent<iSlotable>().LoadSlot(activeInvData.gewgawActivInv[i]);
            }
        }

        #region EQUIP TO VIEW from selection MENU
        public bool Equip2GewgawSlot(Iitems item)  // right click 
        {
            // try equip to slot 1 then 2 
            // and if both fails then remove from slot 1 
            CharController charController = InvService.Instance.charSelectController;

            if (!(item.itemType == ItemType.GenGewgaws || item.itemType == ItemType.PoeticGewgaws ||
             item.itemType == ItemType.SagaicGewgaws || item.itemType == ItemType.RelicGewgaws))
                return false;

            if(!ItemService.Instance.allItemSO.IsGewgawEquipable(charController, item)) // if false return
                return false;
            int slotWithSameSlotType = ItemService.Instance.allItemSO.IsSlotRestricted(charController, item); 
            if (slotWithSameSlotType != -1)
            {
                Swap(item, slotWithSameSlotType);
                return true;
            }

            slot1result = false; slot2result = false;
                slot3result =false;
                slot1result = AddItemtoActiveSlotView(item, 0);
                if (!slot1result) // try slot 2 
                {
                    slot2result = AddItemtoActiveSlotView(item, 1);
                    if (slot2result)
                        return true;
                    else
                        slot3result = AddItemtoActiveSlotView(item, 2); 

                    if(slot3result)
                        return true;
                    else
                        Swap(item, 0);
                        return true;
                }
                return true;            
        }
        #endregion

        public bool AddItemtoActiveSlotView(Iitems item, int slotID)  // NEW ITEM ADDED
        {
            Transform child = transform.GetChild(slotID);
            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
            if (iSlotable.ItemsInSlot.Count == 0)
            {
                iSlotable.AddItem(item);
                return true;
            }
            return false;
        }
        void Swap(Iitems itemAdd, int slotID)
        {
            Transform child = transform.GetChild(slotID);
            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();            
            if(iSlotable.ItemsInSlot[0] != null)
            {
                InvService.Instance.invMainModel.AddItem2CommInv(iSlotable.ItemsInSlot[0]);
                iSlotable.RemoveItem(); 
            }
            AddItemtoActiveSlotView(itemAdd, slotID); 
        }

        void ClearInv()
        {
        
            for (int i = 0; i < 3; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.GetComponent<iSlotable>().ClearSlot();
            }
        }



         void OnDragResult2GewgawActiveInv(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.GewgawsActiveInv)
            {
                Debug.Log(result + " Drag fail result Invoked ");
                Transform slotParent = itemsDragDrop.slotParent;
                itemsDragDrop.transform.DOMove(slotParent.position, 0.1f);

                itemsDragDrop.transform.SetParent(slotParent);
                RectTransform cloneRect = itemsDragDrop.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector3.zero;
                cloneRect.localScale = Vector3.one;

                itemsDragDrop.iSlotable.AddItem(itemsDragDrop.itemDragged);
            }
        }

        #region INIT LOAD UNLOAD
        public void Init()
        {

        }

        public void Load()
        {


        }

        public void UnLoad()
        {
        }
        #endregion 
        public void ShowRightClickList(ItemSlotController itemSlotController)
        {

            int i = 0;
            foreach (ItemActions itemAction in itemSlotController.rightClickActions)
            {
                Transform btn = rightClickOpts.GetChild(i);
                btn.GetComponentInChildren<TextMeshProUGUI>().text
                    = InvService.Instance.InvSO.GetItemActionStrings(itemAction);
                // this will have a diff class 
                // btn.GetComponent<ItemActionPtrController>().Init(itemAction);
                i++;
            }
            for (int j = i; j < rightClickOpts.childCount; j++)
            {
                rightClickOpts.GetChild(j).gameObject.SetActive(false);
            }
            Transform slotTrans = itemSlotController.gameObject.transform;
            Vector3 offset = new Vector3(100 / 2f, 130 / 2f, 0f) * canvas.scaleFactor;

            rightClickOpts.DOMove(slotTrans.position + offset, 0.01f);
        }

        public void OpenRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(true);
        }
        public void CloseRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(false);
        }


    }




}
//if (item.itemType == ItemType.GenGewgaws)
//{
//    GenGewgawSO genGewgawSO = ItemService.Instance.allItemSO.GetGenGewgawSO((GenGewgawNames)item.itemName);
//    if (!genGewgawSO.ChkEquipRestriction(charController))
//        return false;
//}
//if (item.itemType == ItemType.PoeticGewgaws)
//{
//    PoeticGewgawSO poeticSO = ItemService.Instance.allItemSO.GetPoeticGewgawSO((PoeticGewgawNames)item.itemName);
//    if (!poeticSO.ChkEquipRestriction(charController))
//        return false;
//}
//if (item.itemType == ItemType.SagaicGewgaws)
//{
//    SagaicGewgawSO sagaicSO = ItemService.Instance.allItemSO.GetSagaicGewgawSO((SagaicGewgawNames)item.itemName);
//    if (!sagaicSO.ChkEquipRestriction(charController))
//        return false;
//}