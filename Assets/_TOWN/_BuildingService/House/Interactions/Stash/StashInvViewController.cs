using Common;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI;


namespace Town
{
    public class StashInvViewController : MonoBehaviour, IPanel
    {
        [SerializeField] Button reverseBtn;

        [Header("Canvas NTBR")]
        [SerializeField] Canvas canvas;

        [Header("Excess Panel Ref")]
        public int currExcessInvSize;
        public Stack<Iitems> itemSent2CommInv = new Stack<Iitems>();
        [Header("to be ref")]
        [SerializeField] Transform slotContainer; 
        void Awake()
        {
            InvService.Instance.OnDragResult += OnDragResult2Stash;
           // reverseBtn.onClick.AddListener(OnReverseBtnPressed);

        }
        void OnReverseBtnPressed()
        {


            //for (int i = 0; i < transform.GetChild(0).childCount; i++)
            //{
            //    Transform child = transform.GetChild(0).GetChild(i);  // go
            //    child.gameObject.GetComponent<ExcessItemSlotController>().RemoveAllItems();
            //}
            //// ClearInv();  
            //InvService.Instance.invMainModel.excessInvItems.Clear();
        }

        void OnSellAllPressed()
        {
            //// iitem get SO or directly get price
            //for (int i = 0; i < transform.GetChild(0).childCount; i++)
            //{
            //    Transform child = transform.GetChild(0).GetChild(i);  // go
            //    iSlotable iSlot = child.gameObject.GetComponent<iSlotable>();
            //    if (!child.gameObject.GetComponent<ExcessItemSlotController>().IsEmpty())
            //    {
            //        int count = iSlot.ItemsInSlot.Count;
            //        Iitems item = iSlot.ItemsInSlot[0];
            //        if (count > 0)
            //        {
            //            CostData costData =
            //            ItemService.Instance.GetCostData(item.itemType, item.itemName);
            //            // add to play Eco and dispose item
            //            int silver = (costData.cost.silver / 3) * count;
            //            int bronze = (costData.cost.bronze / 3) * count;
            //            Currency itemSaleVal = new Currency(silver, bronze).RationaliseCurrency();

            //            EcoServices.Instance.AddMoney2PlayerInv(itemSaleVal);
            //            iSlot.RemoveAllItems();
            //        }
            //    }

            //}
        }

        #region SLOT RELATED 
        public void ShowRightClickList(ItemSlotController itemSlotController)
        {

            //int i = 0;
            //foreach (ItemActions itemAction in itemSlotController.rightClickActions)
            //{
            //    Transform btn = rightClickOpts.GetChild(i);
            //    btn.GetComponentInChildren<TextMeshProUGUI>().text
            //        = InvService.Instance.InvSO.GetItemActionStrings(itemAction);
            //    //btn.GetComponent<ItemActionPtrController>().Init(itemAction);
            //    i++;
            //}
            //for (int j = i; j < rightClickOpts.childCount; j++)
            //{
            //    rightClickOpts.GetChild(j).gameObject.SetActive(false);
            //}
            //Transform slotTrans = itemSlotController.gameObject.transform;
            //Vector3 offset = new Vector3(100 / 2f, 130 / 2f, 0f) * canvas.scaleFactor;

            //rightClickOpts.DOMove(slotTrans.position + offset, 0.01f);
        }

        public void OpenRightClickOpts()
        {
          //  rightClickOpts.gameObject.SetActive(true);
        }
        public void CloseRightClickOpts()
        {
           // rightClickOpts.gameObject.SetActive(false);
        }
        public void OnDragResult2Stash(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.StashInv)
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

        #endregion

        #region TO_INV_FILL
        public bool AddItem2InVView(Iitems item, bool onDrop = true)  // ACTUAL ADDITION 
        {
            bool slotFound = false;
            for (int i = 0; i < slotContainer.childCount; i++)
            {
                Transform child = slotContainer.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (iSlotable.ItemsInSlot.Count > 0)
                {
                    if (iSlotable.ItemsInSlot[0].itemName == item.itemName)
                    {
                        if (iSlotable.AddItem(item, onDrop))
                        {
                            slotFound = true;
                            return slotFound;
                        }
                    }
                }
            }
            if (!slotFound)
            {
                for (int i = 0; i < slotContainer.childCount; i++)
                {
                    Transform child = slotContainer.GetChild(i);
                    iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                    if (iSlotable.AddItem(item, onDrop))
                    {
                        slotFound = true;
                        return slotFound;
                    }
                }
            }
            return slotFound;
        }
     
        public bool IsStashInvFull()
        {
            for (int i = 0; i < slotContainer.childCount; i++)
            {
                Transform child = slotContainer.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (!iSlotable.isSlotFull())
                {
                    return false;
                }
            }
            return true;
        }
        public void InitStashInv()
        {
            ClearInv();
            foreach (var item in InvService.Instance.invMainModel.stashInvIntItems.ToList())
            {
                AddItem2InVView(item, false);
            }
        }

        void ClearInv()
        {
            for (int i = 0; i < slotContainer.childCount; i++)
            {
                Transform child = slotContainer.GetChild(i);  // go
                child.gameObject.GetComponent<StashSlotController>().ClearSlot();
            }
        }

        public void Load()
        {
            
        }

        public void UnLoad()
        {
         
        }

        public void Init()
        {
            InitStashInv(); 
        }


        #endregion
    }
}